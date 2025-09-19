using System.Collections;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Audience : MonoBehaviour
    {
        public enum AudienceState { Idle, Moving, Throwing }

        [SerializeField] private GameEvent throwBall;
        [SerializeField] private float moveSpeed = 25f;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private DodgeBallManager dodgeballManager;

        private Coroutine throwRoutine;
        private Coroutine moveRoutine;
        private AudienceState currentState = AudienceState.Idle;

        private void OnDisable()
        {
            if (moveRoutine != null) StopCoroutine(moveRoutine);
            if (throwRoutine != null) StopCoroutine(throwRoutine);
        }

        public void StartRandomThrowLoop()
        {
            if (currentState == AudienceState.Throwing) return;

            SetAudienceState(AudienceState.Throwing);
        }

        public void StopRandomThrowLoop()
        {
            if (throwRoutine != null)
            {
                StopCoroutine(throwRoutine);
                throwRoutine = null;
            }

            SetAudienceState(AudienceState.Idle);
        }
        public void StartMovementLoop()
        {
            SetAudienceState(AudienceState.Moving);
        }

        private void SetAudienceState(AudienceState newState)
        {
            if (currentState == newState) return;

            currentState = newState;
            ApplyConstraints(newState);

            switch (newState)
            {
                case AudienceState.Moving:
                    StartNewRoutine(ref moveRoutine, MovementLoop());
                    break;
                case AudienceState.Throwing:
                    StopCoroutineIfRunning(ref moveRoutine);
                    StartNewRoutine(ref throwRoutine, RandomThrowLoop());
                    break;
                case AudienceState.Idle:
                    StopCoroutineIfRunning(ref moveRoutine);
                    StopCoroutineIfRunning(ref throwRoutine);
                    break;
            }
        }

        private void ApplyConstraints(AudienceState state)
        {
            switch (state)
            {
                case AudienceState.Throwing:
                    _rb.constraints = RigidbodyConstraints2D.FreezePositionX |
                                      RigidbodyConstraints2D.FreezePositionY |
                                      RigidbodyConstraints2D.FreezeRotation;
                    break;
                case AudienceState.Moving:
                    _rb.constraints = RigidbodyConstraints2D.FreezeRotation |
                                      RigidbodyConstraints2D.FreezePositionY;
                    break;
                default:
                    _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    break;
            }
        }

        private void StartNewRoutine(ref Coroutine routine, IEnumerator method)
        {
            StopCoroutineIfRunning(ref routine);
            routine = StartCoroutine(method);
        }

        private void StopCoroutineIfRunning(ref Coroutine routine)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
                routine = null;
            }
        }

        private IEnumerator RandomThrowLoop()
        {
            while (dodgeballManager.gameState != DodgeBallManager.GameState.Playing)
                yield return null;

            _rb.velocity = Vector2.zero;

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

            throwBall.Raise();

            yield return new WaitForSeconds(0.5f);

            SetAudienceState(AudienceState.Moving);
        }

        private IEnumerator MovementLoop()
        {
            while (true)
            {
                while (dodgeballManager.gameState != DodgeBallManager.GameState.Playing)
                    yield return null;

                float xDir = Random.Range(0, 2) == 0 ? -1f : 1f;
                Vector2 moveDir = new Vector2(xDir, 0f);

                _rb.velocity = moveDir * moveSpeed;

                yield return new WaitForSeconds(Random.Range(1f, 2f));
            }
        }
    }
}
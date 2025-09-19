using System.Collections;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Enemy : MonoBehaviour
    {
        public enum EnemyState { Idle, Moving, Throwing }

        [SerializeField] private GameEvent throwBall;
        [SerializeField] private Animator _animator;
        [SerializeField] private float moveSpeed = 25f;
        [SerializeField] private LayerMask teammateLayer;
        [SerializeField] private float avoidRadius = 5.6f;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private DodgeBallManager dodgeballManager;

        private Coroutine throwRoutine;
        private Coroutine moveRoutine;
        private EnemyState currentState = EnemyState.Idle;

        private void OnDisable()
        {
            StopCoroutineIfRunning(ref moveRoutine);
            StopCoroutineIfRunning(ref throwRoutine);
        }

        public void StartRandomThrowLoop()
        {
            if (currentState == EnemyState.Throwing) return;

            SetEnemyState(EnemyState.Throwing);
        }

        public void StopRandomThrowLoop()
        {
            StopCoroutineIfRunning(ref throwRoutine);
            SetEnemyState(EnemyState.Idle);
        }

        public void StartMovementLoop()
        {
            SetEnemyState(EnemyState.Moving);
        }
        private void SetEnemyState(EnemyState newState)
        {
            if (currentState == newState) return;

            currentState = newState;
            ApplyConstraints(newState);

            switch (newState)
            {
                case EnemyState.Moving:
                    StartNewRoutine(ref moveRoutine, MovementLoop());
                    break;
                case EnemyState.Throwing:
                    StopCoroutineIfRunning(ref moveRoutine);
                    StartNewRoutine(ref throwRoutine, RandomThrowLoop());
                    break;
                case EnemyState.Idle:
                    StopCoroutineIfRunning(ref moveRoutine);
                    StopCoroutineIfRunning(ref throwRoutine);
                    break;
            }
        }

        private void ApplyConstraints(EnemyState state)
        {
            switch (state)
            {
                case EnemyState.Throwing:
                    _rb.constraints = RigidbodyConstraints2D.FreezePositionX |
                                      RigidbodyConstraints2D.FreezePositionY |
                                      RigidbodyConstraints2D.FreezeRotation;
                    break;
                case EnemyState.Moving:
                    _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
            PlayThrowAnimation();

            yield return new WaitForSeconds(0.5f);

            SetEnemyState(EnemyState.Moving);
        }

        private IEnumerator MovementLoop()
        {
            while (dodgeballManager.gameState == DodgeBallManager.GameState.Playing)
            {
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                Vector3 checkPosition = transform.position + (Vector3)randomDir * avoidRadius;

                Collider2D hit = Physics2D.OverlapCircle(checkPosition, avoidRadius, teammateLayer);
                bool isBlocked = hit != null && hit.gameObject != gameObject;

                _rb.velocity = isBlocked ? Vector2.zero : randomDir * moveSpeed;

                yield return new WaitForSeconds(Random.Range(1f, 2f));
            }
        }

        void PlayThrowAnimation()
        {
            _animator.SetTrigger("Throw");
        }
    }
}
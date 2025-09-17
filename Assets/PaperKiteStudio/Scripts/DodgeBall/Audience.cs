using System.Collections;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Audience : MonoBehaviour
    {
        [SerializeField] private GameEvent throwBall;
        [SerializeField] private float moveSpeed = 25f;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private DodgeBallManager dodgeballManager;
        [SerializeField] private bool hasBall = false;

        private Coroutine throwRoutine;
        private Coroutine moveRoutine;

        private void OnEnable()
        {
            //moveRoutine = StartCoroutine(MovementLoop());
        }

        private void OnDisable()
        {
            if (moveRoutine != null) StopCoroutine(moveRoutine);
            if (throwRoutine != null) StopCoroutine(throwRoutine);
        }

        public void StartRandomThrowLoop()
        {
            if (hasBall) return;

            hasBall = true;

            if (moveRoutine != null)
            {
                StopCoroutine(moveRoutine);
                moveRoutine = null;
            }

            if (throwRoutine != null) StopCoroutine(throwRoutine);

            _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

            throwRoutine = StartCoroutine(RandomThrowLoop());
        }

        public void StopRandomThrowLoop()
        {
            if (throwRoutine != null)
            {
                StopCoroutine(throwRoutine);
                throwRoutine = null;
            }
        }

        private IEnumerator RandomThrowLoop()
        {
            while (dodgeballManager.gameState != DodgeBallManager.GameState.Playing)
            {
                yield return null;
            }

            _rb.velocity = Vector2.zero;

            float waitTime = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(waitTime);

            throwBall.Raise();

            throwRoutine = null;

            yield return new WaitForSeconds(0.5f);

            _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            moveRoutine = StartCoroutine(MovementLoop());
            hasBall = false;
        }

        private IEnumerator MovementLoop()
        {
            while (true)
            {
                while (dodgeballManager.gameState != DodgeBallManager.GameState.Playing)
                {
                    yield return null;
                }

                // Choose a random direction: left or right
                float xDir = Random.Range(0, 2) == 0 ? -1f : 1f;
                Vector2 moveDir = new Vector2(xDir, 0f);

                _rb.velocity = moveDir * moveSpeed;

                yield return new WaitForSeconds(Random.Range(1f, 2f));
            }
        }

        public void StartMovementLoop()
        {
            moveRoutine = StartCoroutine(MovementLoop());
        }
    }
}
//public class Audience : MonoBehaviour
//{
//    [SerializeField] private GameEvent throwBall;
//    [SerializeField] private float moveSpeed = 25f;
//    [SerializeField] private Rigidbody2D _rb;
//    [SerializeField] private DodgeBallManager dodgeballManager;
//    [SerializeField] private GameState requiredState;



//    private Coroutine throwRoutine;
//    private Coroutine moveRoutine;

//    [SerializeField] private bool hasBall = false;

//    private void OnEnable()
//    {
//        //moveRoutine = StartCoroutine(MovementLoop());
//    }

//    private void OnDisable()
//    {
//        if (moveRoutine != null) StopCoroutine(moveRoutine);
//        if (throwRoutine != null) StopCoroutine(throwRoutine);
//    }

//    public void StartRandomThrowLoop()
//    {
//        if (hasBall) return;

//        hasBall = true;

//        if (moveRoutine != null)
//        {
//            StopCoroutine(moveRoutine);
//            moveRoutine = null;
//        }

//        if (throwRoutine != null) StopCoroutine(throwRoutine);

//        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

//        throwRoutine = StartCoroutine(RandomThrowLoop());
//    }

//    public void StopRandomThrowLoop()
//    {
//        if (throwRoutine != null)
//        {
//            StopCoroutine(throwRoutine);
//            throwRoutine = null;
//        }
//    }

//    private IEnumerator RandomThrowLoop()
//    {
//        _rb.velocity = Vector2.zero;

//        float waitTime = Random.Range(0.5f, 1.5f);
//        yield return new WaitForSeconds(waitTime);

//        throwBall.Raise();

//        throwRoutine = null;

//        yield return new WaitForSeconds(0.5f);

//        _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
//        moveRoutine = StartCoroutine(MovementLoop());
//        hasBall = false;
//    }

//    private IEnumerator MovementLoop()
//    {
//        while (true)
//        {
//            // Choose a random direction: left or right
//            float xDir = Random.Range(0, 2) == 0 ? -1f : 1f;
//            Vector2 moveDir = new Vector2(xDir, 0f);

//            _rb.velocity = moveDir * moveSpeed;

//            yield return new WaitForSeconds(Random.Range(1f, 2f));
//        }
//    }

//    public void StartMovementLoop()
//    {
//        moveRoutine = StartCoroutine(MovementLoop());
//    }
//}
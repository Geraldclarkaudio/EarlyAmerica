using System.Collections;
using UnityEngine;
using PaperKiteStudio.Dangers;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameEvent throwBall;
    [SerializeField] private Animator _animator;
    [SerializeField] private float moveSpeed = 25f;
    //[SerializeField] private float moveDuration = 1.5f;
    [SerializeField] private LayerMask teammateLayer;
    [SerializeField] private float avoidRadius = 1f;
    [SerializeField] private Rigidbody2D _rb;

    private Coroutine throwRoutine;
    private Coroutine moveRoutine;
    private bool isThrowing = false;

    private void OnEnable()
    {
        moveRoutine = StartCoroutine(MovementLoop());
    }

    private void OnDisable()
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        if (throwRoutine != null) StopCoroutine(throwRoutine);
    }



    public void StartRandomThrowLoop()
    {
        if (throwRoutine != null) StopCoroutine(throwRoutine);

        throwRoutine = StartCoroutine(RandomThrowLoop());
    }

    private IEnumerator RandomThrowLoop()
    {
        isThrowing = true;
        _rb.velocity = Vector2.zero;

        float waitTime = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(waitTime);

        throwBall.Raise();
        PlayThrowAnimation();

        throwRoutine = null;

        yield return new WaitForSeconds(0.5f);
        isThrowing = false;
    }

    private IEnumerator MovementLoop()
    {
        while (true)
        {
            if (!isThrowing)
            {
                Vector2 randomDir = Random.insideUnitCircle.normalized;

                // Avoid teammates
                Collider2D hit = Physics2D.OverlapCircle(transform.position + (Vector3)randomDir * avoidRadius, avoidRadius, teammateLayer);
                if (hit == null)
                {
                    _rb.velocity = randomDir * moveSpeed;
                }
                else
                {
                    _rb.velocity = Vector2.zero;
                }
            }
            else
            {
                _rb.velocity = Vector2.zero;
            }

            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }

    void PlayThrowAnimation()
    {
        _animator.SetTrigger("Throw");
    }
}
using System.Collections;
using UnityEngine;
using PaperKiteStudio.Dangers;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameEvent throwBall;
    [SerializeField] private Animator _animator;
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private LayerMask teammateLayer;
    [SerializeField] private float avoidRadius = 1f;
    [SerializeField] private Rigidbody2D _rb;

    private Coroutine throwRoutine;
    private Coroutine moveRoutine;

    [SerializeField] private bool hasBall = false;

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
        //isThrowing = true;

        _rb.velocity = Vector2.zero;

        float waitTime = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(waitTime);

        throwBall.Raise();
        PlayThrowAnimation();

        throwRoutine = null;

        yield return new WaitForSeconds(0.5f);

        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        moveRoutine = StartCoroutine(MovementLoop());
        hasBall = false;
    }



    private IEnumerator MovementLoop()
    {
        while (true)
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

            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }



    void PlayThrowAnimation()
    {
        _animator.SetTrigger("Throw");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, avoidRadius);
    }
}
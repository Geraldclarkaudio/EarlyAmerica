using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Test : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float moveSpeed = 10f;
    private Rigidbody2D _rb;
    private Vector3 originalScale;
    private Vector2 _movement;
    public bool _isDucking;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        _movement = new Vector2(moveX, moveY);


        if (moveX != 0)
            transform.localScale = new Vector3(originalScale.x * Mathf.Sign(moveX), originalScale.y, originalScale.z);

        // Animation triggers
        _animator.SetBool("isWalking", moveX != 0);

        //if (Input.GetKeyDown(KeyCode.Space))
        //    _animator.SetBool("isDucking", true);
        //else
        //    _animator.SetBool("isDucking", false);

        if (Input.GetKeyDown(KeyCode.F))
            _animator.SetBool("isThrowing", true);
        else
            _animator.SetBool("isThrowing", false);

        // Get animation bools
        _isDucking = _animator.GetBool("isDucking");
    }

    void FixedUpdate()
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        bool isInDuckingState = stateInfo.IsName("Duck");

        if (!isInDuckingState)
            _rb.velocity = _movement.normalized * moveSpeed;
        else
            _rb.velocity = Vector2.zero;
    }
}
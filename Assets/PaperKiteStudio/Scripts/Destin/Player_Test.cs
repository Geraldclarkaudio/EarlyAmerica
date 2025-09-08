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

        // Animation trigger
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
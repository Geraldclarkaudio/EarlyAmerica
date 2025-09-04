using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Test : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetBool("isDucking", true);
        }

        else
        {
            _animator.SetBool("isDucking", false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _animator.SetBool("isThrowing", true);
        }

        else
        {
            _animator.SetBool("isThrowing", false);
        }
    }
}

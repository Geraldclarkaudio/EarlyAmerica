using PaperKiteStudio.Dangers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveImpress : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private DialogueManager _dialogueManager;

    [SerializeField]
    private bool canMove;


    private void Start()
    {
        //_dialogueManager = FindAnyObjectByType<DialogueManager>();
    }
    void Update()
    {
        //if (_dialogueManager.dialogueIsActive)
        //{
        //    canMove = false;
        //}
        //else
        //{
        //    canMove = true;
        //}

        if (canMove)
        {
            float hor = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");

            Vector2 dir = new Vector2(hor, vert);

            if (dir.sqrMagnitude > 0.001f)
            {
                // Normalize to prevent faster diagonal movement
                dir.Normalize();

                // Move the object
                transform.Translate(dir * _speed * Time.deltaTime, Space.World);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                //and made it home
            }
        }
    }

}

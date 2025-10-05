using System.Collections;
using System.Collections.Generic;
using PaperKiteStudio.Dangers;
using UnityEngine;

public class henryhelp : MonoBehaviour
{
    private GameStateMachine gameStateMachine;

    private void Start()
    {
        gameStateMachine = FindAnyObjectByType<GameStateMachine>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameStateMachine.SetState(GameStateMachine.GameState.Playing);
        }
    }
}

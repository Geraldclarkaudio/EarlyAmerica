using System.Collections;
using System.Collections.Generic;
using PaperKiteStudio.Dangers;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Embargo_Manager : BaseGameManager
    {
        private GamePhaseManager gamePhaseManager;

        private void Start()
        {
            gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
        }

        protected override void HandleStateChange(GameStateMachine.GameState state)
        {
            switch (state)
            {
                case GameStateMachine.GameState.Playing:
                    break;

                case GameStateMachine.GameState.Paused:
                    break;
                case GameStateMachine.GameState.Win:
                    break;
                case GameStateMachine.GameState.Lose:
                    break;
                case GameStateMachine.GameState.AdvanceRound:
                    gamePhaseManager.IncrementPhaseStep();
                    break;
            }
        }
    }
}

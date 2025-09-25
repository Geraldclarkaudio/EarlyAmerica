using System.Collections;
using System.Collections.Generic;
using PaperKiteStudio.Dangers;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Embargo_Manager : BaseGameManager
    {
        private GamePhaseManager gamePhaseManager;
        [SerializeField] private ScoreManager scoreManager;

        private void Start()
        {
            gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
            scoreManager.ScoreDepleted += HandleScoreDepleted;

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
                    Debug.Log("Lost game");
                    break;
                case GameStateMachine.GameState.AdvanceRound:
                    gamePhaseManager.IncrementPhaseStep();
                    break;
            }
        }
        private void HandleScoreDepleted()
        {
            Debug.Log("Score depleted — triggering Lose state.");
            stateMachine.SetState(GameStateMachine.GameState.Lose);
        }


    }
}

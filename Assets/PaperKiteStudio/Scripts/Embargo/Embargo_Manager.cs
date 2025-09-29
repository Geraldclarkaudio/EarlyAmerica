using System;
using System.Collections;
using System.Collections.Generic;
using PaperKiteStudio.Dangers;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Embargo_Manager : BaseGameManager
    {
        private GamePhaseManager gamePhaseManager;
        [SerializeField] private Embargo_Dialog dialog;
        [SerializeField] private ScoreManager scoreManager;

        private void Start()
        {
            gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
            //scoreManager.ScoreDepleted += HandleScoreDepleted;
        }

        protected override void HandleStateChange(GameStateMachine.GameState state)
        {
            switch (state)
            {
                case GameStateMachine.GameState.AdvanceRound:
                    gamePhaseManager.IncrementPhaseStep();
                    break;
            }
        }
        //private void HandleScoreDepleted()
        //{
        //    stateMachine.SetState(GameStateMachine.GameState.Lose);
        //}


    }
}

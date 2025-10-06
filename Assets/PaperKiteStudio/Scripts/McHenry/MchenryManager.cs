using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class MchenryManager : BaseGameManager
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
                case GameStateMachine.GameState.AdvanceRound:
                    gamePhaseManager.IncrementGamePhase();
                    break;
            }
        }
    }
}

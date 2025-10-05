using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Ghent_Guy : MovingObject
    {
        protected override void HandleGameStateChanged(GameStateMachine.GameState newState)
        {
            switch (newState)
            {
                case GameStateMachine.GameState.Paused:
                    isMoving = false;
                    Debug.Log("movement stopped");
                    break;
            }
        }
    }
}

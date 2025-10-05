using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Ghent_Manager : MonoBehaviour
    {
        public bool hasMessage;

        private void OnEnable()
        {
            GameStateMachine.OnStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            GameStateMachine.OnStateChanged -= HandleGameStateChanged;
        }

        public void SetHasMessage(bool status)
        {
            hasMessage = status;
        }

        private void HandleGameStateChanged(GameStateMachine.GameState newState)
        {
            switch (newState)
            {
                case GameStateMachine.GameState.Paused:
                    Debug.Log("start delivered dialog");
                    break;
            }
        }
    }
}
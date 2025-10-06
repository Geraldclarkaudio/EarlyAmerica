using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class MchenryDialog : MonoBehaviour
    {
        private DialogueManager _dialogueManager;
        private GamePhaseManager _gamePhaseManager;

        private void OnEnable()
        {
            GameStateMachine.OnStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            GameStateMachine.OnStateChanged -= HandleGameStateChanged;
        }

        private void Start()
        {
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
        }

        private void HandleGameStateChanged(GameStateMachine.GameState newState)
        {
            switch (newState)
            {
                case GameStateMachine.GameState.Win:
                    StartWinDialog();
                    break;
                case GameStateMachine.GameState.Lose:
                    Debug.Log("Startin lose dialog");
                    StartLoseDialog();
                    break;
            }
        }

        public void StartWinDialog()
        {
            switch (_gamePhaseManager.GetPhaseStep())
            {
                case 1:
                    _dialogueManager.dialogueIndex = 38;
                    break;
                //case 2:
                //    _dialogueManager.dialogueIndex = 28;

                //    break;
                //case 3:
                //    _dialogueManager.dialogueIndex = 30;
                //    break;
            }

            _dialogueManager.StartDialogue();
        }

        public void StartLoseDialog()
        {
            switch (_gamePhaseManager.GetPhaseStep())
            {
                case 1:
                    _dialogueManager.dialogueIndex = 37;
                    break;
                //case 2:
                //    _dialogueManager.dialogueIndex = 29;

                //    break;
                //case 3:
                //    _dialogueManager.dialogueIndex = 31;
                //    break;
            }

            _dialogueManager.StartDialogue();
        }
    }
}

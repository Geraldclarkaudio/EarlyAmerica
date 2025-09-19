using System;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class XYZ_Dialog : MonoBehaviour
    {
        private DialogueManager _dialogueManager;
        private GamePhaseManager _gamePhaseManager;

        private void Start()
        {
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
        }
        public void StartWinDialog()
        {
            switch (_gamePhaseManager.GetPhaseStep())
            {
                case 1: // good job you beat round 1
                    _dialogueManager.dialogueIndex = 13;
                    break;
                case 2: // you beat round 2 
                    _dialogueManager.dialogueIndex = 14;

                    break;
                case 3: // you beat round 3
                    _dialogueManager.dialogueIndex = 15;
                    break;
            }

            _dialogueManager.StartDialogue();
        }

        public void StartLoseDialog()
        {
            switch (_gamePhaseManager.GetPhaseStep())
            {
                case 1: // good job you lost round 1
                    _dialogueManager.dialogueIndex = 13;
                    break;
                case 2: // you lost round 2 
                    _dialogueManager.dialogueIndex = 14;

                    break;
                case 3: // you lost round 3
                    _dialogueManager.dialogueIndex = 15;
                    break;
            }

            _dialogueManager.StartDialogue();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class DodgeBallManager : MonoBehaviour
    {
        [SerializeField] GameEvent playingEvent;
        [SerializeField] GameEvent winGameEvent;
        [SerializeField] GameEvent loseGameEvent;
        [SerializeField] GameEvent pauseEvent;
        [SerializeField] DodgeballUI dodgeballUI;

        private DialogueManager _dialogueManger;
        private GamePhaseManager _gamePhaseManager;

        public enum GameState // only controls whether the timer is going or not right now.. 
         {
                PreGame,
                Playing,
                WinGame,
                LoseGame,
                Paused
         }

        public GameState gameState;

        private void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
            _dialogueManger = FindAnyObjectByType<DialogueManager>();

            gameState = GameState.PreGame;
        }

        public void WinGame()
        {
            gameState = GameState.WinGame;

            switch (_gamePhaseManager._phaseStep)
            {
                case 1:

                    _dialogueManger.dialogueIndex = 6;
                    break;
                case 2:
                    _dialogueManger.dialogueIndex = 10;
                    break;
                case 3:
                    _dialogueManger.dialogueIndex = 11;
                    break;
            }
            _dialogueManger.StartDialogue();
        }
        public void LoseGame()
        {
            Debug.Log("lose game called");
            gameState = GameState.LoseGame;
            _dialogueManger.dialogueIndex = 7;
            _dialogueManger.StartDialogue();
        }
        public void Playing()
        {
            gameState = GameState.Playing;
        }
        public void Paused()
        {
            gameState = GameState.Paused;
        }

        public void EndGame()
        {
            if (dodgeballUI.neutrality > 0)
            {
                WinGame();
            }

            else
            {
                LoseGame();
            }
        }

        public void AdvanceRound()
        {
            if (gameState == GameState.WinGame)
            {
                //int currentPhase = _gamePhaseManager._phaseStep;
                //int step = currentPhase + 1;
                //_gamePhaseManager.SetPhaseStep(step);
                _gamePhaseManager.IncrementPhaseStep();
                gameState = GameState.PreGame;
            }

            if (gameState == GameState.LoseGame)
            {
                gameState = GameState.PreGame;
            }

            //else
            //{
            //    _gamePhaseManager.IncrementGamePhase();
            //}
        }

        public void StartIntroDialog()
        {
            Debug.Log("Start intro dialog");
            _dialogueManger.dialogueIndex = 8;
            _dialogueManger.StartDialogue();
            Debug.Log("Dialog should be started");

            switch (_gamePhaseManager._phaseStep)
            {
                case 1:
                    break;
                case 2:
                    _dialogueManger.dialogueIndex = 8;
                    _dialogueManger.StartDialogue();
                    break;
                case 3:
                    _dialogueManger.dialogueIndex = 9;
                    _dialogueManger.StartDialogue();
                    break;
            }
        }
    }
}
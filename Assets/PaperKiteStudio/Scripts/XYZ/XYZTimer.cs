using PaperKiteStudio.Dangers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class XYZTimer : MonoBehaviour
    {
        [SerializeField]
        private DialogueManager _dialogueManager;
        [SerializeField]
        private GamePhaseManager _gamePhaseManager;
        [SerializeField]
        private bool canTick; // ensures time out conditions only happen one time. 
        [SerializeField]
        private float _timer;
        [SerializeField]
        private TMP_Text _timerText;


        public static event Action onTimeOut;

        private void Start()
        {
            canTick = true;
            //set timer to 60 or however many seconds we want the player to play the game. 
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
            _timer = 20f;
        }

        private void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                return;
            }
            if (canTick)
            {
                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                    UpdateClock(_timer);
                }
                if (_timer <= 0)
                {
                    WinRound();
                    BeginNewDialogue();

                    _gamePhaseManager.IncrementPhaseStep();
                }
            }
        }

        private void BeginNewDialogue() // the newly triggered dialogue will have an end dialogue event that reloads the scene.. 
        {
            if (_gamePhaseManager.GetGamePhase() == _gamePhaseManager.GetTempPhase())
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
        }

        private void WinRound()
        {
            _timer = 0;
            _timerText.text = "0:00";
            onTimeOut?.Invoke();

            canTick = false;
        }

        private void UpdateClock(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public void ResetTimer()
        {
            _timer = 20f;
            canTick = true;
        }
    }
}
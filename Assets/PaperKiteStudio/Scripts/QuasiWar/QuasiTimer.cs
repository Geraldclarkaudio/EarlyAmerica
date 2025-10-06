using System;
using TMPro;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class QuasiTimer : MonoBehaviour
    {
        //[SerializeField] GameStateMachine stateMachine;
        [SerializeField]
        private float _timer;
        [SerializeField]
        private TMP_Text _timerText;

        [SerializeField]
        private GameObject _cutScene;
        [SerializeField]
        private DialogueManager _dialogueManager;

        private bool timerStopped;

        private void Start()
        {
            timerStopped = false;
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _timer = 60f;
        }

        public void StopTimer()
        {
            timerStopped = true;
        }

        private void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                return;
            }

            if(timerStopped == false)
            {
                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                    UpdateClock(_timer);
                }
                if (_timer <= 0)
                {
                    _timer = 0;
                    WinRound();
                    _cutScene.SetActive(true);
                }
            }
                
        }

        private void WinRound()
        {
            _timerText.text = "0:00";
        }


        private void UpdateClock(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
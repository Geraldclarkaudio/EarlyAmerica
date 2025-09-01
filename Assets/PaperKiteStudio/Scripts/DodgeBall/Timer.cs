using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private DodgeBallManager _dodgeBallManager;
        [SerializeField]
        private float _timer;
        [SerializeField]
        private TMP_Text _timerText;

        private void Start()
        {
            //set timer to 60 or however many seconds we want the player to play the game. 

            _timer = 60f;
        }

        private void Update()
        {
            if(_dodgeBallManager.gameState != DodgeBallManager.GameState.Playing)
            {
                return;
            }

            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
                UpdateClock(_timer);
            }
            if (_timer <= 0)
            {
                //stop game calculate score

                _timer = 0;
                _timerText.text = "0:00";
            }
        }

        private void UpdateClock(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
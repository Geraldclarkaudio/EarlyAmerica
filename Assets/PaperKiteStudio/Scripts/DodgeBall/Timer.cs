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
        [SerializeField] DodgeballUI dodgeballUI;
        [SerializeField] BallPoolV2 ballPool;
        [SerializeField]
        private float _timer;
        [SerializeField]
        private TMP_Text _timerText;
        [SerializeField] GameEvent roundEnd;
        [SerializeField] Enemy french;
        [SerializeField] Enemy british;
        [SerializeField] Audience audience;

        private bool _isPlaying = false;

        private void Start()
        {
            //set timer to 60 or however many seconds we want the player to play the game. 

            _timer = 30f;
        }

        private void Update()
        {
            if (_dodgeBallManager.gameState == DodgeBallManager.GameState.PreGame)
                _timer = 30f;

            if (_isPlaying)
            {
                if (_dodgeBallManager.gameState != DodgeBallManager.GameState.Playing)
                {
                    french.StopAllCoroutines();
                    british.StopAllCoroutines();
                    audience.StopAllCoroutines();
                    return;
                }

                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                    UpdateClock(_timer);
                }
                if (_timer <= 0)
                {
                    _timer = 0;
                    _timerText.text = "0:00";

                    if (dodgeballUI.neutrality >  0)
                    {
                        _dodgeBallManager.gameState = DodgeBallManager.GameState.WinGame;
                    }

                    else if (dodgeballUI.neutrality <= 0)
                    {
                        _dodgeBallManager.gameState = DodgeBallManager.GameState.LoseGame;
                    }

                    _dodgeBallManager.EndGame();
                    roundEnd.Raise();
                    ballPool.ResetPool();
                }
            }
        }

        private void UpdateClock(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        public void StartTimer()
        {
            _isPlaying = true;
        }
    }
}
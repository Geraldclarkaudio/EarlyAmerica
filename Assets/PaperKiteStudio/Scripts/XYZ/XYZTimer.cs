using TMPro;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class XYZTimer : MonoBehaviour
    {
        [SerializeField] GameStateMachine stateMachine;
        [SerializeField]
        private float _timer;
        [SerializeField]
        private TMP_Text _timerText;

        [SerializeField] XYZ_Manager_V2 manager;

        private void Start()
        {
            _timer = 20f;
        }

        private void Update()
        {
            if (stateMachine.CurrentState == GameStateMachine.GameState.Playing)
            {
                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                    UpdateClock(_timer);
                }
                if (_timer <= 0)
                {
                    ResetTimer();
                    EndRound();
                    stateMachine.SetState(GameStateMachine.GameState.Win);
                }
            }
        }

        public void EndRound()
        {
            _timerText.text = "0:00";
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
        }
    }
}
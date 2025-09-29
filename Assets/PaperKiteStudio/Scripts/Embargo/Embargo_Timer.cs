using System.Collections;
using TMPro;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Embargo_Timer : MonoBehaviour
    {

        [SerializeField] GameStateMachine stateMachine;
        [SerializeField] private TMP_Text _timerText;

        public float originalTimerDuration;
        private float timerDuration;

        private Coroutine timerRoutine;

        private void OnEnable()
        {
            GameStateMachine.OnStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            GameStateMachine.OnStateChanged -= HandleGameStateChanged;
        }

        private void HandleGameStateChanged(GameStateMachine.GameState newState)
        {
            if (newState == GameStateMachine.GameState.Playing)
            {
                StartTimer();
            }

            if (newState == GameStateMachine.GameState.Lose)
            {
                StopTimer();
            }
        }

        public void StartTimer()
        {
            if (timerRoutine != null) StopCoroutine(timerRoutine);
            timerRoutine = StartCoroutine(TimerCountdown());
        }

        private IEnumerator TimerCountdown()
        {
            timerDuration = originalTimerDuration;
            while (timerDuration > 0f)
            {
                timerDuration -= Time.deltaTime;
                UpdateClock(timerDuration);
                yield return null;
            }

            _timerText.text = "0:00";
            stateMachine.SetState(GameStateMachine.GameState.Win);
        }

        public void StopTimer()
        {
            if (timerRoutine != null)
            {
                StopCoroutine(timerRoutine);
                timerRoutine = null;
            }

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

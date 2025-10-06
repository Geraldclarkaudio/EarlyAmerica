using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class TimedEffect : MonoBehaviour
    {
        private Coroutine _activeRoutine;

        public void Play(float duration)
        {
            // If already running, stop and reset
            if (_activeRoutine != null)
            {
                StopCoroutine(_activeRoutine);
                gameObject.SetActive(false);
            }

            gameObject.SetActive(true);
            Camera.main.DOShakePosition(0.25f, 0.25f, 20, 45, true, ShakeRandomnessMode.Full);
            _activeRoutine = StartCoroutine(DisableAfter(duration));
        }

        private IEnumerator DisableAfter(float duration)
        {
            yield return new WaitForSeconds(duration);
            gameObject.SetActive(false);
            _activeRoutine = null;
        }
    }
}
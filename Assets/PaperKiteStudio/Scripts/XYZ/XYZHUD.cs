using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
namespace PaperKiteStudio.Dangers
{
    public class XYZHUD : MonoBehaviour
    {
        [SerializeField] GameStateMachine stateMachine;
        [SerializeField] int _coinAmount;
        [SerializeField] int roundStartCoinAmount;
        [SerializeField] private TMP_Text _coinText;
        [SerializeField] private RectTransform _promptPanel;
        [SerializeField] private Sprite _paidSprite;
        [SerializeField] private Sprite _blockedSprite;
        [SerializeField] private float _screenShakeStrength;
        [SerializeField] private RectTransform _roundPanel;
        [SerializeField] private TMP_Text _roundText;

        public UnityEvent _loadSceneEvent;

        private void OnEnable()
        {
            GameStateMachine.OnStateChanged += HandleStateChange;
        }

        private void OnDisable()
        {
            GameStateMachine.OnStateChanged -= HandleStateChange;
        }

        private void Start()
        {
            UpdateCoinUI();
        }

        private void HandleStateChange(GameStateMachine.GameState newState)
        {
            switch (newState)
            {
                case GameStateMachine.GameState.Paused:
                    UpdateCoinAmount();
                    DisplayPaidPrompt();
                    break;
            }
        }

        #region PROMPT
        public void DisplayPaidPrompt()
        {
            _promptPanel.DOScale(1, 0.5f).OnComplete(() =>
            {
                StartCoroutine(DisablePrompt());
            });
        }

        IEnumerator DisablePrompt()
        {
            yield return new WaitForSeconds(1.0f);
            _promptPanel.DOScale(0, 0.5f);
            if(_coinAmount > 0)
            {
                if (stateMachine.Is(GameStateMachine.GameState.Paused))
                    stateMachine.SetState(GameStateMachine.GameState.Playing);
            }
        }
        #endregion

        public void UpdateCoinAmount()
        {
            _coinAmount--;

            if (_coinAmount <= 0)
            {
                stateMachine.SetState(GameStateMachine.GameState.Lose); // lose in the hud.. eh! 
                Debug.Log("Lost");
                //_gameOverPanel.SetActive(true); // animate with DG.Twweening eventually
            }
            if (!stateMachine.Is(GameStateMachine.GameState.Lose))
            {
                UpdateCoinUI();
            }
            Camera.main.DOShakePosition(0.15f, _screenShakeStrength, 10, 90, true, ShakeRandomnessMode.Full);
        }

        public void UpdateCoinUI()
        {
            _coinText.text = _coinAmount.ToString();
            Debug.Log("coin ui updated");
        }

        public void SetRoundStartCoinAmount()
        {
            roundStartCoinAmount = _coinAmount;
            UpdateCoinUI();
        }

        public void SetCoinAmount()
        {
            _coinText.text = 0.ToString();
            Debug.Log("coin text set to 0");
            _coinAmount = roundStartCoinAmount;
        }
    }
}
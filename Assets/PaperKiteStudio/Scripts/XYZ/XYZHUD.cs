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
        [SerializeField]
        int _coinAmount;
        [SerializeField]
        int roundStartCoinAmount;
        public UnityEvent _loadSceneEvent;
        [SerializeField]
        private TMP_Text _coinText;
        [SerializeField]
        private RectTransform _promptPanel;
        [SerializeField]
        private Sprite _paidSprite;
        [SerializeField]
        private Sprite _blockedSprite;
        [SerializeField]
        private float _screenShakeStrength;
        [SerializeField]
        private RectTransform _roundPanel;
        [SerializeField]
        private TMP_Text _roundText;

        private void Start()
        {
            UpdateCoinUI();
        }

        #region PROMPT
        //public void DisplayRoundPanel() //displays after dialogue to initiate spawning. 
        //{
        //    _roundPanel.DOScale(1, 0.5f).OnComplete(() => {
        //        StartCoroutine(DisableRoundPrompt());
        //    });
        //}
        public void DisplayPaidPrompt()
        {
            _promptPanel.DOScale(1, 0.5f).OnComplete(() =>
            {
                StartCoroutine(DisablePrompt());
            });
        }
        //IEnumerator DisableRoundPrompt()
        //{
        //    yield return new WaitForSeconds(1.0f);
        //    _roundPanel.DOScale(0, 0.5f);
        //    stateMachine.SetState(GameStateMachine.GameState.Playing);
        //}
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
                stateMachine.SetState(GameStateMachine.GameState.Lose);
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
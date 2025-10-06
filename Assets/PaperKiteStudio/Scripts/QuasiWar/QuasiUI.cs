using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class QuasiUI : MonoBehaviour
    {
        [SerializeField]
        private int _tradeAmount;
        [SerializeField]
        private TMP_Text _tradeAmountText;
        [SerializeField]
        private GameObject _gameOverPanel;
        [SerializeField]
        private RectTransform _roundPanel;
        [SerializeField]
        private RectTransform _promptPanel;
        [SerializeField]
        private QuasiTimer _timer;
        private void Start()
        {
            EnemyShip.onHitPlayer += DisplayGameOver;
            CannonBall.onHitPlayer += DisplayGameOver;
        }
        private void OnDisable()
        {
            EnemyShip.onHitPlayer -= DisplayGameOver;
            CannonBall.onHitPlayer-= DisplayGameOver;
        }
        private void DisplayGameOver()
        {
            _gameOverPanel.SetActive(true);
            _timer.StopTimer();
        }

        public void DisplayWinPrompt() // displays when tribute is paid. 
        {
            _promptPanel.DOScale(1, 0.5f).OnComplete(() =>
            {
                //wait 1 seconds. Disappear
                StartCoroutine(DisablePrompt());
            });
        }
        IEnumerator DisablePrompt()
        {
            yield return new WaitForSeconds(1.0f);
            _promptPanel.DOScale(0, 0.5f);
            //start the scene over with more than 1 trade. 
        }
        public void WinRound()
        {
            _tradeAmount++;

            if (_tradeAmount == 3)
            {
                //completed this mini game can move to next level.
            }
        }
        private void UpdateTradeAmount()
        {
            _tradeAmountText.text = _tradeAmount.ToString();
        }
    }
}
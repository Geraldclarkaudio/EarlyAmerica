using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
namespace PaperKiteStudio.Dangers
{
    public class XYZHUD : MonoBehaviour
    {
        [SerializeField]
        int _coinAmount;
        public UnityEvent _loadSceneEvent;
        [SerializeField]
        private TMP_Text _coinText;
        public static event Action oncoinDeplete;
        [SerializeField]
        private GameObject _gameOverPanel;
        [SerializeField]
        private RectTransform _promptPanel;
        [SerializeField]
        private Sprite _paidSprite;
        [SerializeField]
        private Sprite _blockedSprite;

        [SerializeField]
        private XYZSpawner _spawner;

        [SerializeField]
        private float _screenShakeStrength;

        [SerializeField]
        private RectTransform _roundPanel;
        [SerializeField]
        private TMP_Text _roundText;

        public static event Action SetPlaying;

        private void Start()
        {
            UpdateCoinUI();
            //XYZ_V2.onSteal += UpdateCoinAmount;
            XYZ.onSteal += UpdateCoinAmount;
            XYZ_V2.onSteal += UpdateCoinAmount;
            XYZ.DisplayTribute += DisplayPaidPrompt;
            XYZ_V2.DisplayTribute += DisplayPaidPrompt;
        }
        private void OnDisable()
        {
            //XYZ_V2.onSteal -= UpdateCoinAmount;
            XYZ.onSteal -= UpdateCoinAmount;
            XYZ_V2.onSteal -= UpdateCoinAmount;
            XYZ.DisplayTribute -= DisplayPaidPrompt;
            XYZ_V2.DisplayTribute -= DisplayPaidPrompt;
        }

        #region PROMPT
        public void DisplayRoundPanel() //displays after dialogue to initiate spawning. 
        {
            _roundPanel.DOScale(1, 0.5f).OnComplete(() => {
                StartCoroutine(DisableRoundPrompt());
            });
        }
        public void DisplayPaidPrompt() // displays when tribute is paid. 
        {
            _spawner.StopSpawn();
            _spawner.SetCanSpawnFalse();
            _promptPanel.DOScale(1, 0.5f).OnComplete(() =>
            {
                //wait 1 seconds. Disappear
                StartCoroutine(DisablePrompt());
            });
        }
        IEnumerator DisableRoundPrompt()
        {
            yield return new WaitForSeconds(1.0f);
            _roundPanel.DOScale(0, 0.5f);
            _spawner.SetCanSpawnTrue(); // begins spawning enemies
        }
        IEnumerator DisablePrompt()
        {
            yield return new WaitForSeconds(1.0f);
            _promptPanel.DOScale(0, 0.5f);
            if(_coinAmount > 0)
            {
                _spawner.SetCanSpawnTrue();
                SetPlaying?.Invoke();
            }
        }
        #endregion 

        private void UpdateCoinAmount()
        {
            _coinAmount--;
            
            if(_coinAmount <= 0)
            {
                //game over; start over. 
                oncoinDeplete?.Invoke();
                _gameOverPanel.SetActive(true); // animate with DG.Twweening eventually
            }
            //DisplayPaidPrompt();
            UpdateCoinUI();
            Camera.main.DOShakePosition(0.15f, _screenShakeStrength, 10, 90, true, ShakeRandomnessMode.Full);
        }
        private void UpdateCoinUI()
        {
            _coinText.text = _coinAmount.ToString();
        }
    }
}
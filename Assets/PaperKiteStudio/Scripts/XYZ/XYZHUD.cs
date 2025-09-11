using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
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

        private void Start()
        {
            UpdateCoinUI();
            XYZ.onSteal += UpdateCoinAmount;
        }
        private void OnDisable()
        {
            XYZ.onSteal -= UpdateCoinAmount;
        }

        private void UpdateCoinAmount()
        {
            _coinAmount--;
            
            if(_coinAmount <= 0)
            {
                //game over; start over. 
                oncoinDeplete?.Invoke();
                _gameOverPanel.SetActive(true); // animate with DG.Twweening eventually
            }

            UpdateCoinUI();
        }
        private void UpdateCoinUI()
        {
            _coinText.text = _coinAmount.ToString();
        }
    }
}
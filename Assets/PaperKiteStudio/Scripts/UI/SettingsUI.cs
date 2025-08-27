using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
namespace PaperKiteStudio.Dangers
{
    public class SettingsUI : MonoBehaviour
    {

        [SerializeField]
        private bool _ttsOn;
        [SerializeField]
        private Image _toggleButtonImage;
        [SerializeField]
        private Sprite _toggleOnSprite;
        [SerializeField]
        private Sprite _toggleOffSprite;
        [SerializeField]
        private TMP_Text _onOffText;
        
        [SerializeField]
        private CanvasGroup _settingsGroup;
        [SerializeField]
        private RectTransform _generalRect;
        [SerializeField]
        private Button _openSettingsButton;
        [SerializeField]
        private Button _closeSettingsButton;

        // Start is called before the first frame update
        void Start()
        {
            _ttsOn = true; // on by default
            _onOffText.text = "ON";
        }
        public void ToggleTTS()
        {
            if (!_ttsOn) // if its disabled
            {
                _ttsOn = true;
                _toggleButtonImage.sprite = _toggleOnSprite;
                _onOffText.text = "ON";
            }
            else if (_ttsOn) // if its on..
            {
                _ttsOn = false;
                _toggleButtonImage.sprite = _toggleOffSprite;
                _onOffText.text = "OFF";
            }
        }

        public bool GetTTSToggle()
        {
            return _ttsOn;
        }

        public void TurnOffSettings()
        {
            _generalRect.DOAnchorPosX(675, 1.0f);

            _settingsGroup.DOFade(0, 1.0f);
        }
        public void TurnOnSettings()
        {
            _generalRect.DOAnchorPosX(356, 0.1f);
            _settingsGroup.DOFade(1, 1.0f);
        }

        public void OpenSettingsMenu()
        {
            //disable Open settings button and close settings buttons.

            _openSettingsButton.interactable = false;
            _closeSettingsButton.interactable = false;
            
            _settingsGroup.DOFade(1, 1.0f); // fade the canvas group in
            
            _generalRect.DOAnchorPosX(356, 0.1f).OnComplete(() => // when its done
            {
                _closeSettingsButton.interactable = true;
            });
        }

        public void CloseSettingsMenu()
        {
            _openSettingsButton.interactable = false;
            _closeSettingsButton.interactable = false;

            _generalRect.DOAnchorPosX(675, 1.0f).OnComplete(() => // when its done
            {
                _openSettingsButton.interactable = true;
            });
        }
    }
}
using DG.Tweening;
using LoLSDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace PaperKiteStudio.Dangers
{
    public class PopUpDialogue : MonoBehaviour
    {
        [SerializeField]
        private DialogueManager _dialogueManager;
        [SerializeField]
        private float _moveSpeed;
        [Header("Current Panel")]
        [SerializeField]
        private RectTransform _currentPanel;
        [SerializeField]
        private RectTransform _originalTransform;
        [SerializeField]
        private RectTransform _targetTransform;
        [SerializeField]
        private TMP_Text _currentText;
        [SerializeField]
        private string[] _keys;
        [SerializeField]
        private int _keyIndex;

        [Header("GuideBot")]
        [SerializeField]
        private RectTransform _guideBotPanel;
        [SerializeField]
        private RectTransform _GBOG;
        [SerializeField]
        private RectTransform _GBTar;
        [SerializeField]
        private TMP_Text _gbText;
        [Header("Enemy")]
        [SerializeField]
        private RectTransform _enemyPanel;
        [SerializeField]
        private RectTransform _EnOG;
        [SerializeField]
        private RectTransform _EnTar;
        [SerializeField]
        private TMP_Text _enText;
        [Header("Timer Stuff")]
        [SerializeField]
        private float _popUpTimer;
        [SerializeField]
        private float _originalTimer;

        Initializer _init;

        [SerializeField]
        private bool _hasSpawned;
        [SerializeField]
        private int _timeToReset;

        private void Start()
        {
            _init = FindAnyObjectByType<Initializer>();
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _popUpTimer = _originalTimer;
            SetGuideBot();
        }

        private void SetGuideBot()
        {
            _currentPanel = _guideBotPanel;
            _targetTransform = _GBTar;
            _originalTransform = _GBOG;
            _currentText = _gbText;
        }
        private void SetEnemy()
        {
            _currentPanel = _enemyPanel;
            _targetTransform = _EnTar;
            _originalTransform = _EnOG;
            _currentText = _enText;
        }
        private void SwitchCurrentPanel()
        {
            if (_currentPanel == _guideBotPanel)
            {
                SetEnemy();
            }
            else if (_currentPanel == _enemyPanel)
            {
                SetGuideBot();
            }
        }

        private void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                return;
            }
            if (_hasSpawned == false)
            {
                _popUpTimer -= Time.deltaTime;

                if (_popUpTimer <= 0)
                {
                    //assign random key to text and animate pop up 
                    AssignRandomKey();
                    AnimateMovement();
                    _hasSpawned = true;
                    _popUpTimer = _originalTimer;

                }
            }
        }

        private void ResetHasSpawned()
        {
            _hasSpawned = false;
            SwitchCurrentPanel();
        }

        private void AssignRandomKey()
        {
            if(_currentPanel == _enemyPanel)
            {
                _keyIndex = Random.Range(4, 6);
            }
            else if(_currentPanel == _guideBotPanel)
            {
                _keyIndex = Random.Range(0, 3);
            }
            //_keyIndex = Random.Range(0, _availableKeyIndexes.Length);
            _currentText.text = _init.GetText(_keys[_keyIndex]);
            LOLSDK.Instance.SpeakText(_init.GetText(_keys[_keyIndex]));
        }

        private void AnimateMovement()
        {
            _currentPanel.DOAnchorPosX(_targetTransform.anchoredPosition.x, 0.5f).OnComplete(() =>
            {
                DOVirtual.DelayedCall(2f, () =>
                {
                    _currentPanel.DOAnchorPosX(_originalTransform.anchoredPosition.x, 0.5f).OnComplete(() => {
                        Invoke("ResetHasSpawned", _timeToReset);
                    });
                });
            });
        }
    }
}
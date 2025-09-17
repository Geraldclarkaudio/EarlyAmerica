using System;
using UnityEngine;
using DG.Tweening;
namespace PaperKiteStudio.Dangers
{
    public class XYZ : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private GameObject _playerPos;
        [SerializeField]
        private GameObject _originalPosition;
        [SerializeField]
        private GameObject _associatedButton;


        public static event Action onSteal;

        [SerializeField]
        private GameObject[] _buttonPositions;

        private Tween moveTween;
        private Tween scaleTween;

        [SerializeField]
        private GamePhaseManager _gamePhaseManager;
        [SerializeField]
        private DialogueManager _dialogueManager;

        private void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
        }

        private void OnEnable()
        {
            XYZTimer.onTimeOut += StopAgent;

            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _associatedButton.SetActive(true); // random its position
            _associatedButton.transform.position = _buttonPositions[UnityEngine.Random.Range(0, _buttonPositions.Length)].transform.position;
        }
        private void OnDisable()
        {
            XYZTimer.onTimeOut -= StopAgent;
        }

        public void StopAgent()
        {
            moveTween?.Kill();
            scaleTween?.Kill();

            transform.position = _originalPosition.transform.position;
            _associatedButton.SetActive(false); // random its position
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                StopAgent();
            }

            float distanceToPlayer = Vector2.Distance(transform.position, _playerPos.transform.position);
            float duration = 0;

            if (_gamePhaseManager.GetGamePhase() == _gamePhaseManager.GetTempPhase())
            {
                switch (_gamePhaseManager.GetPhaseStep())
                {
                    case 1:
                        duration = 3;
                        break;
                    case 2:
                        duration = 2;
                        break;
                    case 3:
                        duration = 1.5f;
                        break;
                }
            }

            if(moveTween == null || !moveTween.IsActive())
            {
                moveTween = transform.DOMove(_playerPos.transform.position, duration).OnComplete(() =>
                {
                    transform.position = _originalPosition.transform.position;
                    _associatedButton.SetActive(false); // random its position
                    gameObject.SetActive(false);
                    //get points removed. 
                    onSteal?.Invoke();
                });
            }
            if (scaleTween == null || !scaleTween.IsActive())
            {
                transform.DOScale(1, duration);
            }
        }
    }
}
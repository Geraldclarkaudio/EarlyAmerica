using System;
using UnityEngine;
using DG.Tweening;

namespace PaperKiteStudio.Dangers
{
    public class XYZ_V2 : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _playerPos;
        [SerializeField] private GameObject _originalPosition;
        [SerializeField] private GameObject _associatedButton;
        [SerializeField] private GameObject[] _buttonPositions;

        [SerializeField] private GamePhaseManager _gamePhaseManager;
        [SerializeField] private DialogueManager _dialogueManager;

        private Tween moveTween;
        private Tween scaleTween;
        private bool hasStartedTweens = false;

        public static event Action onSteal;
        public static event Action DisplayTribute;

        private void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
        }

        private void OnEnable()
        {
            XYZTimer.onTimeOut += StopAgent;

            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _associatedButton.SetActive(true);
            _associatedButton.transform.position = _buttonPositions[UnityEngine.Random.Range(0, _buttonPositions.Length)].transform.position;

            hasStartedTweens = false;
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
            _associatedButton.SetActive(false);
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                StopAgent();
                return;
            }

            if (_gamePhaseManager.GetGamePhase() != _gamePhaseManager.GetTempPhase()) return;
            if (hasStartedTweens) return;

            float duration = _gamePhaseManager.GetPhaseStep() switch
            {
                1 => 3f,
                2 => 2f,
                3 => 1.5f,
                _ => 0f
            };

            StartTweens(duration);
            hasStartedTweens = true;
        }

        private void StartTweens(float duration)
        {
            moveTween = transform.DOMove(_playerPos.transform.position, duration).OnComplete(() =>
            {
                Debug.Log("Tween completed — invoking onSteal");
                transform.position = _originalPosition.transform.position;
                _associatedButton.SetActive(false);
                DisplayTribute?.Invoke();
                gameObject.SetActive(false);
                onSteal?.Invoke();
            }).SetAutoKill(true);

            scaleTween = transform.DOScale(1, duration).SetAutoKill(true);
        }
    }
}


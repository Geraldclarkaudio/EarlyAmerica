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

        [SerializeField] GameStateMachine stateMachine;
        [SerializeField] private GamePhaseManager _gamePhaseManager;
        [SerializeField] private DialogueManager _dialogueManager;

        private Tween moveTween;
        private Tween scaleTween;
        private bool hasStartedTweens = false;

        private void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
        }

        private void OnEnable()
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _associatedButton.SetActive(true);
            _associatedButton.transform.position = _buttonPositions[UnityEngine.Random.Range(0, _buttonPositions.Length)].transform.position;

            hasStartedTweens = false;
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
                stateMachine.SetState(GameStateMachine.GameState.Paused);
                transform.position = _originalPosition.transform.position;
                _associatedButton.SetActive(false);
                gameObject.SetActive(false);
            }).SetAutoKill(true);

            scaleTween = transform.DOScale(1, duration).SetAutoKill(true);
        }
    }
}


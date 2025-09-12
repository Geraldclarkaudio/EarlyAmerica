using DG.Tweening;
using System;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class EnemyShip : MonoBehaviour
    {
        [SerializeField]
        private QuasiGameManager _quasiGameManager;
        [SerializeField]
        private DialogueManager _dialogueManager;
        [SerializeField]
        private bool _mover;
        [SerializeField]
        private GameObject _playerPos;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _screenShakeStrength;

        public static event Action onHitPlayer;

        [SerializeField]
        private GameObject _projectilePrefab;
        [SerializeField]
        private float _fireTimer;
        [SerializeField]
        private float _originalFireTimer;

        private void Start()
        {
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
            _fireTimer = _originalFireTimer;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                //SEIZE SHIP!
                PlayerShip player = other.GetComponent<PlayerShip>();
                player.DisableMove(); // basically restart the game.. display restart prompt
                Camera.main.DOShakePosition(0.5f, _screenShakeStrength, 10, 90, true, ShakeRandomnessMode.Full);
                onHitPlayer?.Invoke();
                _quasiGameManager.gameOver = true;
            }
        }
        void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                return;
            }
            if (!_quasiGameManager.gameOver)
            {
        
                    if (_mover) //chaser
                    {
                        // Direction from enemy to player
                        Vector2 direction = (_playerPos.transform.position - transform.position).normalized;

                        // Move toward player
                        transform.position += (Vector3)(direction * _speed * Time.deltaTime);

                        // Rotate so Y axis points toward player
                        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(0, 0, -angle);
                    }

                    else // cannon? 
                    {
                        if(_fireTimer> 0)
                        {
                            _fireTimer -= Time.deltaTime;
                        }
                        if (_fireTimer < 0)
                        {
                            _fireTimer = _originalFireTimer;
                        Fire();
                        }
                    }
                
            }
        }

        private void Fire()
        {
            Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        }
    }
}

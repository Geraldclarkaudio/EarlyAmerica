using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class CannonBall : MonoBehaviour
    {
        public enum Direction
        {
            Down,
            Up,
            Left,
            Right
        }
        [SerializeField]
        private Direction _direction;
        [SerializeField]
        private Vector2 _moveDirection;
        [SerializeField]
        private float _speed;
        public static event Action onHitPlayer;

        QuasiGameManager _gameManager;

        [SerializeField]
        private float _activeTimer;
        [SerializeField]
        private float _originalActiveTimer;

        [SerializeField]
        private bool _playerCannonBall;

        private void OnEnable()
        {
            _activeTimer = _originalActiveTimer;
        }
        // Start is called before the first frame update
        void Start()
        { // makes this reusable later? 
            _gameManager = FindAnyObjectByType<QuasiGameManager>();
            switch (_direction)
            {
                case Direction.Down:
                    _moveDirection = Vector2.down; // player
                    break;
                    case Direction.Up:
                    _moveDirection = Vector2.up; // enemy
                    break;
                    case Direction.Left:
                    _moveDirection = Vector2.left;
                    break;
                    case Direction.Right:
                    _moveDirection = Vector2.right;
                    break;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (_playerCannonBall)
                {
                    return;
                }

                PlayerShip player = collision.GetComponent<PlayerShip>();
                player.DisableMove();
                Camera.main.DOShakePosition(0.5f, 1, 10, 90, true, ShakeRandomnessMode.Full);
                onHitPlayer?.Invoke();
                _gameManager.gameOver = true;
            }

            if (collision.CompareTag("Frenchy"))
            {
                if (!_playerCannonBall)
                {
                    return;
                }
                Debug.Log("Hit z' Enemy");
            }
        }

        void Update()
        {
            transform.Translate(_moveDirection * _speed * Time.deltaTime);

            if(_activeTimer > 0)
            {
                _activeTimer -= Time.deltaTime;

                if(_activeTimer <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
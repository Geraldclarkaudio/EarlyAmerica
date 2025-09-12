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

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = FindAnyObjectByType<QuasiGameManager>();
            switch (_direction)
            {
                case Direction.Down:
                    _moveDirection = Vector2.down;
                    break;
                    case Direction.Up:
                    _moveDirection = Vector2.up;
                    break;
                    case Direction.Left:
                    _moveDirection = Vector2.left;
                    break;
                    case Direction.Right:
                    _moveDirection = Vector2.right;
                    break;
            }

            Destroy(gameObject, 5.0f);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerShip player = collision.GetComponent<PlayerShip>();
                player.DisableMove();
                Camera.main.DOShakePosition(0.5f, 1, 10, 90, true, ShakeRandomnessMode.Full);
                onHitPlayer?.Invoke();
                _gameManager.gameOver = true;
            }
        }
        // Update is called once per frame
        void Update()
        {
            transform.Translate(_moveDirection * _speed * Time.deltaTime);
        }
    }
}
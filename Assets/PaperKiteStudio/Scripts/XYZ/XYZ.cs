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

        private void OnEnable()
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _associatedButton.SetActive(true); // random its position
            _associatedButton.transform.position = _buttonPositions[UnityEngine.Random.Range(0, _buttonPositions.Length)].transform.position;
        }

        private void Update()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _playerPos.transform.position);
            float duration = 2;

            transform.DOMove(_playerPos.transform.position, duration).OnComplete(() => 
            {
                transform.position = _originalPosition.transform.position;
                _associatedButton.SetActive(false); // random its position

                gameObject.SetActive(false);
            });
            transform.DOScale(1, duration);


        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{

    public class ImpressmentMiniGameUI : MonoBehaviour
    {
        [SerializeField]
        private Canvas _miniGameCanvas;
        [SerializeField]
        private RectTransform _needle;
        [SerializeField]
        private RectTransform _backgroundImage;
        [SerializeField] 
        private RectTransform _perfectClickZone;

        [SerializeField]
        float _speed;
        [SerializeField]
        private float _minX;
        [SerializeField]
        private float _maxX;
        [SerializeField]
        private bool movingRight;

        public static event Action onPerfectClick;
        public static event Action onMissedClick;

  
        public ImpressmentTriggerZone _currentTriggerZone;

        private void Start()
        {
            _minX = _backgroundImage.anchoredPosition.x - (_backgroundImage.rect.width / 2);
            _maxX = _backgroundImage.anchoredPosition.x + (_backgroundImage.rect.width / 2);
        }

        void MoveNeedle()
        {
            float newX = _needle.anchoredPosition.x + (movingRight ? _speed : -_speed) * Time.deltaTime; // multiply difficulty eventually 

            if (newX > _maxX)
            {
                newX = _maxX;
                movingRight = false;
            }
            else if (newX < _minX)
            {
                newX = _minX;
                movingRight = true;
            }

            _needle.anchoredPosition = new Vector2(newX, _needle.anchoredPosition.y);
        }

        void CheckPerfectClick()
        {
            Vector2 indicatorPosition = _needle.anchoredPosition;
            //define perfect target zone 
            Vector2 perfectCastZonePosition = _perfectClickZone.anchoredPosition;
            Vector2 perfectCastZoneSize = _perfectClickZone.sizeDelta;

            //check whether the indicator is within the target zone
            if (indicatorPosition.x >= perfectCastZonePosition.x - perfectCastZoneSize.x / 2 &&
                indicatorPosition.x <= perfectCastZonePosition.x + perfectCastZoneSize.x)
            {
                // perfect click
                Debug.Log("PERFECT");
                _currentTriggerZone.Complete();
                onPerfectClick?.Invoke();
            }
            else
            {
                //missed click
                Debug.Log("Miss");
                onMissedClick?.Invoke();
                //make dialogue manager pop up with a loss dialogue that reloads the scene. 
            }
        }

        private void Update()
        {
            if (_miniGameCanvas.enabled == true)
            {
                MoveNeedle();

                if (Input.GetMouseButtonDown(0))
                {
                    CheckPerfectClick();
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace PaperKiteStudio.Dangers
{
    public class Prompt : MonoBehaviour
    {
        [SerializeField]
        private Canvas _targetCanvas;
        [SerializeField]
        private RectTransform _panelRect;
        [SerializeField]
        private RectTransform _originalPos;
        [SerializeField]
        private RectTransform _targetPos;
        [SerializeField]
        private SceneLoader _sceneLoader;
        private void Start()
        {
            _panelRect.anchoredPosition = _originalPos.anchoredPosition;
            EnemySight.onCaught += CaughtPrompt;
        }
        private void OnDisable()
        {
            EnemySight.onCaught -= CaughtPrompt;
        }

        private void CaughtPrompt()
        {
            _targetCanvas.enabled = true;
            _panelRect.DOAnchorPosY(_targetPos.anchoredPosition.x, 0.5f);
            _sceneLoader.LoadScene(); // reloads the scene

        }
    }
}
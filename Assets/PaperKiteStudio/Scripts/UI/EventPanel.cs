using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

namespace PaperKiteStudio.Dangers
{
    public class EventPanel : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _thisRectTrans;
        [SerializeField]
        private TMP_Text _text;
        Initializer _initializer;
        [SerializeField]
        private Timeline _timeline;
        private void Awake()
        {
            _initializer = FindAnyObjectByType<Initializer>();
        }
        private void OnEnable()
        {
            //assign hint text to the panel's text component
            _text.text = _initializer.GetText(_timeline.GetCurrentEvent().eventText);
            _thisRectTrans.DOScale(1, 0.25f);
        }

        public void DisablePanel()
        {
            _timeline.ToggleInteractabilityEventButtons(false);

            _thisRectTrans.DOScale(0, 0.25f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}
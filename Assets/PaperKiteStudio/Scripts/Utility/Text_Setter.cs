using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace PaperKiteStudio.Dangers
{
    public class Text_Setter : MonoBehaviour
    {
        [SerializeField]
        private Initializer _init;
        [SerializeField]
        private TMP_Text _textComponent;
        [SerializeField]
        private string _key;

        private void Start()
        {
            _init = FindAnyObjectByType<Initializer>();
            _textComponent.text = _init.GetText(_key);
        }
    }
}
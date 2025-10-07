using LoLSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    
    public class ReadAloudButton : MonoBehaviour
    {
        [SerializeField]
        private Page _associatedPage;

        public void SetCurrentPage(Page currentPage)
        {
            _associatedPage = currentPage;
        }

        public void Speak(int keyIndex)
        {
            LOLSDK.Instance.SpeakText(_associatedPage.updateTextKeys[keyIndex]);
        }
    }
}
using JetBrains.Annotations;
using LoLSDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class ImpressmentTriggerZone : MonoBehaviour
    {
        [SerializeField]
        private Initializer _init;
        [SerializeField]
        private int triggerID;
        [SerializeField]
        private bool completed;

        [SerializeField]
        private Dialogue _dialogue;
        [SerializeField]
        private Canvas _dialogueCanvas;
        [SerializeField]
        private Canvas _miniGameCanvas;
        [SerializeField]
        private string[] _textKeys;
        [SerializeField]
        private TMP_Text _dialogueText;
        [SerializeField]
        ImpressmentMiniGameUI _miniGameUI;
        private void Start()
        {
            _init = FindAnyObjectByType<Initializer>();
            completed = false;
        }

        public void Complete()
        {
            completed = true;
            _dialogueCanvas.enabled = false;
            _miniGameCanvas.enabled = false;
            _miniGameUI._currentTriggerZone = null;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (!completed)
                {
                    PlayerMoveImpress player = collision.GetComponent<PlayerMoveImpress>();
                    player.DisableMovement();
                    _dialogueCanvas.enabled = true; // turn on canvases...
                    _miniGameCanvas.enabled = true;
                    _miniGameUI._currentTriggerZone = this;
        
                    switch (triggerID)
                    {
                        case 1:
                             _dialogueText.text = _init.GetText(_dialogue.key[0]); // set the text to be differnt for each trigger zone
                            break;
                        case 2:
                            _dialogueText.text = _init.GetText(_dialogue.key[1]);
                            break;
                        case 3:
                            _dialogueText.text = _init.GetText(_dialogue.key[2]);
                            break;
                        case 4:
                            _dialogueText.text = _init.GetText(_dialogue.key[3]);
                            break;
                        case 5:
                            _dialogueText.text = _init.GetText(_dialogue.key[4]);
                            break;
                        case 6:
                            _dialogueText.text = _init.GetText(_dialogue.key[5]);
                            break;
                        case 7:
                            _dialogueText.text = _init.GetText(_dialogue.key[6]);
                            break;
                        case 8:
                            _dialogueText.text = _init.GetText(_dialogue.key[7]);
                            break;
                    }

                    LOLSDK.Instance.SpeakText(_dialogueText.text);
                }
            }
        }
    }
}
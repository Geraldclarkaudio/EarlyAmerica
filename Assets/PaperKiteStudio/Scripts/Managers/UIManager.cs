using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

namespace PaperKiteStudio.Dangers
{
    /// <summary>
    /// Needs to handle the state of the UI and disable/enable the appropriate canvas... 
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public enum UIState
        {
            Off, 
            Fade,
            Dialogue, 
            Journal
            //whatevcer other states might exist later.
        }
            
        public UIState state;

        [SerializeField]
        private Canvas _dialogueCanvas;
        [SerializeField]
        private Canvas _fadeCanvas;
        [SerializeField]
        private Canvas _journalCanvas;

        [SerializeField]
        private Image _fadeImage;
        [SerializeField]
        private Button _nextLineButton;

        [SerializeField]
        private DialogueManager _dialogueManager;
        [SerializeField]
        private JournalManager _journalManager;
        [SerializeField]
        private GamePhaseManager _gamePhaseManager;

        public static event Action onOpened;

        private void OnEnable()
        {
            DialogueManager.onBeginDialogue += DialogueUI;
        }
        private void OnDisable()
        {
            DialogueManager.onBeginDialogue -= DialogueUI;
        }
        public UIState GetState()
        {
            return state;
        }

        public void DialogueUI() // maybe this could find all the available buttons and disable them eventually? 
        {
            state = UIState.Dialogue;
        }
        public void FadeOutandLoadScene(int sceneIndex)
        {
            state = UIState.Fade;
            _fadeCanvas.enabled = true;

            _fadeImage.DOFade(1, 3).OnComplete(() =>
            {
                _fadeImage.DOFade(0, 3).OnComplete(() =>
                {
                    _fadeCanvas.enabled = false;
                });
                    SceneManager.LoadScene(sceneIndex);
            });
        }
        public void TurnOffJournal()
        {
            state = UIState.Off;
            _journalCanvas.enabled = false;
        }
        private void Update()
        {
            if (_dialogueManager.dialogueIsActive)
            {
                return; // use to lock up the screen while dialogue is active. 
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                
                if (_gamePhaseManager.GetHasOpenedJournal() == false)
                {
                    _gamePhaseManager.SetHasOpenedJournalComplete();
                    _dialogueManager.dialogueIndex = 41;
                    _dialogueManager.StartDialogue();
                }
                //open journal.. 
                if(state != UIState.Journal)
                {
                    state = UIState.Journal;
                    _journalCanvas.enabled = true;
                    _journalManager.TurnOnJournal();
                    onOpened?.Invoke();
                }
                else
                {
                    state = UIState.Off;
                    _journalCanvas.enabled = false;
                }

            }
        }
    }
}
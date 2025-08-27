using LoLSDK;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PaperKiteStudio.Dangers
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField]
        private Initializer _init;
        [SerializeField]
        private TMP_Text textComponent;
        [SerializeField]
        private Image _iconObject;
        [SerializeField]
        private float canProceed = -1;
        [SerializeField]
        private float textRate = 1f;  // default. adjusts to how long the TTS takes to read it. 

        public Dialogue[] dialogues; // the list of dialogue scriptable objects
        public Dialogue currentDialogue; // the currently used dialogue SO
        public int keyIndex; // which index of the keys for that dialogue
        public int dialogueIndex;//which dialogue asset

        public bool dialogueIsActive;
        public Canvas _thisCanvas;

        [SerializeField]
        private RectTransform _dialogueRectTransform;
        [SerializeField]
        private RectTransform[] _dialoguePanelPositions; // array of potential dialogue box positions in case the dialogue needs to move around. For example: During a tutorial section. 

        [SerializeField]
        private SettingsUI _ttsToggle;
        [SerializeField]
        private Slider _progressSlider;
        [SerializeField]
        private Button _nextButton;

        public static event Action onBeginDialogue;
        public static event Action onEndDialogue;
        public void StartDialogue() // called when button is clicked or specific event happens. 
        {
            onBeginDialogue?.Invoke();

            _thisCanvas.enabled = true;

            if (dialogueIndex < dialogues.Length)
            {
                currentDialogue = dialogues[dialogueIndex];
                keyIndex = 0;//reset keys back to 0
                dialogueIndex = currentDialogue.dialogueID;
                dialogueIsActive = true;
            }

            if (currentDialogue != null)
            {
                if (currentDialogue._animations.Length > 0) // anims? 
                {

                }
                if (currentDialogue._newUIEvents.Length > 0) // ui events? 
                {
                    if (currentDialogue._newUIEvents[keyIndex] != null)
                    {
                        currentDialogue._newUIEvents[keyIndex].Raise();
                    }
                }
                if (currentDialogue._dialoguePositionChanges.Length > 0) // pos changes?
                {
                    if (currentDialogue._dialoguePositionChanges[keyIndex] != null)
                    {
                        currentDialogue._dialoguePositionChanges[keyIndex].Raise();
                    }
                }
                if (currentDialogue._eventTriggers != null) // trigs? 
                {
                    if (currentDialogue._eventTriggers.Length > 0)
                    {
                        if (currentDialogue._eventTriggers[keyIndex] != null)
                        {
                            currentDialogue._eventTriggers[keyIndex].Raise();
                        }
                    }
                }
            }

            textComponent.text = _init.GetText(currentDialogue.key[keyIndex]);
            _iconObject.sprite = currentDialogue.icons[keyIndex];
            textRate = textComponent.text.Length * 0.0667f;
            canProceed = Time.time + textRate;
            _progressSlider.value = 0;
            _progressSlider.maxValue = textRate;

            //tts
            if (_ttsToggle.GetTTSToggle() == true)
            {
                LOLSDK.Instance.SpeakText(currentDialogue.key[keyIndex]);
            }
        }

        public void NextLine()
        {
            if (canProceed < Time.time)
            {
                if (keyIndex < currentDialogue.key.Length - 1) // if the current dialgue asset's key index is less than the length of the array...
                {
                    keyIndex++;

                    if (currentDialogue._animations.Length > 0)
                    {

                    }

                    if (currentDialogue._newUIEvents.Length > 0)
                    {
                        if (currentDialogue._newUIEvents[keyIndex] != null)
                        {
                            currentDialogue._newUIEvents[keyIndex].Raise();
                        }
                    }
                    if (currentDialogue._dialoguePositionChanges.Length > 0) // triple checking null.. why?
                    {
                        if (currentDialogue._dialoguePositionChanges[keyIndex] != null)
                        {
                            currentDialogue._dialoguePositionChanges[keyIndex].Raise();
                        }
                    }
                    if (currentDialogue._eventTriggers != null)
                    {
                        if (currentDialogue._eventTriggers.Length > 0)
                        {
                            if (currentDialogue._eventTriggers[keyIndex] != null)
                            {
                                currentDialogue._eventTriggers[keyIndex].Raise();
                            }
                        }
                    }

                    textComponent.text = _init.GetText(currentDialogue.key[keyIndex]);
                    _iconObject.sprite = currentDialogue.icons[keyIndex];
                    textRate = textComponent.text.Length * 0.0667f;
                    canProceed = Time.time + textRate;
                    _progressSlider.value = 0;
                    _progressSlider.maxValue = textRate;

                    //speak text
                    if (_ttsToggle.GetTTSToggle() == true)
                    {
                        LOLSDK.Instance.SpeakText(currentDialogue.key[keyIndex]);
                    }
                }
                else if (keyIndex >= currentDialogue.key.Length - 1) // at the end of the keys
                {
                    if(currentDialogue._endDialogueEvent != null)
                    {
                        currentDialogue._endDialogueEvent.Raise(); // if something should happen at the end of a dialogue, do that thing
                    }

                    dialogueIndex++; // move to the next dialogue asset for the next time dialogues take place. 
                    dialogueIsActive = false;
                    _thisCanvas.enabled = false;
                    onEndDialogue?.Invoke(); // can cause problems if more than one thing subs to this in each scene..
                }
            }
        }

        public void ChangeDialoguePosition(int position)
        {
            _dialogueRectTransform.anchoredPosition = _dialoguePanelPositions[position].anchoredPosition;
        }

        private void Update()
        {
            if (dialogueIsActive)
            {
                //For testing. Delete later (used to make wait time between dialogue lines 0 so we dont have to wait for TTS length) 
                _progressSlider.value = _progressSlider.maxValue;
                canProceed = 0;


                if (_progressSlider.value < _progressSlider.maxValue)
                {
                    _nextButton.interactable = false;
                    _progressSlider.value += Time.deltaTime;
                }
                else
                {
                    _nextButton.interactable = true;
                }
            }
        }
    }
}

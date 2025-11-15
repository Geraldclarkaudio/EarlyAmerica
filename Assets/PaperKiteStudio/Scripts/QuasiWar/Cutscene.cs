using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace PaperKiteStudio.Dangers
{
    public class Cutscene : MonoBehaviour
    {
        [SerializeField]
        private DialogueManager _dialogueManager;
        [SerializeField]
        private PlayableDirector _cutsceneDirector;
        [SerializeField]
        private GamePhaseManager _gamePhaseManager;
        [SerializeField]
        private RectTransform _journalRect;
        [SerializeField]
        private RectTransform _originalRect;
        [SerializeField]
        private RectTransform _targetRect;

        private void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
        }

        public void BeginDialogue()
        {
            _dialogueManager.dialogueIndex = 18;
            _dialogueManager.StartDialogue();
            _cutsceneDirector.Pause();
        }
        public void CutsceneProceed()
        {
            _cutsceneDirector.Resume();
        }
        public void IndrementGamePhase()
        {
            _gamePhaseManager.IncrementGamePhase();
        }
        public void ShowJournalPrompt()
        {
            _journalRect.DOAnchorPosX(_targetRect.anchoredPosition.x, 0.5f);    
        }
    }
}
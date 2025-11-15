using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace PaperKiteStudio.Dangers
{
    public class sHIPbURNcUT : MonoBehaviour
    {
        DialogueManager _dialogueManager;
        GamePhaseManager _gamePhaseManager;
        [SerializeField]
        private PlayableDirector _shipBurnDirector;

        [SerializeField]
        private RectTransform _journalRect;
        [SerializeField]
        private RectTransform _originalRect;
        [SerializeField]
        private RectTransform _targetRect;
        // Start is called before the first frame update
        void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();    
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
        }

        public void BeginShipBurnDialogue()
        {
            _gamePhaseManager.IncrementGamePhase();
            _dialogueManager.dialogueIndex = 22;
            _dialogueManager.StartDialogue();

            _shipBurnDirector.Pause();
        }
        public void ShipBurnDrectorResume()
        {
            _shipBurnDirector.Resume();
        }

        public void ShowJournalPrompt()
        {
            _journalRect.DOAnchorPosX(_targetRect.anchoredPosition.x, 0.5f);
        }
    }
}
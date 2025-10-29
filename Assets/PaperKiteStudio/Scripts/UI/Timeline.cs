using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace PaperKiteStudio.Dangers
{
    public class Timeline : MonoBehaviour
    {
        [SerializeField]
        private JournalManager journalManager;

        [SerializeField]
        private TimelineEvent _currentEvent;

        [SerializeField]
        private TimelinePage[] _pages;
        [SerializeField]
        private TimelineEvent[] _pageEvents;
        [SerializeField]
        private TimelinePage _currentPage;

        [SerializeField]
        private TimelineSlot[] _slots;
        [SerializeField]
        private TimelineSlot _currentSlot;

        [SerializeField]
        private TMP_Text _pageTitle;
        [SerializeField]
        private Button[] _eventButtons;
        [SerializeField]
        private EventPanel _eventPanel;
        [SerializeField]
        private TMP_Text[] _pageButtonTexts;
        [SerializeField]
        private TMP_Text[] _timelineDates;
        Initializer _init;
        GamePhaseManager _gamePhaseManager;
        [SerializeField]
        private int _numberCorrect;
        [SerializeField]
        private RectTransform _celebratePopUp;
        private void Awake()
        {
            _init = FindAnyObjectByType<Initializer>();
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
        }

        public void CompletePage()
        {
            _gamePhaseManager.SetTimelineComplete(_currentPage.pageID); // saves the page being completed
            _numberCorrect = 0; // rest number 
            _celebratePopUp.DOScale(1, 0.25f).OnComplete( () => 
            {
                StartCoroutine(WaitToScaleDownCelebratePrompt()); // vfx 
            });
        }

        IEnumerator WaitToScaleDownCelebratePrompt()
        {
            yield return new WaitForSeconds(1);
            _celebratePopUp.DOScale(0, 1);
        }
        public void SetNumberCorrect()
        {
            _numberCorrect++;

            if (_numberCorrect == 3)
            {
                CompletePage(); //omg 
            }
        }
        private void OnEnable()
        {
            // populate the timeline buttons and the event buttons with corresponding page...
            // assign associated event to each timeline slot button when the Timeline becomes active depending on the page number you're in
            // for the journal?

            if (journalManager == null)
                return;

            int pageNumber = journalManager.GetPageNumber();

            if (_pages != null && pageNumber >= 0 && pageNumber < _pages.Length)
                _currentPage = _pages[pageNumber];
                 _pageTitle.text = _currentPage.pageTitle;
            //assign text to buttons. 
            //page 1 buttons  = Proclamaiton, Jays, and Farewell etc etc 
            for(int i = 0; i < _currentPage.pageButtonTexts.Length; i++) // loop through page texts
            {
                _pageButtonTexts[i].text = _init.GetText(_currentPage.pageButtonTexts[i]);
            }
            for(int i = 0; i < _currentPage.timelineDates.Length; i++)
            {
                _timelineDates[i].text = _currentPage.timelineDates[i]; // just set the string value.. 
            }
            ///check if this page has been completed. If it has, disable all the buttons and set the slot colors to yellow and put green circles on event buttons. 
            // Defensive checks
            if (_slots == null || _slots.Length == 0)
                return;

            if (_pageEvents == null || _pageEvents.Length == 0)
            {
                // Clear slot associations if there are no events
                for (int i = 0; i < _slots.Length; i++)
                    if (_slots[i] != null)
                        _slots[i].SetAssociatedEvent(null);
                return;
            }

            // Assume events are grouped by page in contiguous blocks where each page has up to _slots.Length events.
            int eventsPerPage = _slots.Length;
            int offset = pageNumber * eventsPerPage;
            int eventsCount = _pageEvents.Length;

            for (int i = 0; i < _slots.Length; i++)
            {
                TimelineEvent associated = (offset + i) < eventsCount && (offset + i) >= 0
                    ? _pageEvents[offset + i]
                    : null;

                if (_slots[i] != null)
                    _slots[i].SetAssociatedEvent(associated);
            }

            CheckCurrentPageCompletion();
        }
        private void CheckCurrentPageCompletion()
        {
            bool isCompleted = false;

            switch (_currentPage.pageID)
            {
                case 1: isCompleted = _gamePhaseManager.timeline1Completed; break;
                case 2: isCompleted = _gamePhaseManager.timeline2Completed; break;
                case 3: isCompleted = _gamePhaseManager.timeline3Completed; break;
                case 4: isCompleted = _gamePhaseManager.timeline4Completed; break;
                case 5: isCompleted = _gamePhaseManager.timeline5Completed; break;
                case 6: isCompleted = _gamePhaseManager.timeline6Completed; break;
                case 7: isCompleted = _gamePhaseManager.timeline7Completed; break;
            }

            if (isCompleted)
            {
                CompletePage(); // disable all the buttons
                ToggleInteractabilityEventButtons(false);
                ToggleInteractabilitySlotButtons(false);
            }
            else
            {
                ToggleInteractabilityEventButtons(true);
                ToggleInteractabilitySlotButtons(true);
            }
        }


        //private void CheckCurrentPageCompletion()
        //{
        //    if (_currentPage.pageID == 1)
        //    {
        //        if (_gamePhaseManager.timeline1Completed)
        //        {
        //            CompletePage(); //disable all the buttons.. 
        //            ToggleInteractabilityEventButtons(false);
        //            ToggleInteractabilitySlotButtons(false);
        //        }
        //    }

        //    if (_currentPage.pageID == 2)
        //    {
        //        if (_gamePhaseManager.timeline2Completed)
        //        {
        //            CompletePage(); //disable all the buttons.. 
        //            ToggleInteractabilityEventButtons(false);
        //            ToggleInteractabilitySlotButtons(false);
        //        }
        //    }

        //    if (_currentPage.pageID == 3)
        //    {
        //        if (_gamePhaseManager.timeline3Completed)
        //        {
        //            CompletePage(); //disable all the buttons.. 
        //            ToggleInteractabilityEventButtons(false);
        //            ToggleInteractabilitySlotButtons(false);
        //        }
        //    }

        //    if (_currentPage.pageID == 4)
        //    {
        //        if (_gamePhaseManager.timeline4Completed)
        //        {
        //            CompletePage(); //disable all the buttons.. 
        //            ToggleInteractabilityEventButtons(false);
        //            ToggleInteractabilitySlotButtons(false);
        //        }
        //    }

        //    if (_currentPage.pageID == 5)
        //    {
        //        if (_gamePhaseManager.timeline5Completed)
        //        {
        //            CompletePage(); //disable all the buttons.. 
        //            ToggleInteractabilityEventButtons(false);
        //            ToggleInteractabilitySlotButtons(false);
        //        }
        //    }

        //    if (_currentPage.pageID == 6)
        //    {
        //        if (_gamePhaseManager.timeline6Completed)
        //        {
        //            CompletePage(); //disable all the buttons.. 
        //            ToggleInteractabilityEventButtons(false);
        //            ToggleInteractabilitySlotButtons(false);
        //        }
        //    }

        //    if (_currentPage.pageID == 7)
        //    {
        //        if (_gamePhaseManager.timeline7Completed)
        //        {
        //            CompletePage(); //disable all the buttons.. 
        //            ToggleInteractabilityEventButtons(false);
        //            ToggleInteractabilitySlotButtons(false);
        //        }
        //    }
        //}
        public void SetCurrentEvent(TimelineEvent currentEvent)
        {
            _currentEvent = currentEvent;
        }
        public void SetCurrentSlot(TimelineSlot currentSlot)
        {
            _currentSlot = currentSlot;
        }
        public TimelineEvent GetCurrentEvent()
        {
            return _currentEvent;
        }
        public TimelineSlot GetCurrentSlot()
        {
            return _currentSlot;
        }

        public void ToggleInteractabilityEventButtons(bool interact)
        {
            if (interact)
            {
                foreach (Button button in _eventButtons)
                {
                    button.interactable = true;
                }
            }
            else
            {
                foreach (Button button in _eventButtons)
                {
                    button.interactable = false;
                }
            }
        }
        public void ToggleInteractabilitySlotButtons(bool interact)
        {
            if (interact)
            {
                foreach(TimelineSlot slot in _slots)
                {
                    slot.SetCompleted(false);
                }
            }
            else
            {
                foreach (TimelineSlot slot in _slots)
                {
                    slot.SetCompleted(true);
                }
            }
        }
        public void ToggleEventPanel(bool toggle)
        {
            if (toggle)
            {
                _eventPanel.gameObject.SetActive(true);
            }
            else
            {
                _eventPanel.DisablePanel();
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaperKiteStudio.Dangers
{
    public class TimelineSlot : MonoBehaviour
    {
        [SerializeField]
        private Timeline _timelineManager;
        [SerializeField]
        private GameObject _eventPanel;
        [SerializeField]
        private TimelineEvent _associatedEvent;
        [SerializeField]
        private GameObject _positionToEnable;

        public bool completed;
        [SerializeField]
        private Image _thisImage;
        [SerializeField]
        Button _thisButton;

        public void SlotClicked()
        {
            _timelineManager.SetCurrentEvent(_associatedEvent);
            //open event panel. 
            _eventPanel.transform.position = _positionToEnable.transform.position;
            _timelineManager.ToggleEventPanel(true);
            _timelineManager.SetCurrentSlot(this);
            _timelineManager.ToggleInteractabilityEventButtons(true);
        }

        public void SetAssociatedEvent(TimelineEvent tlEvent)
        {
            _associatedEvent = tlEvent;
        }
        public TimelineEvent GetAssociatedEvent()
        {
            return _associatedEvent;
        }

        public void SetCompleted(bool complete)
        {
            completed = complete;

            if (completed)
            {
                _thisImage.color = Color.yellow;
                _thisButton.interactable = false;
                _timelineManager.SetNumberCorrect();
            }
            else
            {
                _thisImage.color = Color.white;
                _thisButton.interactable = true;
            }
            // save it? 
        }
    }
}
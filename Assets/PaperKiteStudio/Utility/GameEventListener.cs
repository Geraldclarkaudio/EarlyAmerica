using UnityEngine;
using UnityEngine.Events;
namespace PaperKiteStudio.Dangers
{
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        public UnityEvent<int> _dialoguePositionChangeResponse;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            if (Response != null)
                Response.Invoke();
        }
        public void OnDialoguePositionEventRaised()
        {
            if (_dialoguePositionChangeResponse != null)
            {
                _dialoguePositionChangeResponse.Invoke(0); // original position.
            }
        }
    }
}
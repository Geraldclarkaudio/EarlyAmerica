
using UnityEngine;
using UnityEngine.UI;
namespace PaperKiteStudio.Dangers
{
    public class TimelineEventButton : MonoBehaviour
    {
        [SerializeField]
        private int _buttonID;
        [SerializeField]
        private Timeline _timelineManager;
        [SerializeField]
        private Sprite _correctSprite;
        [SerializeField]
        private Sprite _wrongSprite;
        [SerializeField]
        private GameObject _correctPopUp;
        [SerializeField]
        private Image _correctImage;

        private void OnEnable()
        {
            _correctPopUp.SetActive(false); 
        }
        public void SelectedButton()
        {
            if (_timelineManager.GetCurrentSlot() != null)
            {
                if (_timelineManager.GetCurrentSlot().GetAssociatedEvent().eventID != _buttonID)
                {
                    _correctImage.sprite = _wrongSprite;
                }
                else
                {
                    _correctImage.sprite = _correctSprite;
                    _timelineManager.GetCurrentSlot().SetCompleted(true);
                }


                _timelineManager.ToggleEventPanel(false);
                _timelineManager.ToggleInteractabilityEventButtons(false);
                _correctPopUp.SetActive(true);

            }
        }
    }
}
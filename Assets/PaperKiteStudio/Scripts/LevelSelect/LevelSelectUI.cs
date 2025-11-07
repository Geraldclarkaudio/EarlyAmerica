using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PaperKiteStudio.Dangers
{
    public class LevelSelectUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _levelPanel;
        [SerializeField]
        private List<Button> _levelButtons;
        [SerializeField]
        private GamePhaseManager _gamePhaseManager;
        [SerializeField]
        private GameObject _completeGameButton;
        [SerializeField]
        private GameObject _journalInstructionText;
        private void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
        }

        public void UnlockLevelButtons()
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                _levelButtons[i].interactable = i < _gamePhaseManager.GetGamePhase();
            }

            if(_gamePhaseManager.GetGamePhase() == 0)
            {
                _levelButtons[0].interactable = true;
            }

            if(_gamePhaseManager.GetGamePhase() >= 8)
            {
                _completeGameButton.SetActive(true);
            }
        }
        public void LockAllButtons()
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                _levelButtons[i].interactable = false;
            }
        }

        public void LevelSelected(int levelID)
        {
            _gamePhaseManager.SetTempPhase(levelID);
        }

        public void CompleteGameButtonEnable() // give player option to complete assignment.
        {
            _completeGameButton.SetActive(true);
        }
        public void ActivateJournalInstructions()
        {
            if(_gamePhaseManager.GetHasOpenedJournal() == true)
            _journalInstructionText.SetActive(true);
        }
    }
}
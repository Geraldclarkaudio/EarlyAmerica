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

        private void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
          //  UnlockLevelButtons(_gamePhaseManager.GetGamePhase()); 
          //this is now being called as a game vent after the dialogue when loading into the level select scene. 
        }

        public void UnlockLevelButtons() //planet buttons are enabled/disabled according to current GamePhase. 
        {
            //Debug.Log("UNLOCK BUTTONS");
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
    }
}
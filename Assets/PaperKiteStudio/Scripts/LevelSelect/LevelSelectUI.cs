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

        private void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
            UnlockLevelButtons(_gamePhaseManager.GetGamePhase());
        }

        public void UnlockLevelButtons(int phase) //planet buttons are enabled/disabled according to current GamePhase. 
        {
            for (int i = 0; i < _levelButtons.Count; i++)
            {
                _levelButtons[i].interactable = i < phase;
            }

            if(phase == 0)
            {
                _levelButtons[0].interactable = true;
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
    }
}
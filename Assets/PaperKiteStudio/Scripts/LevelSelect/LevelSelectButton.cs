using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class LevelSelectButton : MonoBehaviour
    {
        [SerializeField]
        private GamePhaseManager _gamePhaseManager;
        private void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
        }
        public void Clicked(int tempPhase)
        {
            _gamePhaseManager.SetTempPhase(tempPhase);
        }
    }
}
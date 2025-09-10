using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class DodgeBallManager : MonoBehaviour
    {
         public enum GameState // only controls whether the timer is going or not right now.. 
         {
                PreGame,
                Playing,
                WinGame,
                LoseGame,
                Paused
         }

        public GameState gameState;

        [SerializeField]
        private DialogueManager _dialogueManger;
        [SerializeField]
        private GamePhaseManager _gamePhaseManager;
        private Dodgeball_Data data;

        private void Start()
        {
            data = new();
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();    
            gameState = GameState.PreGame;
        }

        public void WinGame()
        {
            gameState = GameState.WinGame;
        }
        public void LoseGame()
        {
            gameState = GameState.LoseGame;
        }
        public void Playing()
        {
            gameState = GameState.Playing;
        }
        public void Paused()
        {
            gameState = GameState.Paused;
        }
    }
}
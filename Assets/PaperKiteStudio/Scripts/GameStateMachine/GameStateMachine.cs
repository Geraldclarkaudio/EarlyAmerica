using System;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class GameStateMachine : MonoBehaviour
    {
        public enum GameState
        {
            Pregame,
            Playing,
            Paused,
            Win,
            Lose,
            AdvanceRound,
            CutScente
        }

        [Header("Current Game State")]
        [SerializeField] private GameState currentState = GameState.Pregame;
        public GameState CurrentState => currentState;

        public static event Action<GameState> OnStateChanged;

        public void SetState(GameState newState)
        {
            if (newState == CurrentState) return;

            currentState = newState;
            Debug.Log($"GameState changed to: {newState}");
            OnStateChanged?.Invoke(newState);
        }

        public bool Is(GameState state) => CurrentState == state;
    }
}
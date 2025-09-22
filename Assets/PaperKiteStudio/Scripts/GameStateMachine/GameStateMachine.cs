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

        public GameState CurrentState { get; private set; } = GameState.Pregame;

        public static event Action<GameState> OnStateChanged;

        public void SetState(GameState newState)
        {
            if (newState == CurrentState) return;

            CurrentState = newState;
            Debug.Log($"GameState changed to: {newState}");
            OnStateChanged?.Invoke(newState);
        }

        public bool Is(GameState state) => CurrentState == state;
    }
}
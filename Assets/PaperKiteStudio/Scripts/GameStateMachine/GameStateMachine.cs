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
            OnStateChanged?.Invoke(newState);
        }

        public bool Is(GameState state) => CurrentState == state;

        public void SetToPregame() => SetState(GameState.Pregame);
        public void SetToPlaying() => SetState(GameState.Playing);
        public void SetToPaused() => SetState(GameState.Paused);
        public void SetToWin() => SetState(GameState.Win);
        public void SetToLose() => SetState(GameState.Lose);
        public void SetToAdvanceRound() => SetState(GameState.AdvanceRound);
        public void SetToCutScene() => SetState(GameState.CutScente);


    }
}
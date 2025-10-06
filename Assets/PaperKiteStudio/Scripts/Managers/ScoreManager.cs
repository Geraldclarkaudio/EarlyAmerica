using System;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private int startingScore = 50;
        [SerializeField] private int minScore = 0;
        [SerializeField] private bool useLossCondition = true;

        [SerializeField] private int currentScore;
        [SerializeField] private int roundStartScore = 50;

        public event Action<int> ScoreChanged;
        public event Action ScoreDepleted;

        private GameStateMachine gameStateMachine;

        private void OnEnable()
        {
            GameStateMachine.OnStateChanged += HandleGameStateChanged;
        }

        private void OnDisable()
        {
            GameStateMachine.OnStateChanged -= HandleGameStateChanged;
        }

        void Awake()
        {
            currentScore = startingScore;
            ScoreChanged?.Invoke(currentScore);
            gameStateMachine = FindAnyObjectByType<GameStateMachine>();
        }

        public void ModifyScore(int amount)
        {
            currentScore += amount;
            ScoreChanged?.Invoke(currentScore);
            currentScore = Mathf.Max(currentScore, minScore);

            if (useLossCondition && currentScore <= minScore)
            {
                ScoreDepleted?.Invoke();
                gameStateMachine.SetState(GameStateMachine.GameState.Lose);
            }
        }

        public void ApplyScoreEffect(ScoreEffect effect)
        {
            if (effect == null) return;

            ModifyScore(effect.scoreDelta);

            if (useLossCondition && effect.triggersLossOnDepletion && currentScore <= minScore)
            {
                ScoreDepleted?.Invoke();
                gameStateMachine.SetState(GameStateMachine.GameState.Lose);
            }
        }

        private void HandleGameStateChanged(GameStateMachine.GameState newState)
        {
            switch (newState)
            {
                case GameStateMachine.GameState.Playing:
                    currentScore = roundStartScore;
                    ScoreChanged?.Invoke(currentScore);
                    break;
                case GameStateMachine.GameState.Win:
                    roundStartScore = currentScore;
                    break;
            }
        }
    }
}

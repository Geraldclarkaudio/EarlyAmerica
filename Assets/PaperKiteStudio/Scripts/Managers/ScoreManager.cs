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

        public event Action<int> ScoreChanged;
        public event Action ScoreDepleted;

        void Awake()
        {
            currentScore = startingScore;
            ScoreChanged?.Invoke(currentScore);
        }

        public void ModifyScore(int amount)
        {
            currentScore += amount;
            ScoreChanged?.Invoke(currentScore);

            if (useLossCondition && currentScore <= minScore)
            {
                ScoreDepleted?.Invoke();
            }
        }

        public int GetScore() => currentScore;

        // may get rid of
        public void TriggerGameOver()
        {
            Debug.Log("Score reached 0");
        }

        public void ApplyScoreEffect(ScoreEffect effect)
        {
            if (effect == null) return;

            ModifyScore(effect.scoreDelta);

            if (useLossCondition && effect.triggersLossOnDepletion && currentScore <= minScore)
            {
                ScoreDepleted?.Invoke();
            }
        }


    }
}

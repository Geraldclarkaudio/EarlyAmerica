using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class CrateScoreTrigger : MonoBehaviour
    {
        [SerializeField] private ScoreEffect scoreEffect;
        [SerializeField] private ScoreManager scoreManager;

        public void ApplyScore()
        {
            if (scoreEffect == null || scoreManager == null) return;

            scoreManager.ApplyScoreEffect(scoreEffect);
        }
    }
}

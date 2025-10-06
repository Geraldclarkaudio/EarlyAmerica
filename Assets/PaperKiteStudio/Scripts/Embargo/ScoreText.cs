using UnityEngine;
using TMPro;

namespace PaperKiteStudio.Dangers
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] ScoreManager scoreManager;
        [SerializeField] TMP_Text scoreText;

        private void OnEnable()
        {
            scoreManager.ScoreChanged += HandleScoreChanged;
            scoreManager.ScoreDepleted += HandleScoreDepleted;
        }

        private void OnDisable()
        {
            scoreManager.ScoreChanged -= HandleScoreChanged;
            scoreManager.ScoreDepleted -= HandleScoreDepleted;
        }

        private void HandleScoreChanged(int obj)
        {
            scoreText.text = "Score: " + obj.ToString();
        }

        private void HandleScoreDepleted()
        {
            scoreText.text = "0";
        }
    }
}

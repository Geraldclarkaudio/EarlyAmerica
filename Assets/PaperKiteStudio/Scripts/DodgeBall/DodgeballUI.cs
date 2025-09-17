using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace PaperKiteStudio.Dangers
{
    public class DodgeballUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _frenchScoreText;
        [SerializeField] private TMP_Text _britishScoreText;
        //[SerializeField] private TMP_Text _loudmouthScoreText;
        [SerializeField] private TMP_Text _playerScoreText;

        [SerializeField] private int frenchScore = 0;
        [SerializeField] private int britishScore = 0;
        [SerializeField] private int audienceScore = 0;
        //[SerializeField] private int loudmouthScore = 0;
        [SerializeField] private int playerScore = 0;
        [SerializeField] private Slider neutralitySlider;
        [SerializeField] private float maxInfluenceSpread = 10f;

        public float neutrality = 0;


        private void OnEnable()
        {
            Ball.onHitPlayer += UpdateScore;
        }
        private void OnDisable()
        {
            Ball.onHitPlayer -= UpdateScore;
        }

        public float GetNeutralityValue()
        {
            int totalInfluence = frenchScore + britishScore + audienceScore;
            int playerInfluence = playerScore;

            // Neutrality score: positive if player is dominant, negative if others are
            return playerInfluence - totalInfluence;
        }

        public void Reset()
        {
            neutrality = 6;
            neutralitySlider.value = 0;
            britishScore = 0;
            audienceScore = 0;
            playerScore = 0;
            frenchScore = 0;
        }

        public void UpdateScore(BallType type)
        {
            switch (type)
            {
                case BallType.French:
                    frenchScore++;

                    neutrality = Mathf.Clamp(GetNeutralityValue(), -maxInfluenceSpread, maxInfluenceSpread);
                    neutralitySlider.value = neutrality;

                    _frenchScoreText.text = frenchScore.ToString();
                    break;
                case BallType.British:
                    britishScore++;

                    neutrality = Mathf.Clamp(GetNeutralityValue(), -maxInfluenceSpread, maxInfluenceSpread);
                    neutralitySlider.value = neutrality;

                    _britishScoreText.text = britishScore.ToString();
                    break;
                case BallType.Audience:
                    audienceScore++;

                    neutrality = Mathf.Clamp(GetNeutralityValue(), -maxInfluenceSpread, maxInfluenceSpread);
                    neutralitySlider.value = neutrality;

                    //_loudmouthScoreText.text = loudmouthScore.ToString();
                    break;
                case BallType.Player:
                    playerScore++;

                    neutrality = Mathf.Clamp(GetNeutralityValue(), -maxInfluenceSpread, maxInfluenceSpread);
                    neutralitySlider.value = neutrality;

                    _playerScoreText.text = playerScore.ToString();
                    break;
            }
        }

    }
}
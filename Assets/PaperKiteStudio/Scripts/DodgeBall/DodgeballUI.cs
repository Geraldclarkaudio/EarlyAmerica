using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
        //[SerializeField] private int loudmouthScore = 0;
        [SerializeField] private int playerScore = 0;

        private void OnEnable()
        {
            Ball.onHitPlayer += UpdateScore;
        }
        private void OnDisable()
        {
            Ball.onHitPlayer -= UpdateScore;
        }

        public void UpdateScore(BallType type)
        {
            switch (type)
            {
                case BallType.French:
                    frenchScore++;
                    _frenchScoreText.text = frenchScore.ToString();
                    break;
                case BallType.British:
                    britishScore++;
                    _britishScoreText.text = britishScore.ToString();
                    break;
                case BallType.LoudMouth:
                    //loudmouthScore++;
                    //_loudmouthScoreText.text = loudmouthScore.ToString();
                    break;
                case BallType.Player:
                    playerScore++;
                    _playerScoreText.text = playerScore.ToString();
                    break;
            }
        }

    }
}
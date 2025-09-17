using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class XYZManager : MonoBehaviour
    {
        public enum GameState
        {
            Playing,
            Win,
            Lose
        }
        public GameState state;

        private void Start()
        {
            state = GameState.Playing;
        }

        private void WinGame()
        {
            state = GameState.Win;
            //TO DO move to next round without reloading scene. 

            //start dialogue 
            //disable the agents if they are alive when timer runs out. 
            //reset all the stuff. (gold amount)
        }

        private void LoseGame()
        {
            state = GameState.Lose;
            //money = 0
        }
    }
}
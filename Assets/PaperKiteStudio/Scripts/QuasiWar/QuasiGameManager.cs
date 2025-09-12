using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class QuasiGameManager : MonoBehaviour
    {
        [SerializeField]
        QuasiUI _quasiUI;
        [SerializeField]
        PlayerShip _playerShip;
        public bool gameOver; // so far only used for stopping ship movement.
        private void Start()
        {
            gameOver = false;
        }
        public void WinGame()
        {
            gameOver = true;
            //update UI
            _quasiUI.DisplayWinPrompt();
            _playerShip.DisableMove();
        }
    }
}
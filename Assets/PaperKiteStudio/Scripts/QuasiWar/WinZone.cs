using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class WinZone : MonoBehaviour
    {
        [SerializeField]
        QuasiGameManager gameManager;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                //WIN
                gameManager.WinGame();

            }   
        }
    }
}
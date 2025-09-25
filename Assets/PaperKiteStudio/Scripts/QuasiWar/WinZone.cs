using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class WinZone : MonoBehaviour
    {
        [SerializeField]
        private Canvas _winCanvas;
        [SerializeField]
        private PlayerMove _player;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                //WIN prompt to press e to light ship on fire. 
                _winCanvas.enabled = true;
                _player.SetCanBurn(true);
            }   
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                //WIN prompt to press e to light ship on fire. 
                _winCanvas.enabled = false;
                _player.SetCanBurn(false);

            }
        }
    }
}
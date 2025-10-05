using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class ImpressWinZone : MonoBehaviour
    {
        [SerializeField]
        private GamePhaseManager gamePhaseManager;
        [SerializeField]
        private GameObject _endImpressmentCutscene;

        private void Start()
        {
            gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if(gamePhaseManager.GetGamePhase() == 6) // if we havent beat this yet.. 
                {
                    gamePhaseManager.IncrementGamePhase();
                }
                _endImpressmentCutscene.SetActive(true);
            }
        }
    }
}
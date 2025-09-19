using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class XYZ_Manager_V2 : MonoBehaviour
    {
        [SerializeField] GamePhaseManager gamePhaseManager;
        [SerializeField] private XYZSpawner spawner;
        [SerializeField] private XYZTimer timer;
        [SerializeField] private XYZHUD hud;

        public static event Action StopSpawn;
        public static event Action StartSpawn;

        public enum GameState
        {
            Pregame,
            Hit,
            Playing,
            Win,
            Lose
        }
        public GameState gameState;

        private void Start()
        {
            XYZ_V2.onSteal += SetStateHit;
            XYZHUD.SetPlaying += SetStatePlaying;
            gameState = GameState.Pregame;
        }

        public void SetStatePlaying()
        {
            gameState = GameState.Playing;
            //start spawning
            StartSpawn?.Invoke();
        }

        public void SetStateHit()
        {
            //stop spawning
            StopSpawn?.Invoke();
            gameState = GameState.Hit;
            Debug.Log("tribute paid");
        }

        public void SetStateWin()
        {
            gameState = GameState.Win;
            gamePhaseManager.IncrementGamePhase();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PaperKiteStudio.Dangers
{

    public class GamePhaseManager : MonoBehaviour
    {
        [SerializeField]
        private Initializer _init;
        [SerializeField]
        private DialogueManager _dialogueManager;
        [SerializeField]
        private UIManager _uiManager;

        public int _gamePhase; // used to store value of overall game's progression
        public int _phaseStep; // used to store sub phase of the current game/temp phase. may not need it. depends on the design. 
        public int _tempPhase; // used to store the value of the currently played phase.. 

        /// <summary>
        /// You can have a tempPhase of 1 while the gmae phase is 10 for example. If we want the player to be able to return to previous parts of the game, this is 
        /// necessary. Otherwise we probably wont need a tempPhase. 
        /// </summary>


        [SerializeField]
        private bool isContinuePressed;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += NewScene;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= NewScene;
        }

        private void NewScene(Scene scene, LoadSceneMode mode) 
        {
            switch (scene.name)
            {
                case "Init": // do nothing. 
                    return;
                case "Scene1":
                    switch (_gamePhase) // this can get pretty ganular depending on the game design
                    {
                        case 1: // new game clicked OR continue clicked without having completed phase 1
                            _dialogueManager.dialogueIndex = 0;
                            break;
                        case 2: // clicked continue and has completed phase 1 / not finished phase 2 
                            _dialogueManager.dialogueIndex = 1;
                            break;
                        case 3://clicked continu and has completed phase 2 / not finished phase 3 
                            _dialogueManager.dialogueIndex = 2;
                            break;
                        case 4:
                            break;
                        case 5:
                            _dialogueManager.dialogueIndex = 3;
                            break;
                    }
                    break;
                case "Scene 2 name":
                    break;
            }
            // start a dialogue at the beginning of every scene load.
            _dialogueManager.StartDialogue();
        }

        public void SetGamePhase(int phase)
        {
            _gamePhase = phase;
            _init.playerData.gamePhase = phase;
            _init.Save();
        }
        public void SetPhaseStep(int step)
        {
            _phaseStep = step;
            _init.playerData.phaseStep = step;
            _init.Save();
        }
        public void SetTempPhase(int temp)
        {
            _tempPhase = temp;
        }

        public void IncrementGamePhase() // call when current phase is completed. 
        {
            _gamePhase++;
            if (_gamePhase > 9)
            {
                _gamePhase = 10;
            }
            SetGamePhase(_gamePhase);
            SetPhaseStep(0);
            //if (GetTempPhase() < GetGamePhase())//intention with this is to allow the student to return to previous sections of the game without changing the actual game phase. 
            //{

            //}
            //else
            //{
            //    _gamePhase++;
            //    if(_gamePhase > 9)
            //    {
            //        _gamePhase = 10;
            //    }
            //    SetGamePhase(_gamePhase);
            //    SetPhaseStep(0);
            //}
        }
        public void IncrementPhaseStep()
        {
            _phaseStep++;

            if(_phaseStep > 3)
            {
                IncrementGamePhase();
            }
        }

        public int GetGamePhase()
        {
            return _gamePhase;
        }
        public int GetPhaseStep()
        {
            return _phaseStep;
        }
        public int GetTempPhase()
        {
            return _tempPhase;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                IncrementPhaseStep();
            }
        }
    }
}
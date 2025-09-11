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
                case "LevelSelect":

                    switch (_gamePhase)
                    {
                        case 0: // intro dialogue
                            _dialogueManager.dialogueIndex = 0;
                            break;

                        case 1:
                            // has played dodgeball game at least but hasnt bEAT IT.. 
                            switch (_phaseStep)
                            {
                                case 1:
                                    _dialogueManager.dialogueIndex = 2;
                                    break;
                                case 2:
                                    _dialogueManager.dialogueIndex = 2;
                                    break;
                                case 3:
                                    _dialogueManager.dialogueIndex = 2;
                                    break;
                            }
                            break;
                        case 2: //has played part 2 but not beat it
                            switch (_phaseStep)
                            {
                                case 0:
                                    _dialogueManager.dialogueIndex = 3;
                                    break;
                                case 1:
                                    _dialogueManager.dialogueIndex = 3;
                                    break;
                                case 2:
                                    _dialogueManager.dialogueIndex = 3;
                                    break;
                                case 3:
                                    _dialogueManager.dialogueIndex = 3;
                                    break;
                            }
                            break;
                        case 3:
                            //played 3 not beat..etc 
                            break;
                    }
                    break;

                case "DodgeBall":
                    if(_phaseStep == 0)
                    {
                        SetPhaseStep(1); // set phase step to 1 for first visit to this game. 
                    }
                    switch (_gamePhase) // this can get pretty ganular depending on the game design
                    {
                        case 1: // new game clicked OR continue clicked without having completed phase 1
                            _dialogueManager.dialogueIndex = 1;
                            break;
                        case > 1:
                            //dialogue like "we have already completed this but dodgeball is fun eh? 
                            break;
                    }
                    break;
                
                case "XYZ":
                    if (_phaseStep == 0)
                    {
                        SetPhaseStep(1); // set phase step to 1 for first visit to this game. 
                    }
                    switch (_gamePhase)
                    {
                        case 2: // clicked continue and has completed phase 1 / not finished phase 2 
                            _dialogueManager.dialogueIndex = 4;
                            break;
                        case > 2:
                            //already complete but have fun
                            break;
                    }
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
        public void SetPhaseStep(int step) // call after each sub part of main.. example after each round of dodgeball, after xyz, after quasi etc. 
        {
            _phaseStep = step;
            _init.playerData.phaseStep = step;
            _init.Save();
        }
        public void SetTempPhase(int temp)
        {
            _tempPhase = temp;

            if (_tempPhase > _gamePhase)
            {
                SetGamePhase(temp);
            }
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
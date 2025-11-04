using LoLSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public bool timeline1Completed;
        public bool timeline2Completed;
        public bool timeline3Completed;
        public bool timeline4Completed;
        public bool timeline5Completed;
        public bool timeline6Completed;
        public bool timeline7Completed;

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
                    //set the timeline pages completion status? 
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
                            switch (_phaseStep)
                            {
                                case 0:
                                    _dialogueManager.dialogueIndex = 16;
                                    break;
                                case > 0: // hasnt beat it but has played it?
                                    _dialogueManager.dialogueIndex = 17;
                                    break;
                            }
                            //played 3 not beat..etc 
                            break;
                        case 4:
                            switch(_phaseStep)
                            {
                                case 0:
                                    _dialogueManager.dialogueIndex = 19;
                                    break;
                                case > 0:
                                    _dialogueManager.dialogueIndex= 20;
                                    break;

                            }
                            break;
                        case 5: // embargo
                            _dialogueManager.dialogueIndex = 23;
                            break;
                        case 6: // impressment
                            _dialogueManager.dialogueIndex = 25; 
                            break;
                        case 7: // won impressment
                            _dialogueManager.dialogueIndex = 35;
                            break;
                        case 8:
                            _dialogueManager.dialogueIndex = 39; //game complete dialogue
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
                            switch (_phaseStep)
                            {
                                case 1:
                                    _dialogueManager.dialogueIndex = 1;
                                    break;
                                case 2:
                                    _dialogueManager.dialogueIndex = 8;
                                    break;
                                case 3:
                                    _dialogueManager.dialogueIndex = 9;
                                    break;
                            }
                            break;
                        case 2:
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
                            _dialogueManager.dialogueIndex = 4;
                            //already complete but have fun
                            break;
                    }
                    break;
                case "QuasiWar":
                    if (_phaseStep == 0)
                    {
                        SetPhaseStep(1); // set phase step to 1 for first visit to this game. 
                    }
                    switch (_phaseStep)
                    {
                        case 1:
                            _dialogueManager.dialogueIndex = 5;
                            break;
                        case 2:
                            _dialogueManager.dialogueIndex = 5;
                            break;
                        case 3:
                            _dialogueManager.dialogueIndex = 5;
                            break;
                    }
                    break;
                case "BarbaryWars":
                    if (_phaseStep == 0)
                    {
                        SetPhaseStep(1); // set phase step to 1 for first visit to this game. 
                    }
                    switch (_phaseStep)
                    {
                        case 1:
                            _dialogueManager.dialogueIndex = 21; // sneak time
                            break;
                        case 2:
                            _dialogueManager.dialogueIndex = 23; // ship battle time
                            break;
                        case 3:
                            _dialogueManager.dialogueIndex = 21; // probably wont exist. we'll just increment the GP after phase 2 here. 
                            break;
                    }
                    break;

                case "Embargo":
                    if (_phaseStep == 0)
                    {
                        SetPhaseStep(1); // set phase step to 1 for first visit to this game. 
                    }
                    switch (_phaseStep)
                    {
                        case 1:
                            _dialogueManager.dialogueIndex = 24;
                            break;
                        case 2:
                            _dialogueManager.dialogueIndex = 24;
                            break;
                        case 3:
                            _dialogueManager.dialogueIndex = 24;
                            break;
                    }
                    break;
                case "Impressment":
                    if (_phaseStep == 0)
                    {
                        SetPhaseStep(1);
                    }
                    _dialogueManager.dialogueIndex = 32;
                    break;
                case "McHenry":
                    if (_phaseStep == 0)
                    {
                        SetPhaseStep(1);
                    }
                    _dialogueManager.dialogueIndex = 36;
                    break;
            }
            // start a dialogue at the beginning of every scene load.
            _dialogueManager.StartDialogue();
        }


        public void SetTimelineComplete(int timelinePage) 
        {
            switch (timelinePage)
            {
                case 1:
                    timeline1Completed = true;
                    _init.playerData.timeline1Complete = true;
                    break;
                case 2:
                    timeline2Completed = true;
                    _init.playerData.timeline2Complete = true;
                    break;
                case 3:
                    timeline3Completed = true;
                    _init.playerData.timeline3Complete = true;
                    break;
                case 4:
                    timeline4Completed = true;
                    _init.playerData.timeline4Complete = true;
                    break;
                case 5:
                    timeline5Completed = true;
                    _init.playerData.timeline5Complete = true;
                    break;
                case 6:
                    timeline6Completed = true;
                    _init.playerData.timeline6Complete = true;
                    break;
                case 7:
                    timeline7Completed = true;
                    _init.playerData.timeline7Complete = true;
                    break;
            }

            _init.Save();
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
            if(_tempPhase < _gamePhase)
            {
         
                return; // dont increment it if we are replayying a level... 
            }
            _gamePhase++;
            if (_gamePhase > 8)
            {
                _gamePhase = 8;
            }

            LOLSDK.Instance.SubmitProgress(1, _gamePhase, 8);
            SetGamePhase(_gamePhase);
            SetPhaseStep(0);
        }
        public void IncrementPhaseStep()
        {
            _phaseStep++;
            int step = _phaseStep;

            if (_gamePhase > _tempPhase)
            {
                if(_phaseStep > 3)
                {
                    _phaseStep = 0;
                }
                return;
            }

            // added this so that if i a player quits after the phase was completed it will save
            _init.playerData.phaseStep = step;
            _init.Save();

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
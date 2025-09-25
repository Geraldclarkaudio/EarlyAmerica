using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
namespace PaperKiteStudio.Dangers
{
    public class sHIPbURNcUT : MonoBehaviour
    {
        DialogueManager _dialogueManager;
        GamePhaseManager _gamePhaseManager;
        [SerializeField]
        private PlayableDirector _shipBurnDirector;
        // Start is called before the first frame update
        void Start()
        {
            _gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();    
            _dialogueManager = FindAnyObjectByType<DialogueManager>();
        }

        public void BeginShipBurnDialogue()
        {
            _gamePhaseManager.IncrementGamePhase();
            _dialogueManager.dialogueIndex = 22;
            _dialogueManager.StartDialogue();

            _shipBurnDirector.Pause();
        }
        public void ShipBurnDrectorResume()
        {
            _shipBurnDirector.Resume();
        }
    }
}
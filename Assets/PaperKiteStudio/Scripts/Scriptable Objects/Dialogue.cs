using TMPro;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueObject/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public int dialogueID;
        public string[] key;
        public Sprite[] icons;
        public string[] _speakerName;
        public string[] _animations; // animator for the player character. 
        public GameEvent[] _newUIEvents;
        public GameEvent[] _dialoguePositionChanges;
        public GameEvent[] _eventTriggers;
        public GameEvent _endDialogueEvent;
    }
}
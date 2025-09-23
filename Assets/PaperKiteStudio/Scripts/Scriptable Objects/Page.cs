using TMPro;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    [CreateAssetMenu(fileName = "Page", menuName = "JournalPage/Page")]
    public class Page : ScriptableObject
    {
        public string titleKey;
        public int pageNumber;
        public string[] updateTextKeys;
        public int associatedGamePhase;
    }
}
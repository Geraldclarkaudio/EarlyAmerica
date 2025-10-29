using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    [CreateAssetMenu(fileName = "Timeline", menuName = "Timeline/Page")]
    public class TimelinePage : ScriptableObject
    {
        public int pageID;
        public string pageTitle;
        public string[] pageButtonTexts;
        public string[] timelineDates;
    }
}

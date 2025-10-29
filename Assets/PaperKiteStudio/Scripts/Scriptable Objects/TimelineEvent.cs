using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    [CreateAssetMenu(fileName = "Timeline", menuName = "Timeline/Event")]
    public class TimelineEvent : ScriptableObject
    {
        public int eventID;
        public string eventText;
    }
}

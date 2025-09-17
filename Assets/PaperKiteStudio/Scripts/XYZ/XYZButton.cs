using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class XYZButton : MonoBehaviour
    {
        [SerializeField]
        private XYZ _agent;
        public void StopAgent()
        {
            _agent.StopAgent();
        }
    }
}
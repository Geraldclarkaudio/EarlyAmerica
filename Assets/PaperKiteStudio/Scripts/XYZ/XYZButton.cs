using System;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class XYZButton : MonoBehaviour
    {
        [SerializeField] GameStateMachine stateMachine;
        [SerializeField]
        private XYZ_V2 _agent;

        public static event Action ReflexClicked;
        public void StopAgent()
        {
            _agent.StopAgent();
        }

        public void InvokeReflexAction()
        {
            ReflexClicked?.Invoke();
        }
    }
}
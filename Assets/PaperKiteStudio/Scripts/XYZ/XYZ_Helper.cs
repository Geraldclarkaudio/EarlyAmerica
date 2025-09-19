using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class XYZ_Helper : MonoBehaviour
    {
        [SerializeField] GameStateMachine stateMachine;
        public void StartGame()
        {
            stateMachine.SetState(GameStateMachine.GameState.Playing);
        }
    }
}

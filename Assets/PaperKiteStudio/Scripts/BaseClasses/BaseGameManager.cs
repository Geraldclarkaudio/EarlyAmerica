using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public abstract class BaseGameManager : MonoBehaviour
    {
        [SerializeField] protected GameStateMachine stateMachine;

        protected virtual void OnEnable()
        {
            GameStateMachine.OnStateChanged += HandleStateChange;
        }

        protected virtual void OnDisable()
        {
            GameStateMachine.OnStateChanged -= HandleStateChange;
        }

        protected abstract void HandleStateChange(GameStateMachine.GameState state);
    }
}

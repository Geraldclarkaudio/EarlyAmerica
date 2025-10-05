using PaperKiteStudio.Dangers;
using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class DeliverCollision : BaseCollision
    {
        private Ghent_Manager manager;
        private GameStateMachine gameStateMachine;

        private void Start()
        {
            manager = FindAnyObjectByType<Ghent_Manager>();
            gameStateMachine = FindAnyObjectByType<GameStateMachine>();
        }

        protected override void OnTargetCollision(Collider2D other)
        {
            if (manager.hasMessage)
            {
                manager.SetHasMessage(false);
                gameStateMachine.SetState(GameStateMachine.GameState.Paused);
            }
        }
    }
}

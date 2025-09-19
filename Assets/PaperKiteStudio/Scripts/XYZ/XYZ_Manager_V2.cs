using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class XYZ_Manager_V2 : MonoBehaviour
    {
        private GamePhaseManager gamePhaseManager;
        [SerializeField] GameStateMachine stateMachine;
        [SerializeField] private XYZ_Spawner_V2 spawner;
        [SerializeField] private XYZTimer timer;
        [SerializeField] private XYZHUD hud;
        [SerializeField] private XYZ_Dialog dialog;

        private void Start()
        {
            gamePhaseManager = FindAnyObjectByType<GamePhaseManager>();
        }

        private void OnEnable()
        {
            GameStateMachine.OnStateChanged += HandleStateChange;
            XYZButton.ReflexClicked += HandleReflexSuccess;
        }

        private void OnDisable()
        {
            GameStateMachine.OnStateChanged -= HandleStateChange;
            XYZButton.ReflexClicked -= HandleReflexSuccess;
        }

        private void HandleStateChange(GameStateMachine.GameState state)
        {
            switch (state)
            {
                case GameStateMachine.GameState.Playing:
                    spawner.Spawn();
                    break;

                case GameStateMachine.GameState.Paused:
                    hud.UpdateCoinAmount();
                    hud.DisplayPaidPrompt();
                    break;
                case GameStateMachine.GameState.Win:
                    dialog.StartWinDialog();
                    gamePhaseManager.IncrementPhaseStep();                    
                    break;
                case GameStateMachine.GameState.Lose:
                    //repeat round
                    break;
                case GameStateMachine.GameState.AdvanceRound:
                    gamePhaseManager.IncrementPhaseStep();
                    break;
            }
        }

        private void HandleReflexSuccess()
        {
            spawner.Spawn();
        }
    }
}
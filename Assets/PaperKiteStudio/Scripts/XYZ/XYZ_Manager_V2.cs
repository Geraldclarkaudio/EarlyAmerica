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
                    hud.UpdateCoinUI();
                    spawner.Spawn();
                    break;

                case GameStateMachine.GameState.Paused:
                    hud.UpdateCoinAmount();
                    hud.DisplayPaidPrompt();
                    break;
                case GameStateMachine.GameState.Win:
                    hud.SetRoundStartCoinAmount();
                    dialog.StartWinDialog();
                    gamePhaseManager.IncrementPhaseStep();                    
                    break;
                case GameStateMachine.GameState.Lose:
                    timer.EndRound();
                    hud.SetCoinAmount();
                    timer.ResetTimer();
                    dialog.StartLoseDialog();
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
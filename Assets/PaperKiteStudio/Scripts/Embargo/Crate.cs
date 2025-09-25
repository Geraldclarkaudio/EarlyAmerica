using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Crate : MovingObject
    {
        [SerializeField] private ScoreManager scoreManager;


        //private void Start()
        //{
        //    StartMoving();
        //}

        protected override void OnTargetCollision(Collider2D other)
        {
            if (CompareTag("Foreign"))
            {
                if (TryGetComponent<CrateScoreTrigger>(out var trigger))
                {
                    trigger.ApplyScore();
                }
            }

            gameObject.SetActive(false);
        }
    }
}

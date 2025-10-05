using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class MessageCollision : BaseCollision
    {
        private Ghent_Manager manager;

        private void Start()
        {
            manager = FindAnyObjectByType<Ghent_Manager>();
        }

        protected override void OnTargetCollision(Collider2D other)
        {
            Debug.Log("collision happened");
            manager.SetHasMessage(true);
            gameObject.SetActive(false);
        }
    }
}

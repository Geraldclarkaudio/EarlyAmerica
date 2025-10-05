using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class BaseCollision : MonoBehaviour
    {
        [Header("Collision Settings")]
        [SerializeField] protected string targetTag = "Player";

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (string.IsNullOrEmpty(targetTag))
            {
                Debug.LogWarning($"{name} has no targetTag set for collision detection.");
                return;
            }

            if (other.CompareTag(targetTag))
            {
                OnTargetCollision(other);
            }
        }

        /// <summary>
        /// Called when this object collides with something matching targetTag.
        /// Override this in subclasses to define custom behavior.
        /// </summary>
        protected virtual void OnTargetCollision(Collider2D other)
        {
            // Default: do nothing
        }
    }
}

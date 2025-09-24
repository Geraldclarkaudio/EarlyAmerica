using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class Crate : MovingObject
    {
        private void Start()
        {
            StartMoving();
        }

        protected override void OnTargetCollision(Collider2D other)
        {
            Debug.Log($"{name} hit {other.name} and will now self-destruct.");
            gameObject.SetActive(false);
        }
    }
}

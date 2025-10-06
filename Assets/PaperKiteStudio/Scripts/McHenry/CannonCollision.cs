using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    public class CannonCollision : BaseCollision
    {
        [SerializeField] private GameObject explosionEffect;

        protected override void OnTargetCollision(Collider2D other)
        {
            if (other.TryGetComponent<CrateScoreTrigger>(out var trigger))
            {
                trigger.ApplyScore();
            }

            if (explosionEffect != null && explosionEffect.TryGetComponent<TimedEffect>(out var effect))
            {
                effect.Play(0.5f);
            }


            gameObject.SetActive(false);
        }
    }
}
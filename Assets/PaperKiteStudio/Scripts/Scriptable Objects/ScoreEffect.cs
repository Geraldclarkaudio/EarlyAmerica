using UnityEngine;

namespace PaperKiteStudio.Dangers
{
    [CreateAssetMenu(menuName = "ScoreEffect")]
    public class ScoreEffect : ScriptableObject
    {
        [Tooltip("How much to modify the score (positive or negative)")]
        public int scoreDelta;

        [Tooltip("If true, triggers game over when applied and score reaches zero")]
        public bool triggersLossOnDepletion;
    }


}

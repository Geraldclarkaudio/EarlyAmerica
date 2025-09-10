using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class AudienceBall : Ball
    {
        private void Awake()
        {
            ballType = BallType.Audience;
            _associatedCharacter = GameObject.Find("Audience Guy");
        }
    }
}
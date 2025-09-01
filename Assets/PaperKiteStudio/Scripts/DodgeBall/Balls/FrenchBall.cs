using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class FrenchBall : Ball
    {
        private void Awake()
        {
            ballType = BallType.French;
            _associatedCharacter = GameObject.Find("French Guy");
        }
    }
}
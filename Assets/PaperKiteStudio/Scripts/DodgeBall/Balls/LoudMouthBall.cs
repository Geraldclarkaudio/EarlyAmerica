using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class LoudMouthBall : Ball
    {
        private void Awake()
        {
            ballType = BallType.LoudMouth;
            _associatedCharacter = GameObject.Find("LoudMouth Guy");
        }
    }
}
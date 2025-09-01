using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PaperKiteStudio.Dangers
{
    public class BritishBall : Ball
    {
        private void Awake()
        {
            ballType = BallType.British;
            _associatedCharacter = GameObject.Find("British Guy");
        }
    }
}
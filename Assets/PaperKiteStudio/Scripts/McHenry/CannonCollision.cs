using System.Collections;
using System.Collections.Generic;
using PaperKiteStudio.Dangers;
using UnityEngine;

public class CannonCollision : BaseCollision
{
    protected override void OnTargetCollision(Collider2D other)
    {
        gameObject.SetActive(false);
    }
}

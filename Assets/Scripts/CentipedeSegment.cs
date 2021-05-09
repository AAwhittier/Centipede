using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSegment : Enemy
{
    public override void TakeDamage()
    {
        gameObject.SetActive(false);
    }
    
}

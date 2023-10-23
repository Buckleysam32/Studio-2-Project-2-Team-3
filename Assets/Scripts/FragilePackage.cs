using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragilePackage : Package
{
    
    //an example of how to override the base class functios for specific package types
    protected override void TakeDamage(float damage)
    {
        durability -= damage*1.5f; // its fragile so it takes more damage
    }
}

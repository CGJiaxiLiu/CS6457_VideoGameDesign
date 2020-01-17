using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGlassChestLevel0 : BreakableGlassChest
{
    public GameObject tutBubble;

    public override void OnCollideWithWeapon()
    {
        Destroy(tutBubble);
        base.OnCollideWithWeapon();
    }
}

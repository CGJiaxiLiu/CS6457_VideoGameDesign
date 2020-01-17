using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public GameObject Crowbar;
    public GameObject Spear;

    public void CrowbarAttackStart()
    {
        print("AttackStart");
        Crowbar.transform.Find("brokeGlassCase").GetComponent<CapsuleCollider>().enabled = true;
    }

    public void CrowbarAttackEnd()
    {
        print("AttackEnd");
        Crowbar.transform.Find("brokeGlassCase").GetComponent<CapsuleCollider>().enabled = false;
    }

    public void CrowbarAttackAnimationEnd()
    {
        print("CrowbarAttackAnimationEnd");
        Crowbar.GetComponent<crowbar>().resume();
    }

    public void SpearAttackStart()
    {
        print("SpearAttackStart");
        Spear.GetComponent<spear>().collisionEnable = true;
    }

    public void SpearAttackEnd()
    {
        print("SpearAttackEnd");
        Spear.GetComponent<spear>().collisionEnable = false;
    }

    public void SpearAttackAnimationEnd()
    {
        Spear.GetComponent<spear>().resume();
    }
}

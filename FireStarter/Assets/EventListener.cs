using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour
{
    public void AttackStart()
    {
        print("AttackStart");
        GetComponent<Animator>().SetBool("IsAttacking", true);
    }

    public void AttackEnd()
    {
        print("AttackEnd");
        GetComponent<Animator>().SetBool("IsAttacking", false);
    }


}

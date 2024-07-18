using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    private Attacker attacker;

    void Start()
    {
        attacker = transform.parent.GetComponent<Attacker>();
    }

    
    public void AttackStart()
    {
        attacker.AttackStart();
    }

}

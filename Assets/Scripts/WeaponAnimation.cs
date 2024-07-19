using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    [SerializeField] private Attacker attacker;

    
    
    public void AttackCol()
    {
        attacker.AttackCol();
    }

    public void AttackStart()
    {
        attacker.AttackStart();
    }
}

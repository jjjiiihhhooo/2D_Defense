using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Transform weaponParent;
    [SerializeField] private BoxCollider2D weaponCol;
    [SerializeField] private float damage;
    [SerializeField] private float knockBackPower;

    private bool isAttack;
    private float colliderDelay = 0.1f;

    public bool IsAttack { get => isAttack; }
    public float Damage { get => damage; }
    public float KnockBackPower { get => knockBackPower; }

    private void Update()
    {
        AttackDelay();
    }

    private void AttackDelay()
    {
        if (weaponCol.enabled)
        {
            colliderDelay -= Time.deltaTime;
            if(colliderDelay <= 0f)
            {
                weaponCol.enabled = false;
                colliderDelay = 0.1f;
            }
            
        }
        
    }

    public void AttackCol()
    {
        weaponCol.enabled = true;
    }

    public void AttackStart()
    {
        weaponParent.rotation = transform.rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float shootSpeed;
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private BoxCollider2D weaponCol;

    private bool isAttack;
    private float colliderDelay = 0.1f;
    private Vector3 speedVec;
    private Vector3 defaultVec;

    public bool IsAttack { get => isAttack; }

    private void Awake()
    {
        speedVec.x = shootSpeed;
        defaultVec.x = 1.5f;
    }

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

    public void AttackStart()
    {
        weaponCol.enabled = true;
    }
}

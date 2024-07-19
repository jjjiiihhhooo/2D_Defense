using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    public void Dead()
    {
        enemyController.Dead();
    }

    public void Hit()
    {
        enemyController.VelocityZero();
        enemyController.IsHit_False();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

[System.Serializable]
public class Enemy
{
    protected EnemyController enemyController;

    public string[] anim_names;

    public float plusGold;
    public float damage;

    [SerializeField] protected float moveSpeed;

    public virtual void Awake(EnemyController _enemyController)
    {
        enemyController = _enemyController;
        anim_names = new string[3];
    }

    public virtual void Update()
    {

    }

    public virtual void Hit()
    {
        enemyController.VelocityZero();
        KnockBack(enemyController.target.GetComponent<Player>().Attacker.KnockBackPower);
        enemyController.Animator.Play(anim_names[1], 0, 0f);
    }

    public void KnockBack(float KnockBackPower)
    {
        Vector2 dir = enemyController.target.position - enemyController.transform.position;
        enemyController.rigid.AddForce(-dir.normalized * KnockBackPower, ForceMode2D.Impulse);
    }
}

[System.Serializable]
public class Zombie : Enemy
{
    public override void Awake(EnemyController _enemyController)
    {
        base.Awake(_enemyController);
        
        anim_names[0] = "Run";
        anim_names[1] = "Hit";
        anim_names[2] = "Die";
        moveSpeed = 1f;
        plusGold = 100f;
        damage = 10f;
    }

    public override void Update()
    {
        Move();
    }

    private void Move()
    {
        if (enemyController.GetIsHit() || enemyController.GetIsDead()) return;

        if (!enemyController.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Run"))enemyController.Animator.Play(anim_names[0], 0, 0f);

        if (enemyController.transform.position.x > enemyController.target.position.x) enemyController.enemyRenderer.flipX = true;
        else if(enemyController.enemyRenderer.flipX) enemyController.enemyRenderer.flipX = false;

        

        Vector2 dirVec = enemyController.target.position - enemyController.transform.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;

        enemyController.rigid.MovePosition(enemyController.rigid.position + nextVec);

        enemyController.VelocityZero();

    }
}

[System.Serializable]
public class Skeleton : Enemy
{
    public override void Awake(EnemyController _enemyController)
    {
        base.Awake(_enemyController);

        anim_names[0] = "Run";
        anim_names[1] = "Hit";
        anim_names[2] = "Die";
        moveSpeed = 2f;
        plusGold = 200f;
        damage = 20f;
    }

    public override void Update()
    {
        Move();
    }

    private void Move()
    {
        if (enemyController.GetIsHit() || enemyController.GetIsDead()) return;

        if (!enemyController.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Run")) enemyController.Animator.Play(anim_names[0], 0, 0f);

        if (enemyController.transform.position.x > enemyController.target.position.x) enemyController.enemyRenderer.flipX = true;
        else if (enemyController.enemyRenderer.flipX) enemyController.enemyRenderer.flipX = false;



        Vector2 dirVec = enemyController.target.position - enemyController.transform.position;
        Vector2 nextVec = dirVec.normalized * moveSpeed * Time.fixedDeltaTime;

        enemyController.rigid.MovePosition(enemyController.rigid.position + nextVec);

        enemyController.VelocityZero();

    }
}

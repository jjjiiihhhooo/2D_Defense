using UnityEngine;

[System.Serializable]
public class Enemy
{
    protected EnemyController enemyController;

    public virtual void Awake(EnemyController _enemyController)
    {
        enemyController = _enemyController;
    }

    public virtual void Update()
    {
        Move();
    }

    public virtual void Hit(float _knockBackPower)
    {
        enemyController.VelocityZero();
        KnockBack(_knockBackPower);
        enemyController.Animator.Play(enemyController.anim_names[1], 0, 0f);
    }

    protected virtual void Move()
    {
        if (enemyController.GetIsHit() || enemyController.GetIsDead()) return;

        if (!enemyController.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Run")) enemyController.Animator.Play(enemyController.anim_names[0], 0, 0f);

        if (enemyController.transform.position.x > enemyController.target.position.x) enemyController.enemyRenderer.flipX = true;
        else if (enemyController.enemyRenderer.flipX) enemyController.enemyRenderer.flipX = false;

        Vector2 dirVec = enemyController.target.position - enemyController.transform.position;
        Vector2 nextVec = dirVec.normalized * enemyController.moveSpeed * Time.fixedDeltaTime;

        enemyController.rigid.MovePosition(enemyController.rigid.position + nextVec);

        enemyController.VelocityZero();
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
    }

    public override void Update()
    {
        base.Update();
    }
}

[System.Serializable]
public class Skeleton : Enemy
{
    public override void Awake(EnemyController _enemyController)
    {
        base.Awake(_enemyController);
    }

    public override void Update()
    {
        base.Update();
    }
}

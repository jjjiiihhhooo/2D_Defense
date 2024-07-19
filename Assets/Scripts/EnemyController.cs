using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;
    [SerializeField] private int index;

    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;

    public Transform Temp;

    [HideInInspector] public Transform target;

    public SpriteRenderer enemyRenderer;
    public Rigidbody2D rigid;
    public Animator Animator { get => animator; }

    private bool isHit;
    private bool isDead;

    private void Start()
    {
        curHp = maxHp;

        if (index == 0)
            enemy = new Zombie();
        else if (index == 1)
            enemy = new Skeleton();

        enemy.Awake(this);
        target = Player.Instance.transform;
    }


    private void FixedUpdate()
    {
        enemy.Update();
    }


    public void VelocityZero()
    {
        rigid.velocity = Vector3.zero;
    }

    public bool GetIsHit()
    {
        return isHit;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public void IsHit_False()
    {
        isHit = false;
    }

    public void Hit(float damage)
    {
        if (isHit) return;
        if (isDead) return;
        isHit = true;
        curHp -= damage;

        if (curHp <= 0) Die();
        
        enemy.Hit();
        
    }

    private void Die()
    {
        isDead = true;
        VelocityZero();
        animator.SetBool("isDead", true);
    }

    public void Dead()
    {

        GameManager.Instance.DataManager.PlusGold(enemy.plusGold);

        Enqueue();
    }

    private void Enqueue()
    {
        GameManager.Instance.StageManager.enemyKillQueue.Enqueue(0);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Weapon")
        {
            Hit(Player.Instance.GetAttackDamage());
        }

        if(other.tag == "Player")
        {
            Player.Instance.Hit(enemy.damage);
            Enqueue();
        }
    }
}

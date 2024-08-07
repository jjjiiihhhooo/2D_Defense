using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;
    [SerializeField] private int index;

    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;

    public Transform target;

    public SpriteRenderer enemyRenderer;
    public Rigidbody2D rigid;
    public Animator Animator { get => animator; }

    public string[] anim_names;

    public float plusGold;
    public float plusEXP;
    public float damage;
    public float moveSpeed;

    private bool isHit;
    private bool isDead;

    private void Start()
    {
        SelectEnemy();
        EnemyInit();
    }

    private void FixedUpdate()
    {
        enemy.Update();
    }

    private void EnemyInit()
    {
        curHp = maxHp;
        target = GameManager.Instance.StageManager.Player.transform;
    }

    private void SelectEnemy()
    {
        if (index == 0)
            enemy = new Zombie();
        else if (index == 1)
            enemy = new Skeleton();

        enemy.Awake(this);
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

    public void Hit(float damage, float _knockBackPower)
    {
        if (isHit) return;
        if (isDead) return;

        GameManager.Instance.SoundManager.Play("EnemyHit", false);

        isHit = true;
        curHp -= damage;

        if (curHp <= 0) Die();

        enemy.Hit(_knockBackPower);
    }

    private void Die()
    {
        isDead = true;
        VelocityZero();
        animator.SetBool("isDead", true);
    }

    public void Dead()
    {
        GameManager.Instance.DataManager.PlusGold(plusGold);
        GameManager.Instance.StageManager.GetStageEXP(plusEXP);
        Enqueue();

        Return();
    }

    private void Return()
    {
        curHp = maxHp;
        isDead = false;
        GameManager.Instance.ObjectPool.Return(this.gameObject);
    }

    private void Enqueue()
    {
        GameManager.Instance.StageManager.enemyKillQueue.Enqueue(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Weapon")
        {
            DamageData damageData = other.GetComponent<DamageData>();

            Hit(damageData.Damage, damageData.KnockBackPower);
        }

        if (other.tag == "Player")
        {
            target.GetComponent<Player>().Hit(damage);
            Enqueue();
            Return();
        }
    }
}

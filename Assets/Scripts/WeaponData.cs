
using UnityEngine;

public class WeaponData
{
    protected Weapon weapon;

    public virtual void Awake(Weapon _weapon)
    {
        weapon = _weapon;
    }

    public virtual void Update()
    {

    }

    public virtual void AttackStart()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void AttackExit()
    {

    }

    protected virtual void AttackDelay()
    {

    }
}

public class CircularSaw : WeaponData
{

}

public class Orbital : WeaponData
{
    private bool isStart = false;
    private bool isReload = false;


    private float attackTime;
    private float attackCurTime;

    private float elapsedTime;

    private Player player;
    private Transform targetTransform;

    Vector3 startPos;

    public override void Awake(Weapon _weapon)
    {
        base.Awake(_weapon);
        elapsedTime = 0f;
        attackTime = 0.3f;
        attackCurTime = 0f;
        player = GameManager.Instance.StageManager.Player;

        weapon.transform.position = player.transform.position;
        isStart = true;
    }

    public override void Update()
    {
        TargetMove();
    }

    private void TargetMove()
    {
        if (!isStart) return;


        if (isReload)
        {
            Reload();
        }
        else
        {
            AttackMove();
        }
    }

    private void AttackMove()
    {
        if (targetTransform == null)
        {
            GameObject temp = TargetSearch();
            if (temp != null) targetTransform = temp.transform;

            return;
        }

        if (attackCurTime < attackTime)
        {
            float t = attackCurTime / attackTime;
            weapon.transform.position = Vector2.Lerp(player.transform.position, targetTransform.position, t);
            attackCurTime += Time.deltaTime;
        }
        else
        {
            Explosion();
            targetTransform = null;
            attackCurTime = 0f;
            startPos = weapon.transform.position;
            isReload = true;
        }
    }

    private void Explosion()
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/Weapon/OrbitalExplosion");
        GameObject.Instantiate(obj, weapon.transform.position, Quaternion.identity);

    }

    private GameObject TargetSearch()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in objects)
        {
            float distance = Vector2.Distance(player.transform.position, obj.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = obj;
            }
        }

        return closest;
    }

    private void Reload()
    {
        if (elapsedTime < weapon.attackDelay)
        {
            float t = elapsedTime / weapon.attackDelay;
            weapon.transform.position = Vector2.Lerp(startPos, player.transform.position, t);
            elapsedTime += Time.deltaTime;
        }
        else
        {
            weapon.transform.position = player.transform.position;
            elapsedTime = 0f;
            isReload = false;
        }
    }

    public override void AttackStart()
    {
        isStart = true;
    }

    public override void Attack()
    {
        startPos = weapon.transform.position;
        base.Attack();
    }

    public override void AttackExit()
    {
        base.AttackExit();
    }

}


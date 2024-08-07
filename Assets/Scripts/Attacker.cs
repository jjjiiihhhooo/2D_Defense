using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private Transform weaponParent;
    [SerializeField] private BoxCollider2D weaponCol;
    [SerializeField] private DamageData damageData;

    public DamageData DamageData { get => damageData; }

    private float colliderDelay = 0.1f;

    private void Update()
    {
        AttackDelay();
    }

    private void AttackDelay()
    {
        if (weaponCol.enabled)
        {
            colliderDelay -= Time.deltaTime;
            if (colliderDelay <= 0f)
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
        GameManager.Instance.SoundManager.Play("PlayerAttack", false);
        weaponParent.rotation = transform.rotation;
    }



}

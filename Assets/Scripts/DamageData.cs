using UnityEngine;

public class DamageData : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float knockbackPower;

    public float Damage { get => damage; }
    public float KnockBackPower { get => knockbackPower; }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetKnockBack(float _kockback)
    {
        knockbackPower = _kockback;
    }
}

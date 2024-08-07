using UnityEngine;



public class Weapon : MonoBehaviour
{
    [SerializeField] private int weapon_index;

    private WeaponData weaponData;

    public Animator animator;

    public float attackDelay;

    private void Awake()
    {
        SelectWeapon();
        weaponData.Awake(this);
    }

    private void Update()
    {
        weaponData.Update();
    }

    private void SelectWeapon()
    {
        if (weapon_index == 0) weaponData = new CircularSaw();
        else if (weapon_index == 1) weaponData = new Orbital();

    }

    public void AttackStart()
    {
        weaponData.AttackStart();
    }

    public void Attack()
    {
        weaponData.Attack();
    }

    public void AttackExit()
    {
        weaponData.AttackExit();
    }

}

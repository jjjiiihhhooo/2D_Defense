using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Attacker attacker;

    [Header("Renderer")]
    [SerializeField] private SpriteRenderer playerModelRenderer;

    [Header("State")]
    [SerializeField] private Data data;
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;

    public float CurHp { get => curHp; }
    public float MaxHp { get => data.HP; }

    private void OnEnable()
    {
        Data _data = GameManager.Instance.DataManager.data;
        data = new Data(_data.Item_List, _data.LV, _data.SC, _data.HP, _data.ATK, _data.DEF, _data.EXP, _data.AS, _data.HR, _data.curEXP);

        attacker.DamageData.SetDamage(data.ATK);

        curHp = data.HP;
    }

    private void OnDestroy()
    {
        GameManager.Instance.DataManager.SetData(data);
    }

    private void Update()
    {
        AttackerRot();
    }

    public void Hit(float damage)
    {
        GameManager.Instance.SoundManager.Play("PlayerHit", false);
        curHp -= damage;
    }

    public void AttackerRot()
    {
        float zAngle = GameManager.Instance.JoystickManager.Angle;
        if (zAngle == 0f) return;

        attacker.transform.rotation = Quaternion.Euler(0f, 0f, zAngle);

        if (zAngle > 90f || zAngle < -90f)
        {
            playerModelRenderer.flipX = true;
        }
        else
        {
            playerModelRenderer.flipX = false;
        }
    }

    public float GetAttackDamage()
    {
        return data.ATK;
    }
}

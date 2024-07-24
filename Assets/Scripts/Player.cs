using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    [Header("무기관련")]
    [SerializeField] private Attacker attacker;

    [Header("렌더러")]
    [SerializeField] private SpriteRenderer playerModelRenderer;

    [Header("State")]
    [SerializeField] private Data data;
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;

    public Attacker Attacker { get => attacker; }
    public float CurHp { get => curHp; }
    public float MaxHp { get => data.HP; }

    //public Player(Data _data)
    //{
    //    data = new Data(_data.Item_List, _data.LV, _data.SC, _data.HP, _data.ATK, _data.DEF, _data.EXP, _data.AS, _data.HR, _data.AA);
    //    curHp = data.HP;
    //}

    private void OnEnable()
    {
        Data _data = GameManager.Instance.DataManager.data;
        data = new Data(_data.Item_List, _data.LV, _data.SC, _data.HP, _data.ATK, _data.DEF, _data.EXP, _data.AS, _data.HR);
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
        curHp -= damage;
    }

    public void AttackerRot()
    {
        float zAngle = GameManager.Instance.JoystickManager.Angle;
        if (zAngle == 0f || attacker.IsAttack) return;

        attacker.transform.rotation = Quaternion.Euler(0f, 0f, zAngle);

        if(zAngle > 90f || zAngle < -90f)
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

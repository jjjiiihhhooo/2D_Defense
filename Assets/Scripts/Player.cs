using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;


    [Header("무기관련")]
    [SerializeField] private Attacker attacker;

    [Header("렌더러")]
    [SerializeField] private SpriteRenderer playerModelRenderer;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        AttackerRot();
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
}

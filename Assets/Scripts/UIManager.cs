using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Slider playerHp;

    private void Update()
    {
        HpUpdate();
    }

    private void HpUpdate()
    {
        playerHp.value = Mathf.Lerp(playerHp.value, Player.Instance.CurHp / Player.Instance.MaxHp , Time.deltaTime * 5f);
    }

}

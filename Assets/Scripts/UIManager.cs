using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Slider playerHp;

    private Player player;

    private void Update()
    {
        if (player == null) return;
        HpUpdate();
    }

    private void HpUpdate()
    {
        if (playerHp == null) playerHp = GameManager.Instance.StageManager.playerHp_Slider;

        playerHp.value = Mathf.Lerp(playerHp.value, player.CurHp / player.MaxHp , Time.deltaTime * 5f);
    }


    public void GetPlayer(Player _player)
    {
        player = _player;
    }

}

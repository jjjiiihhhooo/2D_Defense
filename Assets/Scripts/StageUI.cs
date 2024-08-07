using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private Slider playerHp;
    [SerializeField] private Slider stageEXP;

    [SerializeField] private TextMeshProUGUI playerHpText;
    [SerializeField] private TextMeshProUGUI stageEXpText;
    [SerializeField] private StageManager stageManager;

    private Player player;

    public FixedJoystick joystick;

    private void Update()
    {
        HpUpdate();
        EXPUpdate();
    }

    private void HpUpdate()
    {
        playerHp.value = Mathf.Lerp(playerHp.value, player.CurHp / player.MaxHp, Time.deltaTime * 5f);
        playerHpText.text = (playerHp.value * 100).ToString("F0");
    }

    private void EXPUpdate()
    {
        stageEXP.value = Mathf.Lerp(stageEXP.value, stageManager.CurEXP / stageManager.MaxEXP, Time.deltaTime * 5f);
        stageEXpText.text = (stageEXP.value * 100).ToString("F0");
    }

    public void GetPlayer(Player p)
    {
        player = p;
    }
}

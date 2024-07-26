using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageCountText;

    public TextMeshProUGUI StageCountText { get => stageCountText; }

    private void Update()
    {
        TextUpdate();
    }

    private void TextUpdate()
    {
        stageCountText.text = "STAGE " + GameManager.Instance.DataManager.data.SC.ToString();
    }

}

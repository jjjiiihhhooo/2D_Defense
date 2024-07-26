using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int stageCount;
    [SerializeField] private int waveCount;
    [SerializeField] private int enemyKillCount;

    [SerializeField] private GameObject startMessage_obj;
    [SerializeField] private Wave[] waves;
    [SerializeField] private StageUI stageUI;

    private Wave curWave;

    private Player player;

    private int stageCurCount = 0;

    private float maxEXP = 100;
    private float curEXP = 0;

    public float MaxEXP { get => maxEXP; }
    public float CurEXP { get => curEXP; }


    public Transform[] waveSpawnTransforms;
    public Queue<int> enemyKillQueue;


    private void Awake()
    {
        enemyKillQueue = new Queue<int>();

        GameManager.Instance.GetStageManager(this);

        GameObject temp = Instantiate(GameManager.Instance.player_obj, Vector3.zero, Quaternion.identity);
        player = temp.GetComponent<Player>();
        GameManager.Instance.JoystickManager.GetJoystick(stageUI.joystick);
        stageUI.GetPlayer(player);
        startMessage_obj.SetActive(true);
        curEXP = 0;
        maxEXP = 100;
    }

   

    private void Update()
    {
        QueueCheck();
    }

    private void QueueCheck()
    {
        if (enemyKillQueue.Count > 0)
        {
            enemyKillCount++;
            enemyKillQueue.Dequeue();
        }
    }

    private void WaveDestroy()
    {
        stageCurCount++;
        enemyKillCount = 0;
        Destroy(curWave.gameObject);
        if (stageCurCount <= stageCount) StageStart();
        else StageClear();
    }

    private void StageClear()
    {
        Debug.LogError("StageClear");
        stageCount++;

    }

    public void StageStart()
    {
        if (stageCurCount > waves.Length) return;
        GameObject temp = Instantiate(waves[stageCurCount].gameObject, this.transform);
        curWave = temp.GetComponent<Wave>();
        curWave.WaveStart();
    }

    public void waveClear(int count)
    {
        if (enemyKillCount >= count) WaveDestroy();
    }

    public void GetStageEXP(float _exp)
    {
        curEXP += _exp;
        if (curEXP >= maxEXP)
        {
            curEXP = 0;
            maxEXP *= 2;
        }

    }
}

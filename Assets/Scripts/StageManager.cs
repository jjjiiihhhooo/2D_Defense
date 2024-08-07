using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int stageCount;
    [SerializeField] private int waveCount;
    [SerializeField] private int enemyKillCount;

    [SerializeField] private GameObject startMessage_obj;
    [SerializeField] private Wave[] waves;
    [SerializeField] private StageUI stageUI;
    [SerializeField] private Weapon[] weapons;


    private Wave curWave;

    private Player player;

    private int stageCurCount = 0;

    private float maxEXP = 100;
    private float curEXP = 0;

    public Player Player { get => player; }
    public float MaxEXP { get => maxEXP; }
    public float CurEXP { get => curEXP; }


    public Transform[] waveSpawnTransforms;
    public Queue<int> enemyKillQueue;


    private void Awake()
    {
        GetInit();
        SpawnPlayer();
        StageInit();
    }

    private void Update()
    {
        QueueCheck();
    }

    private void GetInit()
    {
        GameManager.Instance.GetStageManager(this);
        GameManager.Instance.JoystickManager.GetJoystick(stageUI.joystick);
    }

    private void SpawnPlayer()
    {
        GameObject temp = Instantiate(GameManager.Instance.player_obj, Vector3.zero, Quaternion.identity);

        player = temp.GetComponent<Player>();
    }

    private void StageInit()
    {
        enemyKillQueue = new Queue<int>();

        stageUI.GetPlayer(player);
        startMessage_obj.SetActive(true);

        curEXP = 0;
        maxEXP = 100;
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

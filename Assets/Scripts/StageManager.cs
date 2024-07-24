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

    [SerializeField] private Wave[] waves;

    private Wave curWave;

    private GameObject player;

    private int stageCurCount = 0;

    public Slider playerHp_Slider;
    public Transform[] waveSpawnTransforms;
    public Queue<int> enemyKillQueue;


    private void Awake()
    {
        enemyKillQueue = new Queue<int>();
        GameManager.Instance.GetStageManager(this);
        player = Instantiate(GameManager.Instance.player_obj, Vector3.zero, Quaternion.identity);
        GameManager.Instance.UIManager.GetPlayer(player.GetComponent<Player>());
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

    
}

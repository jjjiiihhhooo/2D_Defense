using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int stageCount;
    [SerializeField] private int waveCount;
    [SerializeField] private int enemyKillCount;

    [SerializeField] private Wave[] waves;

    private Wave curWave;

    private int stageCurCount = 0;

    public Transform[] waveSpawnTransforms;
    public Queue<int> enemyKillQueue;

    public void Init()
    {
        enemyKillQueue = new Queue<int>();
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
    }

    public void StageStart()
    {
        Debug.LogError("StageStart");
        GameObject temp = Instantiate(waves[stageCurCount].gameObject, this.transform);
        curWave = temp.GetComponent<Wave>();
        curWave.WaveStart();
    }

    public void waveClear(int count)
    {
        if (enemyKillCount >= count) WaveDestroy();
    }

    
}

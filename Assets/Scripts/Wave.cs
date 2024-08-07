using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private EnemyController[] enemys;

    [SerializeField] private int maxEnemyCount;
    [SerializeField] private int waveCount;

    public int WaveCount { get => waveCount; }

    private int enemyCount;

    private bool isWaveStart;

    private float enemyDelay = 0.3f;

    private float waveDelay = 0f;

    private int curWaveCount;

    private void Start()
    {
        curWaveCount = waveCount;
    }

    private void Update()
    {
        WaveExcute();
    }

    private void WaveExcute()
    {
        if (!isWaveStart) return;
        if (curWaveCount <= 0)
        {
            WaveClear();
            return;
        }

        if (waveDelay > 0)
        {
            waveDelay -= Time.deltaTime;
            return;
        }

        if (enemyDelay > 0)
        {
            enemyDelay -= Time.deltaTime;
        }
        else
        {
            enemyDelay = 0.3f;
            if (enemyCount < maxEnemyCount)
            {
                enemyCount++;

                GameObject enemy = SpawnEnemy();

                enemy.transform.position = RandomPosition();

                enemy.transform.parent = this.transform;
                enemy.SetActive(true);
            }
            else
            {
                enemyCount = 0;
                waveDelay = 3f;
                curWaveCount--;
            }
        }
    }

    private GameObject SpawnEnemy()
    {
        int rand = Random.Range(0, enemys.Length);

        return GameManager.Instance.ObjectPool.Dequeue(enemys[rand].name);
    }

    private Vector3 RandomPosition()
    {
        int rand = Random.Range(0, GameManager.Instance.StageManager.waveSpawnTransforms.Length);

        return GameManager.Instance.StageManager.waveSpawnTransforms[rand].position;
    }

    public void WaveStart()
    {
        isWaveStart = true;
    }

    private void WaveClear()
    {
        int killCount = maxEnemyCount * waveCount;
        GameManager.Instance.StageManager.waveClear(killCount);
    }
}

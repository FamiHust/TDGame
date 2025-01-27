
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public Timer timer;

    public Transform[] spawnPoints;
    public EnemyPrefabProb[] enemyPrefabs;
    private List<GameObject> probList = new List<GameObject>();

    private float coolDownTimer;
    private int currentEnemyCount = 0;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float coolDown = 10.0f;

    private void Awake()
    {
        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }
    }
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 10, coolDown);

        foreach (EnemyPrefabProb prefabProb in enemyPrefabs)
        {
            for (int i = 0; i < prefabProb.probability; i++)
                probList.Add(prefabProb.prefab);
        }

        coolDownTimer = timer.speedManage;
    }

    void Update()
    {
        if (coolDownTimer != timer.speedManage)
        {
            Debug.Log("");
            UpdateCoolDown();
            coolDownTimer = timer.speedManage;
        }
    }

    void UpdateCoolDown()
    {
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", 10, coolDown / timer.speedManage);
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount < maxEnemies)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject myEnemy = Instantiate(probList[Random.Range(0, probList.Count)], spawnPoints[spawnPointIndex].position, Quaternion.identity);
            currentEnemyCount++;
        }
    }

    public void EnemyDestroyed()
    {
        currentEnemyCount--;
    }
}

[System.Serializable]
public class EnemyPrefabProb
{
    public GameObject prefab;
    public int probability;
}

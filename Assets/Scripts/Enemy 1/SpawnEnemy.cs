using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// public class EnemySpawner : MonoBehaviour
// {
//     public Transform[] spawnPoints;
//     public GameObject enemyPrefabs;
//     public EnemyTypeProb[] enemyTypes;
//     private List<EnemyType> probList = new List<EnemyType>();

//     private void Start()
//     {
//         InvokeRepeating("SpawnEnemy", 2, 10);

//         foreach (EnemyTypeProb enem in enemyTypes)
//         {
//             for (int i = 0; i < enem.probability; i++)
//                 probList.Add(enem.type);
//         }
//     }

//     void SpawnEnemy()
//     {
//         int r = Random.Range(0, spawnPoints.Length);

//         GameObject myEnemy = Instantiate(enemyPrefabs, spawnPoints[r].position, Quaternion.identity);

//         myEnemy.GetComponent<EnemyController>().type = probList[Random.Range(0, probList.Count)];
//     }
// }

// [System.Serializable]
// public class EnemyTypeProb
// {
//     public EnemyType type;
//     public int probability;
// }

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public EnemyPrefabProb[] enemyPrefabs; // Thay đổi từ GameObject[] thành EnemyPrefabProb[]
    private List<GameObject> probList = new List<GameObject>();


    public Timer timer;

    private float coolDownTimer;
    [SerializeField] private float coolDown = 12.0f;

    private void Awake()
    {
        if (timer == null)
        {
            timer = FindObjectOfType<Timer>();
        }
    }
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 15, coolDown );

        foreach (EnemyPrefabProb prefabProb in enemyPrefabs)
        {
            for (int i = 0; i < prefabProb.probability; i++)
                probList.Add(prefabProb.prefab);
        }

        coolDownTimer = timer.speedManage;
    }

    void Update()
    {
        if(coolDownTimer != timer.speedManage)
        {
            Debug.Log("");
            UpdateCoolDown();
            coolDownTimer = timer.speedManage;
        }
    }

    void UpdateCoolDown()
    {
        CancelInvoke("SpawnEnemy");
        InvokeRepeating("SpawnEnemy", 15, coolDown / timer.speedManage);
    }
    void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        GameObject myEnemy = Instantiate(probList[Random.Range(0, probList.Count)], spawnPoints[spawnPointIndex].position, Quaternion.identity);

        // Nếu bạn cần gán loại cho enemy, bạn có thể làm như sau:
        // myEnemy.GetComponent<EnemyController>().type = ...; // Gán loại nếu cần
    }
}

[System.Serializable]
public class EnemyPrefabProb
{
    public GameObject prefab; // Prefab enemy
    public int probability; // Xác suất
}
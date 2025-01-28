using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnSkills : MonoBehaviour
{
    public GameObject objectToSpawn;
    public List<Transform> spawnPoints;
    public Button spawnButton;

    [SerializeField] private int maxSpawnCount = 10;
    [SerializeField] private float spawnCooldown = 0.5f;
    private bool isSpawning = false;

    void Start()
    {
        spawnButton.onClick.AddListener(() =>
        {
            if (!isSpawning)
            {
                StartCoroutine(SpawnAllObjectsWithCooldown());
            }
        });
    }

    private IEnumerator SpawnAllObjectsWithCooldown()
    {
        isSpawning = true;

        for (int i = 0; i < maxSpawnCount; i++)
        {
            int randomIndex = Random.Range(0, spawnPoints.Count);
            Transform randomSpawnPoint = spawnPoints[randomIndex];
            Instantiate(objectToSpawn, randomSpawnPoint.position, randomSpawnPoint.rotation);

            yield return new WaitForSeconds(spawnCooldown);
        }

        isSpawning = false;
    }
}

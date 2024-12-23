using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinObject;

    private void Start()
    {
        SpawnCoin();
    }
    
    void SpawnCoin()
    {
        GameObject myCoin = Instantiate(coinObject, new Vector3(Random.Range(-5f, 5f), 11, 0), Quaternion.identity);
        myCoin.GetComponent<Coin>().dropToYPos = Random.Range(0, 10);
        Invoke("SpawnCoin", Random.Range(-8f, 10f));
    }
}

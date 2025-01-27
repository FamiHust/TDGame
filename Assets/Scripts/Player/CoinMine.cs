using UnityEngine;

public class CoinMine : MonoBehaviour
{
    public GameObject coinObject;
    public float coolDown;

    private void Start()
    {
        InvokeRepeating("SpawnCoin", coolDown, coolDown);
    }

    void SpawnCoin()
    {
        GameObject myCoin = Instantiate(
            coinObject,
            new Vector3(
                transform.position.x + Random.Range(-1f, 1f),
                transform.position.y + Random.Range(2f, 3f),
                0
            ),
            Quaternion.identity
        );

        Rigidbody2D rb = myCoin.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }

        // Nếu coin có một thuộc tính để dừng tại vị trí `dropToYPos`
        Coin coin = myCoin.GetComponent<Coin>();
        if (coin != null)
        {
            coin.dropToYPos = transform.position.y - 1;
        }
    }
}

using UnityEngine;

public class CoinMine : MonoBehaviour
{
    public GameObject coinObject; // Prefab của đồng xu
    public float coolDown; // Thời gian spawn

    private void Start()
    {
        InvokeRepeating("SpawnCoin", coolDown, coolDown);
    }

    void SpawnCoin()
    {
        // Tạo đối tượng coin tại vị trí ngẫu nhiên
        GameObject myCoin = Instantiate(
            coinObject,
            new Vector3(
                transform.position.x + Random.Range(-1f, 1f),
                transform.position.y + Random.Range(2f, 3f),
                0
            ),
            Quaternion.identity
        );

        // Thiết lập vận tốc rơi
        Rigidbody2D rb = myCoin.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f; // Tăng tốc độ rơi (giá trị có thể tùy chỉnh)
        }

        // Nếu coin có một thuộc tính để dừng tại vị trí `dropToYPos`
        Coin coin = myCoin.GetComponent<Coin>();
        if (coin != null)
        {
            coin.dropToYPos = transform.position.y - 1;
        }
    }
}

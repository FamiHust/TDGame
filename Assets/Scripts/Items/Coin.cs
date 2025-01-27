using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    public float dropToYPos;
    private bool hasStopped = false;

    void Start()
    {
        Destroy(gameObject, Random.Range(0, 20f));
    }

    void Update()
    {
        // Nếu đồng xu chưa chạm đến dropToYPos, thì tiếp tục rơi
        if (!hasStopped && transform.position.y > dropToYPos)
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
        }
        else if (!hasStopped)
        {
            // Đảm bảo đồng xu dừng tại đúng vị trí
            transform.position = new Vector3(transform.position.x, dropToYPos, transform.position.z);
            hasStopped = true; // Đánh dấu là đồng xu đã dừng
        }
    }
}

// using UnityEngine;

// public class Coin : MonoBehaviour
// {
//     public float dropToYPos;
//     private float speed = 2f;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         Destroy(gameObject, Random.Range(0, 20));
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (transform.position.y > dropToYPos)
//             transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);


//     }
// }
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float dropToYPos; // Vị trí Y nơi đồng xu sẽ dừng lại
    [SerializeField] private float speed = 2f; // Tốc độ rơi xuống
    private bool hasStopped = false; // Trạng thái để đảm bảo đồng xu dừng tại dropToYPos

    void Start()
    {
        // Hủy đồng xu sau một thời gian dài để tránh rác bộ nhớ
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

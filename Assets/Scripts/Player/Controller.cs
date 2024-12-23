using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // Tốc độ di chuyển
    [SerializeField] private float gridSize = 1f; // Kích thước ô
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Kiểm tra phím nhấn
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2.left);
        }
    }

    private void Move(Vector2 direction)
    {
        // Tính toán vị trí mới
        Vector2 newPosition = rb.position + direction * gridSize;
        rb.MovePosition(newPosition);
    }
}
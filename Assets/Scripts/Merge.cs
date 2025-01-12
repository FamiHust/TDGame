using UnityEngine;
using System.Collections;

public class Merge : MonoBehaviour
{
    public GameObject prefabToInstantiate; // Prefab mới sẽ được tạo ra
    private GameObject currentObject; // Đối tượng hiện tại
    private Vector3 originalPosition; // Vị trí ban đầu của đối tượng
    private SpriteRenderer spriteRenderer; // SpriteRenderer của đối tượng
    public LayerMask mergeableLayer; // Chỉ định Layer cho các đối tượng có thể hợp nhất
    private int originalSortingOrder; // Thứ tự sắp xếp ban đầu
    private bool isDragging = false; // Trạng thái kéo
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSortingOrder = spriteRenderer.sortingOrder; // Lưu thứ tự sắp xếp ban đầu
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        if (!isDragging) // Chỉ thực hiện nếu không đang kéo
        {
            currentObject = gameObject;
            originalPosition = currentObject.transform.position;

            spriteRenderer.sortingOrder = 10; // Đưa đối tượng lên trên cùng
            isDragging = true; // Đánh dấu là đang kéo
            boxCollider2D.size = new Vector2(2f, 2f);
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging && currentObject != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Đặt z = 0 cho 2D
            currentObject.transform.position = mousePosition;
        }
    }

    private void OnMouseUp()
    {
        if (isDragging && currentObject != null)
        {
            // Kiểm tra va chạm với các đối tượng khác
            Collider2D[] colliders = Physics2D.OverlapCircleAll(currentObject.transform.position, 1f, mergeableLayer);
            bool foundOtherObject = false;

            foreach (var collider in colliders)
            {
                if (collider.gameObject != currentObject && collider.gameObject.name == currentObject.name)
                {
                    SoundManager.PlaySound(SoundType.SELECT);
                    foundOtherObject = true;
                    // Tạo GameObject mới tại vị trí của đối tượng được kéo thả
                    Instantiate(prefabToInstantiate, collider.transform.position, Quaternion.identity);

                    // Hủy các GameObject cũ
                    Destroy(currentObject);
                    Destroy(collider.gameObject);
                    break;
                }
            }

            if (!foundOtherObject)
            {
                currentObject.transform.position = originalPosition; // Đưa về vị trí ban đầu nếu không hợp nhất
            }

            spriteRenderer.sortingOrder = originalSortingOrder; // Khôi phục thứ tự sắp xếp ban đầu
            isDragging = false; // Đặt lại trạng thái kéo

        }
    }

    // Phương thức đặt trạng thái idle
    public void SetIdleState()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("isIdle"); // Đặt trạng thái về idle
        }
    }

}

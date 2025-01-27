using UnityEngine;
using System.Collections;

public class Merge : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public LayerMask mergeableLayer;
    private GameObject currentObject;
    private Vector3 originalPosition;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private int originalSortingOrder;
    private bool isDragging = false;

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
                    SoundManager.PlaySound(SoundType.MERGE);
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

    public void SetIdleState()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("isIdle"); // Đặt trạng thái về idle
        }
    }

}

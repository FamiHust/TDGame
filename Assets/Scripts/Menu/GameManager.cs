using UnityEngine;
using TMPro;
using UnityEngine.UI; // Thêm namespace này để sử dụng Button

public class GameManager : MonoBehaviour
{
    public GameObject currentPlayer;
    public Sprite currentPlayerSprite;
    public Transform tiles;
    public LayerMask tileMask;
    public LayerMask coinMask;
    public int coins;
    public TextMeshProUGUI coinText;
    public Button buyButton; // Thêm biến để tham chiếu đến nút mua

    private bool isPlayerBought;

    private void Update()
    {
        coinText.text = coins.ToString() + "$";

        // Get mouse position and validate it
        Vector3 mousePosition = Input.mousePosition;
        if (mousePosition.x < 0 || mousePosition.y < 0 ||
            mousePosition.x > Screen.width || mousePosition.y > Screen.height)
        {
            return; // Skip raycast if mouse is outside screen
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        // Add z-coordinate explicitly
        worldPoint.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, tileMask);

        foreach (Transform tile in tiles)
            tile.GetComponent<SpriteRenderer>().enabled = false;

        if (hit.collider && currentPlayer)
        {
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentPlayerSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;

            if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().hasPlayer)
            {
                SoundManager.PlaySound(SoundType.TILE);
                GameObject playerInstance = Instantiate(currentPlayer, hit.collider.transform.position, Quaternion.identity);
                hit.collider.GetComponent<Tile>().hasPlayer = true;
                hit.collider.GetComponent<Tile>().currentPlayer = playerInstance; // Lưu trữ player vào tile

                // Trừ coin khi đặt player thành công
                coins -= playerInstance.GetComponent<Player>().price;

                currentPlayer = null;
                currentPlayerSprite = null;
                isPlayerBought = false; // Reset trạng thái
            }
        }

        RaycastHit2D coinHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, coinMask);

        if (coinHit.collider)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SoundManager.PlaySound(SoundType.SELECT);
                coins += 1;
                Destroy(coinHit.collider.gameObject);
            }
        }

        // Kiểm tra xem có player nào bị destroy không
        foreach (Transform tile in tiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            if (tileComponent.hasPlayer && tileComponent.currentPlayer == null) // Nếu không tìm thấy player
            {
                tileComponent.ResetTile(); // Reset tile
            }
        }
    }

    public void BuyPlayer(GameObject player, Sprite sprite)
    {
        if (!isPlayerBought)
        {
            currentPlayer = player;
            currentPlayerSprite = sprite;
            isPlayerBought = true; // Đánh dấu là đã mua
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize raycast in Scene view
        Vector3 mousePos = Input.mousePosition;
        if (mousePos.x >= 0 && mousePos.y >= 0 &&
            mousePos.x <= Screen.width && mousePos.y <= Screen.height)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
            worldPoint.z = 0;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(worldPoint, 0.1f);
        }
    }
}
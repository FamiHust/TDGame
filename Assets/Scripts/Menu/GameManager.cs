// using UnityEngine;
// using TMPro;
// using UnityEngine.UI; // Thêm namespace này để sử dụng Button

// public class GameManager : MonoBehaviour
// {
//     public GameObject currentPlayer;
//     public Sprite currentPlayerSprite;
//     public Transform tiles;
//     public LayerMask tileMask;
//     public LayerMask coinMask;
//     public int coins;
//     public TextMeshProUGUI coinText;
//     public Button buyButton; // Thêm biến để tham chiếu đến nút mua

//     private void Update()
//     {
//         coinText.text = coins.ToString() + "$";

//         RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

//         foreach (Transform tile in tiles)
//             tile.GetComponent<SpriteRenderer>().enabled = false;

//         if (hit.collider && currentPlayer)
//         {
//             hit.collider.GetComponent<SpriteRenderer>().sprite = currentPlayerSprite;
//             hit.collider.GetComponent<SpriteRenderer>().enabled = true;

//             if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().hasPlayer)
//             {
//                 SoundManager.PlaySound(SoundType.SELECT);
//                 GameObject playerInstance = Instantiate(currentPlayer, hit.collider.transform.position, Quaternion.identity);
//                 hit.collider.GetComponent<Tile>().hasPlayer = true;
//                 hit.collider.GetComponent<Tile>().currentPlayer = playerInstance; // Lưu trữ player vào tile
//                 currentPlayer = null;
//                 currentPlayerSprite = null;
//             }
//         }

//         RaycastHit2D coinHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, coinMask);

//         if (coinHit.collider)
//         {
//             if (Input.GetMouseButtonDown(0))
//             {
//                 SoundManager.PlaySound(SoundType.SELECT);
//                 coins += 1;
//                 Destroy(coinHit.collider.gameObject);
//             }
//         }

//         // Kiểm tra xem có player nào bị destroy không
//         foreach (Transform tile in tiles)
//         {
//             Tile tileComponent = tile.GetComponent<Tile>();
//             if (tileComponent.hasPlayer && tileComponent.currentPlayer == null) // Nếu không tìm thấy player
//             {
//                 tileComponent.ResetTile(); // Reset tile
//             }
//         }
//     }

//     public void BuyPlayer(GameObject player, Sprite sprite)
//     {
//         currentPlayer = player;
//         currentPlayerSprite = sprite;
//     }
// }
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

    private void Update()
    {
        coinText.text = coins.ToString() + "$";

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileMask);

        foreach (Transform tile in tiles)
            tile.GetComponent<SpriteRenderer>().enabled = false;

        if (hit.collider && currentPlayer)
        {
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentPlayerSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;

            if (Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().hasPlayer)
            {
                SoundManager.PlaySound(SoundType.SELECT);
                GameObject playerInstance = Instantiate(currentPlayer, hit.collider.transform.position, Quaternion.identity);
                hit.collider.GetComponent<Tile>().hasPlayer = true;
                hit.collider.GetComponent<Tile>().currentPlayer = playerInstance; // Lưu trữ player vào tile
                currentPlayer = null;
                currentPlayerSprite = null;
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
        currentPlayer = player;
        currentPlayerSprite = sprite;
    }
}
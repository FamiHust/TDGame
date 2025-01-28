using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject currentPlayer;
    public Sprite currentPlayerSprite;
    public Transform tiles;
    public LayerMask tileMask;
    public LayerMask coinMask;
    public TextMeshProUGUI coinText;
    // public Button buyButton;

    [SerializeField] private int coins;

    private bool isPlayerBought;

    private void Update()
    {
        coinText.text = coins.ToString() + "$";

        // Get mouse position and validate it
        Vector3 mousePosition = Input.mousePosition;
        if (mousePosition.x < 0 || mousePosition.y < 0 ||
            mousePosition.x > Screen.width || mousePosition.y > Screen.height)
        {
            return;
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
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
                Player playerScript = currentPlayer.GetComponent<Player>();

                // Check if the player has enough coins
                if (playerScript != null && coins >= playerScript.price)
                {
                    SoundManager.PlaySound(SoundType.TILE);
                    GameObject playerInstance = Instantiate(currentPlayer, hit.collider.transform.position, Quaternion.identity);
                    hit.collider.GetComponent<Tile>().hasPlayer = true;
                    hit.collider.GetComponent<Tile>().currentPlayer = playerInstance;

                    coins -= playerScript.price;

                    currentPlayer = null;
                    currentPlayerSprite = null;
                    isPlayerBought = false;
                }
            }
        }

        // Raycast to collect coins
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

        // Check if any player is destroyed
        foreach (Transform tile in tiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            if (tileComponent.hasPlayer && tileComponent.currentPlayer == null)
            {
                tileComponent.ResetTile();
            }
        }
    }


    public void BuyPlayer(GameObject player, Sprite sprite)
    {
        Player playerScript = player.GetComponent<Player>();

        if (playerScript != null && coins >= playerScript.price)
        {
            if (!isPlayerBought)
            {
                currentPlayer = player;
                currentPlayerSprite = sprite;
                isPlayerBought = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
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

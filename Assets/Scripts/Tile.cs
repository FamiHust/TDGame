using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool hasPlayer;
    public GameObject currentPlayer; // Lưu trữ player hiện tại

    public void ResetTile()
    {
        hasPlayer = false;
        currentPlayer = null; // Reset player
        GetComponent<SpriteRenderer>().sprite = null; // Hoặc sprite mặc định nếu cần
        GetComponent<SpriteRenderer>().enabled = false; // Ẩn sprite nếu cần
    }
}




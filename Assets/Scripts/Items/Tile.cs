using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool hasPlayer;
    public GameObject currentPlayer;

    public void ResetTile()
    {
        hasPlayer = false;
        currentPlayer = null;
        GetComponent<SpriteRenderer>().sprite = null;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}




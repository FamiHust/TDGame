using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public GameObject DeletePanel;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            openDeletePanel();
        }
    }

    public void deleteObject()
    {
        SoundManager.PlaySound(SoundType.TILE);

        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            GameManager.Instance.coins += playerScript.sell; 
        }
        
        Destroy(player);
        closeDeletePanel();
    }

    public void openDeletePanel()
    {
        DeletePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void closeDeletePanel()
    {
        DeletePanel.SetActive(false);
        Time.timeScale = 1f;
    }

}

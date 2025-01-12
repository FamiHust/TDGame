using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSlot : MonoBehaviour
{
    public Sprite playerSprite;

    public GameObject playerObject;

    public int price;

    public Image icon;

    public TextMeshProUGUI priceText;

    private GameManager gms;

    private void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(BuyPlayer);
    }

    private void BuyPlayer()
    {
        if (gms.coins >= price && !gms.currentPlayer)
        {
            gms.coins -= price;
            gms.BuyPlayer(playerObject, playerSprite);
        }
    }

    private void OnValidate()
    {
        if (playerSprite)
        {
            icon.enabled = true;
            icon.sprite = playerSprite;
            priceText.text = price.ToString() + "$";
        }
        else
        {
            icon.enabled = false;
        }
    }
}

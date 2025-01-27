using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSlot : MonoBehaviour
{
    private GameManager gms;

    public Sprite playerSprite;
    public GameObject playerObject;
    public Image icon;
    public TextMeshProUGUI priceText;
    public int price;

    private void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(() => gms.BuyPlayer(playerObject, playerSprite));
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

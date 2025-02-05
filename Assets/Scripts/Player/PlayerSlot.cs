using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSlot : MonoBehaviour
{
    public Sprite playerSprite;
    public GameObject playerObject;
    public Image icon;
    public TextMeshProUGUI priceText;
    public int price;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.BuyPlayer(playerObject, playerSprite));
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

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set;}

    public Canvas parentCanvas; 
    public Transform TooltipTransform;
    public TextMeshProUGUI Title, Details;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Giới hạn vị trí chuột để đảm bảo nó nằm trong giới hạn màn hình
        mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
        mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);

        // Gán vị trí đã giới hạn cho tooltip
        TooltipTransform.position = mousePosition;
    }

    public void Show(string TitleText, string DetailsText)
    {
        Title.text = TitleText;
        Details.text = DetailsText;
        TooltipTransform.gameObject.SetActive(true);
    }

    public void Hide()
    {
        TooltipTransform.gameObject.SetActive(false);
    }
}

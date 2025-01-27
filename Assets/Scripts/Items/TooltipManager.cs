using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public Canvas parentCanvas; // Không cần sử dụng nếu camera Overlay
    public Transform TooltipTransform;
    public TextMeshProUGUI Title, Details;
    public static TooltipManager Instance;

    private void Start()
    {
        Instance = this;
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

// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class TooltipManager : MonoBehaviour
// {
//     public Canvas parentCanvas;
//     public Transform TooltipTransform;

//     private void Start()
//     {

//     }

//     private void Update()
//     {
//         Vector2 movePos;

//         RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Input.mousePosition, parentCanvas.worldCamera, out movePos);

//         TooltipTransform.position = parentCanvas.transform.TransformPoint(movePos);

//         TooltipTransform.SetAsLastSibling();
//     }
// }
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public Canvas parentCanvas; // Không cần sử dụng nếu camera Overlay
    public Transform TooltipTransform;
    public static TooltipManager Instance;
    public TextMeshProUGUI Title, Details;

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

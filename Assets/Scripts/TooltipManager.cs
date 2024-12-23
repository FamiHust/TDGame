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
        // Chuyển vị trí tooltip tới vị trí chuột
        TooltipTransform.position = Input.mousePosition;

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

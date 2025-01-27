using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipDetails : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string TitleText;
    public string DetailsText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Instance.Show(TitleText, DetailsText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }

}

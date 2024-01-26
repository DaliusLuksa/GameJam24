using UnityEngine;
using UnityEngine.EventSystems;

public class WorkshopImageController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    public TownUI townUI;
    public void OnPointerClick(PointerEventData eventData)
    {
        townUI.OnWorkshopImageClicked();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       transform.localScale *= 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale /= 1.1f;
    }
}
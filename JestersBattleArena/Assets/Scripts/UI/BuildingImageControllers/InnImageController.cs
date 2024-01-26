using UnityEngine;
using UnityEngine.EventSystems;

public class InnImageController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    public TownUI townUI;
    public void OnPointerClick(PointerEventData eventData)
    {
        townUI.OnInnImageClicked();
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
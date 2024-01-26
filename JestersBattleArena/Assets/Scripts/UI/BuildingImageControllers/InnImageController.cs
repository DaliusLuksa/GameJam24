using UnityEngine;
using UnityEngine.EventSystems;

public class InnImageController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TownUI townUI;
    private Vector3 initialTransformScale;

    private void Awake() {
        initialTransformScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        townUI.OnInnImageClicked();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       transform.localScale = initialTransformScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = initialTransformScale;
    }
}
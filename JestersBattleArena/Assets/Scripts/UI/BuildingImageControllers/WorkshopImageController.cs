using UnityEngine;
using UnityEngine.EventSystems;

public class WorkshopImageController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TownUI townUI;

    private Vector3 initialTransformScale;

    private void Awake() {
        initialTransformScale = transform.localScale;
    }

    private void OnDisable()
    {
        ResetWorkshopHoverEffect();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        townUI.OnWorkshopImageClicked();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       transform.localScale = initialTransformScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetWorkshopHoverEffect();
    }

    private void ResetWorkshopHoverEffect()
    {
        transform.localScale = initialTransformScale;
    }
}
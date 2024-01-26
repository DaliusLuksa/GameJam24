using UnityEngine;
using UnityEngine.EventSystems;

public class PalaceImageController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TownUI townUI;

    private Vector3 initialTransformScale;

    private void Awake() {
        initialTransformScale = transform.localScale;
    }

    private void OnDisable()
    {
        ResetPalaceHoverEffect();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        townUI.OnPalaceImageClicked();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       transform.localScale = initialTransformScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetPalaceHoverEffect();
    }

    private void ResetPalaceHoverEffect()
    {
        transform.localScale = initialTransformScale;
    }
}
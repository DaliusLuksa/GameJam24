using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StorageItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemSlotImage;
    [SerializeField] private TextMeshProUGUI itemNameText;

    private ItemInfoWindowUI itemInfoWindowUI = null;
    private InventoryItem item = null;

    private void Awake()
    {
        itemInfoWindowUI = FindObjectOfType<ItemInfoWindowUI>();
    }

    public void SetupStorageItemSlot(InventoryItem itemToSetup)
    {
        item = itemToSetup;
        itemSlotImage.sprite = item.ItemSO.Icon;
        itemNameText.text = item.ItemSO.Name;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoWindowUI.UpdateInfo(item);
        itemInfoWindowUI.Root.SetActive(true);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoWindowUI.Root.SetActive(false);
    }
}

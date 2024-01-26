using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StorageItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemSlotImage;
    [SerializeField] private TextMeshProUGUI itemNameText;

    private InventoryItem item = null;

    public void SetupStorageItemSlot(InventoryItem itemToSetup)
    {
        item = itemToSetup;
        itemSlotImage.sprite = item.ItemSO.Icon;
        itemNameText.text = item.ItemSO.Name;
    }
}

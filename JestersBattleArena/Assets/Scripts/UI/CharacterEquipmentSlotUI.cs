using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CharacterEquipmentSlot
{
    Helmet,
    Plate,
    Pants,
    Boots,
    MainHand,
    OffHand,
    None
}

public class CharacterEquipmentSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image helmetSlot;
    [SerializeField] private Image plateSlot;
    [SerializeField] private Image pantsSlot;
    [SerializeField] private Image bootsSlot;
    [SerializeField] private Image mainHandSlot;
    [SerializeField] private Image offHandSlot;

    private ItemInfoWindowUI itemInfoWindowUI = null;
    private InventoryItem item;

    private void Awake()
    {
        itemInfoWindowUI = FindObjectOfType<ItemInfoWindowUI>();
    }

    public InventoryItem SetupSlot(InventoryItem itemToSetup, CharacterEquipmentSlot slot)
    {
        InventoryItem itemToReturn = item != null ? item : null;
        item = itemToSetup;

        switch (slot)
        {
            case CharacterEquipmentSlot.Helmet:
                helmetSlot.sprite = item.ItemSO.Icon;
                break;
            case CharacterEquipmentSlot.Plate:
                plateSlot.sprite = item.ItemSO.Icon;
                break;
            case CharacterEquipmentSlot.Pants:
                pantsSlot.sprite = item.ItemSO.Icon;
                break;
            case CharacterEquipmentSlot.Boots:
                bootsSlot.sprite = item.ItemSO.Icon;
                break;
            case CharacterEquipmentSlot.MainHand:
                mainHandSlot.sprite = item.ItemSO.Icon;
                break;
            case CharacterEquipmentSlot.OffHand:
                offHandSlot.sprite = item.ItemSO.Icon;
                break;
        }

        return itemToReturn;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null) { return; }
        itemInfoWindowUI.UpdateInfo(item);
        itemInfoWindowUI.Root.SetActive(true);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoWindowUI.Root.SetActive(false);
    }

    private void OnDestroy()
    {
        itemInfoWindowUI.Root.SetActive(false);
    }
}

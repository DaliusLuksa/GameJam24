using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StorageItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image itemSlotImage;
    [SerializeField] private TextMeshProUGUI itemNameText;

    private MainGameManager mainGameManager = null;
    private ItemInfoWindowUI itemInfoWindowUI = null;
    private InventoryItem item = null;

    public InventoryItem Item => item;

    private void Awake()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();
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

    private void OnDestroy()
    {
        itemInfoWindowUI.Root.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CharacterEquipmentSlot slot = CharacterEquipmentSlot.None;
        if (item.ItemSO is Weapon)
        {
            Weapon weaponItem = item.ItemSO as Weapon;
            if (weaponItem.WeaponType == WeaponType.Melee || weaponItem.WeaponType == WeaponType.Ranged)
            {
                slot = CharacterEquipmentSlot.MainHand;
            }
            else
            {
                slot = CharacterEquipmentSlot.OffHand;
            }
        }
        else
        {
            Armor armorWeapon = item.ItemSO as Armor;
            switch (armorWeapon.ArmorType)
            {
                case ArmorType.Helmet:
                    slot = CharacterEquipmentSlot.Helmet;
                    break;
                case ArmorType.Chest:
                    slot = CharacterEquipmentSlot.Plate;
                    break;
                case ArmorType.Pants:
                    slot = CharacterEquipmentSlot.Pants;
                    break;
                case ArmorType.Boots:
                    slot = CharacterEquipmentSlot.Boots;
                    break;
            }
        }

        mainGameManager.MainPlayer.EquipItem(item, slot, mainGameManager.BarracksUI.GetCorrectEquipmentSlotUI(slot));
    }
}

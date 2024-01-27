using UnityEngine;

public enum ArmorType
{
    Helmet,
    Chest,
    Pants,
    Boots
}

[CreateAssetMenu(fileName = "New Armor Item", menuName = "Items/Armor Item")]
public class Armor : Item
{
    public Armor() {
        itemType = ItemType.Armor;
    }
    public ArmorType armorType;
}

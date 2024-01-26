using UnityEngine;

public enum ArmorType
{
    Helmet,
    Plate,
    Pants,
    Boots
}

[CreateAssetMenu(fileName = "New Armor Item", menuName = "Items/Armor Item")]
public class Armor : Item
{
}

using UnityEngine;

public enum WeaponType
{
    Melee,
    Ranged,
    Shield
}

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Items/Weapon Item")]
public class Weapon : Item
{
    [SerializeField] private WeaponType weaponType = WeaponType.Melee;
    [SerializeField] private int critRate;
    [SerializeField] private int critDamage;
    [SerializeField] [Min(0)] private float attackSpeed;
}

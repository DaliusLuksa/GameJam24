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
    public Weapon() {
        itemType = ItemType.Weapon;
    }
    [SerializeField] public WeaponType weaponType = WeaponType.Melee;
    [SerializeField] private int critRate;
    [SerializeField] private int critDamage;
    [SerializeField] [Min(0)] private float attackSpeed;

    public int CritRate => critRate;
    public int CritDamage => critDamage;
    public float AttackSpeed => attackSpeed;
}

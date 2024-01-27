using TMPro;
using UnityEngine;

public class ItemInfoWindowUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI AtkDefStatText;
    [SerializeField] private TextMeshProUGUI CritStatText;
    [SerializeField] private TextMeshProUGUI AdditionalStatsText;

    public GameObject Root => root;

    public void UpdateInfo(Item item)
    {
        itemNameText.text = item.Name;
        if (item is Weapon)
        {
            AtkDefStatText.text = $"Atk - {item.MinAttackDamage}~{item.MaxAttackDamage}";
            CritStatText.text = $"CrtR - {(item as Weapon).CritRate} / CrtDmg - {(item as Weapon).CritDamage}";
        }
        else
        {
            AtkDefStatText.text = $"Def - {item.MaxDefense}";
        }
    }
}

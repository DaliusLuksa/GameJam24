using System.Text;
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
            CritStatText.text = $"CrtR - {(item as Weapon).CritRate}% / CrtDmg - {(item as Weapon).CritDamage}%";
        }
        else
        {
            AtkDefStatText.text = $"Def - {item.MaxDefense}";
            CritStatText.text = string.Empty;
        }

        AdditionalStatsText.text = string.Empty;
    }

    public void UpdateInfo(InventoryItem item)
    {
        itemNameText.text = item.ItemName;
        if (item.ItemSO is Weapon)
        {
            AtkDefStatText.text = $"Atk - {item.AttackDamage}";
            CritStatText.text = $"CrtR - {(item.ItemSO as Weapon).CritRate}% / CrtDmg - {(item.ItemSO as Weapon).CritDamage}%";
            StringBuilder newString = new StringBuilder();
            foreach (CharacterStat gainedStat in item.GainedStats)
            {
                newString.AppendLine($"{gainedStat.name} - {gainedStat.value}");
            }
            AdditionalStatsText.text = newString.ToString();
        }
        else
        {
            AtkDefStatText.text = $"Def - {item.Defense}";
            CritStatText.text = string.Empty;
            StringBuilder newString = new StringBuilder();
            foreach (CharacterStat gainedStat in item.GainedStats)
            {
                newString.AppendLine($"{gainedStat.name} - {gainedStat.value}");
            }
            AdditionalStatsText.text = newString.ToString();
        }
    }
}
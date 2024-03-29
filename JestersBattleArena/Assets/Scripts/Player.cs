using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    [SerializeField] private string itemName;
    [SerializeField] private int attackDamage = 0;
    [SerializeField] private int defense = 0;
    [SerializeField] private int weight;
    [SerializeField] private List<CharacterStat> gainedStats;

    public Item ItemSO { get; private set; } = null;
    public int Weight => weight;
    public string ItemName => itemName;
    public int AttackDamage => attackDamage;
    public int Defense => defense;
    public List<CharacterStat> GainedStats => gainedStats;

    public InventoryItem(Item newItem)
    {
        ItemSO = newItem;
        gainedStats = new List<CharacterStat>();
        itemName = newItem.Name;
        weight = newItem.Weight;

        if (newItem is Weapon)
        {
            attackDamage = Random.Range(newItem.MinAttackDamage, newItem.MaxAttackDamage + 1);
        }
        else
        {
            defense = Random.Range(newItem.MinDefense, newItem.MaxDefense + 1);
        }

        foreach (RandomStat randomStat in newItem.OptionalRandomStats)
        {
            var randomNumber = Random.Range(0.0f, 1.0f);
            if (randomNumber <= randomStat.chanceToGive)
            {
                var randomStatNumber = Random.Range(randomStat.minValue, randomStat.maxValue + 1);
                // Don't add it if we rolled 0
                if (randomStatNumber == 0)
                {
                    continue;
                }

                gainedStats.Add(new CharacterStat() {
                    name = randomStat.name,
                    charStat = randomStat.stat,
                    value = randomStatNumber
                });
            }
        }
    }
}

public class Player : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> playerInventory;
    [SerializeField] private Dictionary<Resource, int> playerResources;
    [SerializeField] private int maxInventoryWeight = 30;

    private Character characterSO;

    public int HealthPoints { get; private set; } = 0;
    public bool RangedItemEquiped { get; private set; } = false;
    public bool MeleeItemEquiped { get; private set; } = false;
    public List<CharacterStat> CharacterStats { get; private set; }
    public List<InventoryItem> PlayerInventory => playerInventory;
    public int GetResourceCount(Resource resource) { return playerResources[resource]; }
    public int CurrentInventoryWeight() 
    {
        int value = 0;
        foreach (var item in playerInventory)
        {
            value += item.Weight;
        }
        return value;
    }
    public Sprite PlayerIcon => characterSO.Icon;

    public void SetupPlayer(Character newCharacterSO)
    {
        characterSO = newCharacterSO;
        CharacterStats = newCharacterSO.CharacterStats.ConvertAll(stat => new CharacterStat(stat.name, stat.charStat, stat.value));

        playerInventory = new List<InventoryItem>();
        GiveDefaultResources();
    }

    public void AddItemToPlayerInventory(Item newItem)
    {
        InventoryItem newInvItem = new InventoryItem(newItem);
        // Only add item if the player has space
        if (maxInventoryWeight >= CurrentInventoryWeight() + newInvItem.Weight)
        {
            playerInventory.Add(newInvItem);
        }
        else
        {
            // We shouldn't get here because at this point we will lose the item
            Debug.Log("Failed to add item");
        }
    }

    public void AddItemToPlayerInventory(InventoryItem newItem)
    {
        InventoryItem newInvItem = newItem;
        // Only add item if the player has space
        if (maxInventoryWeight >= CurrentInventoryWeight() + newInvItem.Weight)
        {
            playerInventory.Add(newInvItem);
        }
        else
        {
            // We shouldn't get here because at this point we will lose the item
            Debug.Log("Failed to add item");
        }
    }

    public void RemoveItemFromPlayerInventory(InventoryItem itemToRemove)
    {
        playerInventory.Remove(itemToRemove);
    }

    private void GiveDefaultResources()
    {
        playerResources = new Dictionary<Resource, int>
        {
            { Resource.Wood, 15 },
            { Resource.Leather, 10 },
            { Resource.Steel, 5 }
        };
    }

    public void AwardResourceToPlayer(Resource resource, int value)
    {
        value = Mathf.Clamp(value, 0, 999);
        playerResources[resource] += value;
    }

    public bool IsEnoughToCraft(Item itemToCraft)
    {
        foreach (ResourceCost resource in itemToCraft.ResourcesToCraft)
        {
            if (playerResources[resource.resource] < resource.value)
            {
                return false;
            }
        }

        return true;
    }

    public void RemoveResourcesFromPlayer(List<ResourceCost> resourcesToRemove)
    {
        foreach (ResourceCost resource in resourcesToRemove)
        {
            playerResources[resource.resource] -= resource.value;
        }
    }

    public void SetupPlayerHealthBeforeFight()
    {
        HealthPoints = 0;
        foreach (CharacterStat stat in CharacterStats)
        {
            switch (stat.charStat)
            {
                case Stat.Def:
                    HealthPoints += (int)stat.value;
                    break;
                case Stat.Dex:
                    HealthPoints += (int)stat.value / 3;
                    break;
                case Stat.Str:
                    HealthPoints += (int)stat.value / 2;
                    break;
                case Stat.Luck:
                    HealthPoints += (int)stat.value * 2;
                    break;
            }
        }
    }

    /// <summary>
    /// Deal damage to the player
    /// </summary>
    /// <returns>True if the player is dead</returns>
    public bool DealDamage(int value, int enemyDefense, int enemyCritR, int enemyCritDmg)
    {
        // Check if we scored crit
        if (Random.Range(0f, 1f) <= (float)enemyCritR / 100)
        {
            value += value * enemyCritDmg / 100;
        }

        // Increase attack damage of ranged weapon with Dex stat and Melee with Str
        if (MeleeItemEquiped)
        {
            value += GetStrValue() / 3;
        }
        else if (RangedItemEquiped)
        {
            value += GetDexValue() / 2;
        }

        // Remove some attack damage based on the defense
        value -= value * enemyDefense / 100;
        HealthPoints -= value;
        return HealthPoints <= 0;
    }

    public void EquipItem(InventoryItem itemToEquip, CharacterEquipmentSlot slot, CharacterEquipmentSlotUI slotUI)
    {
        InventoryItem oldItem = slotUI.SetupSlot(itemToEquip, slot);
        if (oldItem != null)
        {
            // Update player's stats after unequip
            RemoveCharacterStatsWithNewItemEquip(oldItem);

            // Add old item to the storage inv
            AddItemToPlayerInventory(oldItem);

            // Remove itemsToEquip from storage inv
            RemoveItemFromPlayerInventory(itemToEquip);

            // Update player's stats after equip
            AddCharacterStatsWithNewItemEquip(itemToEquip);

            // We need to update storage inventory now that we removed the item
            FindObjectOfType<MainGameManager>().BarracksUI.OnStorageInvUpdated.Invoke();
        }
        else
        {
            // Update player's stats
            AddCharacterStatsWithNewItemEquip(itemToEquip);

            // No item was equipped, just remove equiping item from inv (itemToEquip)
            RemoveItemFromPlayerInventory(itemToEquip);

            // We need to update storage inventory now that we removed the item
            FindObjectOfType<MainGameManager>().BarracksUI.OnStorageInvUpdated.Invoke();
        }
    }

    public void ClearEquipmentAndInventory(){
        foreach(var item in playerInventory){
            RemoveCharacterStatsWithNewItemEquip(item);
        }
        playerInventory.Clear();
    }

    public void AddCharacterStatsWithNewItemEquip(InventoryItem item)
    {
        if (item.ItemSO is Weapon)
        {
            if ((item.ItemSO as Weapon).WeaponType == WeaponType.Melee)
            {
                MeleeItemEquiped = true;
            }
            else if ((item.ItemSO as Weapon).WeaponType == WeaponType.Ranged)
            {
                RangedItemEquiped = true;
            }

            for (int i = 0; i < CharacterStats.Count; i++)
            {
                if (CharacterStats[i].charStat == Stat.AttackSpeed)
                {
                    var tempStat = CharacterStats[i];
                    tempStat.value = (item.ItemSO as Weapon).AttackSpeed;
                    CharacterStats[i] = tempStat;
                }

                if (CharacterStats[i].charStat == Stat.Attack)
                {
                    var tempStat = CharacterStats[i];
                    tempStat.value += item.AttackDamage;
                    CharacterStats[i] = tempStat;
                }
            }
        }

        for (int i = 0; i < CharacterStats.Count; i++)
        {
            if (CharacterStats[i].charStat == Stat.Def)
            {
                var tempStat = CharacterStats[i];
                tempStat.value += item.Defense;
                CharacterStats[i] = tempStat;
                break;
            }
        }

        foreach (CharacterStat stat in item.GainedStats)
        {
            for (int i = 0; i < CharacterStats.Count; i++)
            {
                if (CharacterStats[i].charStat == stat.charStat)
                {
                    var tempStat = CharacterStats[i];
                    tempStat.value += stat.value;
                    CharacterStats[i] = tempStat;
                    break;
                }
            }
        }
    }

    private void RemoveCharacterStatsWithNewItemEquip(InventoryItem item)
    {
        if (item.ItemSO is Weapon)
        {
            MeleeItemEquiped = false;
            RangedItemEquiped = false;

            for (int i = 0; i < CharacterStats.Count; i++)
            {
                if (CharacterStats[i].charStat == Stat.AttackSpeed)
                {
                    var tempStat = CharacterStats[i];
                    // Yup
                    tempStat.value = 0.5f;
                    CharacterStats[i] = tempStat;
                }

                if (CharacterStats[i].charStat == Stat.Attack)
                {
                    var tempStat = CharacterStats[i];
                    tempStat.value -= item.AttackDamage;
                    CharacterStats[i] = tempStat;
                }
            }
        }

        for (int i = 0; i < CharacterStats.Count; i++)
        {
            if (CharacterStats[i].charStat == Stat.Def)
            {
                var tempStat = CharacterStats[i];
                tempStat.value -= item.Defense;
                CharacterStats[i] = tempStat;
                break;
            }
        }

        foreach (CharacterStat stat in item.GainedStats)
        {
            for (int i = 0; i < CharacterStats.Count; i++)
            {
                if (CharacterStats[i].charStat == stat.charStat)
                {
                    var tempStat = CharacterStats[i];
                    tempStat.value -= stat.value;
                    CharacterStats[i] = tempStat;
                    break;
                }
            }
        }
    }

    public int GetAttackValue()
    {
        foreach (CharacterStat stat in CharacterStats)
        {
            if (stat.charStat == Stat.Attack)
            {
                return (int)stat.value;
            }
        }

        return 0;
    }

    public int GetDefenseValue()
    {
        foreach (CharacterStat stat in CharacterStats)
        {
            if (stat.charStat == Stat.Def)
            {
                return (int)stat.value;
            }
        }

        return 0;
    }

    public float GetAttackSpeedTime()
    {
        foreach (CharacterStat stat in CharacterStats)
        {
            if (stat.charStat == Stat.AttackSpeed)
            {
                return 1 - stat.value + 1;
            }
        }

        return 0f;
    }

    public int GetCritRate()
    {
        foreach (CharacterStat stat in CharacterStats)
        {
            if (stat.charStat == Stat.CrtR)
            {
                return (int)stat.value;
            }
        }

        return 0;
    }

    public int GetCritDamage()
    {
        foreach (CharacterStat stat in CharacterStats)
        {
            if (stat.charStat == Stat.CrtDmg)
            {
                return (int)stat.value;
            }
        }

        return 0;
    }

    public int GetStrValue()
    {
        foreach (CharacterStat stat in CharacterStats)
        {
            if (stat.charStat == Stat.Str)
            {
                return (int)stat.value;
            }
        }

        return 0;
    }

    public int GetDexValue()
    {
        foreach (CharacterStat stat in CharacterStats)
        {
            if (stat.charStat == Stat.Dex)
            {
                return (int)stat.value;
            }
        }

        return 0;
    }
}

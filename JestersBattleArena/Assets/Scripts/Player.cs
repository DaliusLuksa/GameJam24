using System.Collections.Generic;
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
        CharacterStats = newCharacterSO.CharacterStats;

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
        foreach (CharacterStat stat in CharacterStats)
        {
            switch (stat.charStat)
            {
                case Stat.Def:
                    HealthPoints += stat.value;
                    break;
                case Stat.Dex:
                    HealthPoints += stat.value / 3;
                    break;
                case Stat.Str:
                    HealthPoints += stat.value / 2;
                    break;
                case Stat.Luck:
                    HealthPoints += stat.value * 2;
                    break;
            }
        }
    }

    public int GetAttackValue()
    {
        foreach (CharacterStat stat in CharacterStats)
        {
            if (stat.charStat == Stat.Attack)
            {
                return stat.value;
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
                return stat.value;
            }
        }

        return 0;
    }
}

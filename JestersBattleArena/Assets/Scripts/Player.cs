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

    private Character characterSO;

    public Item itemToAdd;

    public void SetupPlayer(Character newCharacterSO)
    {
        characterSO = newCharacterSO;
        CharacterStats = newCharacterSO.CharacterStats;

        playerInventory = new List<InventoryItem>();
        GiveDefaultResources();

        AddItemToPlayerInventory(itemToAdd);
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
}

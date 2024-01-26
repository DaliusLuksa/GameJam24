using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    [SerializeField] private int weight;
    [SerializeField] private List<CharacterStat> gainedStats;

    public int Weight => weight;

    public InventoryItem(Item newItem)
    {
        gainedStats = new List<CharacterStat>();

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
    [SerializeField] private int maxInventoryWeight = 30;

    public List<CharacterStat> CharacterStats { get; private set; }
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
}

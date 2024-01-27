using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class EnemyAIManager : MonoBehaviour
{
    public static EnemyAIManager instance;
    private MainGameManager mainGameManager = null;

    public IDictionary<int, int> dayWeightDistribution = new Dictionary<int, int>(){
        {1, 5},
        {2, 10},
        {3, 15},
        {4, 20},
        {5, 25},
        {6, 30},
        {7, 30},
        {8, 30},
    };
    public IDictionary<int, int> dayMaxItemDistribution = new Dictionary<int, int>(){
        {1, 2},
        {2, 4},
        {3, 6},
        {4, 6},
        {5, 6},
        {6, 6},
        {7, 6},
        {8, 6},
    };
    public IDictionary<int, int> dayItemTierDistribution = new Dictionary<int, int>(){
        {1, 1},
        {2, 2},
        {3, 3},
        {4, 4},
        {5, 5},
    };

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            mainGameManager = FindObjectOfType<MainGameManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemsBasedOnDay(int day)
    {
        var enemyPlayer = mainGameManager.EnemyPlayer;
        enemyPlayer.ClearEquipmentAndInventory();
        Item[] allItems = Resources.FindObjectsOfTypeAll(typeof(Item)) as Item[];
        ShuffleAllItems(allItems);

        int maxItems = dayMaxItemDistribution[System.Math.Min(day, dayMaxItemDistribution.Count)];
        int maxWeight = dayWeightDistribution[System.Math.Min(day, dayWeightDistribution.Count)];
        int tier = dayItemTierDistribution[System.Math.Min(day, dayItemTierDistribution.Count)];
        string[] availableItemTypes = { WeaponType.Melee.ToString(), WeaponType.Ranged.ToString(), ArmorType.Chest.ToString(), ArmorType.Helmet.ToString(), ArmorType.Pants.ToString(), ArmorType.Boots.ToString() };

        int currentItems = 0;
        int currentWeight = 0;
        string currentType = "";

        foreach (Item item in allItems)
        {
            if (item.itemType == ItemType.Weapon)
            {
                Weapon weapon = (Weapon)item;
                currentType = weapon.WeaponType.ToString();
            }
            else
            {
                Armor armor = (Armor)item;
                currentType = armor.ArmorType.ToString();
            }

            if (currentWeight >= maxWeight || currentItems == maxItems)
            {
                break;
            }

            if (item.Tier != tier)
            {
                continue;
            }
            if (availableItemTypes.All(x => x != currentType))
            {
                continue;
            }

            availableItemTypes = availableItemTypes.Where(x => x != currentType).ToArray();
            InventoryItem newInvItem = new InventoryItem(item);
            enemyPlayer.AddItemToPlayerInventory(newInvItem);
            currentWeight += item.Weight;
            currentItems++;
            Debug.Log($"Enemy player gained item {newInvItem.ItemName}!");
        }

        for (int i = enemyPlayer.PlayerInventory.Count - 1; i >= 0; i--)
        {
            var gainedItem = enemyPlayer.PlayerInventory[i];
            enemyPlayer.AddCharacterStatsWithNewItemEquip(gainedItem);
        }
    }

    public string getRandomItemName()
    {
        System.Random random = new System.Random();
        int randomIndex = random.Next(mainGameManager.EnemyPlayer.PlayerInventory.Count);
        return mainGameManager.EnemyPlayer.PlayerInventory[randomIndex].ItemName;
    }

    void ShuffleAllItems(Item[] Items)
    {
        for (int i = Items.Length - 1; i > 0; i--)
        {
            int range = Random.Range(0, i);
            Item temp = Items[i];
            Items[i] = Items[range];
            Items[range] = temp;
        }
    }

}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAIManager : MonoBehaviour
{
    public static EnemyAIManager instance;
    [SerializeField] private List<InventoryItem> enemyInventory = new List<InventoryItem>();
    public List<CharacterStat> CharacterStats { get; private set; }

    public IDictionary<int, int> dayWeightDistribution = new Dictionary<int, int>(){
        {1, 5},
        {2, 10},
        {3, 15},
        {4, 20},
        {5, 25},
    };
    public IDictionary<int, int> dayMaxItemDistribution = new Dictionary<int, int>(){
        {1, 2},
        {2, 4},
        {3, 6},
        {4, 6},
        {5, 6},
    };
    public IDictionary<int, int> dayItemTierDistribution = new Dictionary<int, int>(){
        {1, 1},
        {2, 1},
        {3, 2},
        {4, 2},
        {5, 2},
    };

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItemsBasedOnDay(int day)
    {
        enemyInventory.Clear();
        Item[] allItems =  Resources.FindObjectsOfTypeAll(typeof(Item)) as Item[];
        ShuffleAllItems(allItems);
        
        int maxItems = dayMaxItemDistribution[day];
        int maxWeight = dayWeightDistribution[day];
        int tier = dayItemTierDistribution[day];
        string[] availableItemTypes = {WeaponType.Melee.ToString(), WeaponType.Ranged.ToString(), ArmorType.Chest.ToString(), ArmorType.Helmet.ToString(), ArmorType.Pants.ToString(), ArmorType.Boots.ToString()};

        int currentItems = 0;
        int currentWeight = 0;
        string currentType = "";

        foreach (Item item in allItems)
        {
            if(item.itemType == ItemType.Weapon) {
                Weapon weapon = (Weapon) item;
                currentType = weapon.weaponType.ToString();
            } 
            else {
                Armor armor = (Armor) item;
                currentType = armor.armorType.ToString();
            }

            if(availableItemTypes.All(x => x != currentType)) {
                break;
            }
            if(currentWeight >= maxWeight || currentItems == maxItems) {
                break;
            }

            if(item.Tier != tier) {
                continue;
            }
        
            availableItemTypes = availableItemTypes.Where(x => x != currentType).ToArray();
            InventoryItem newInvItem = new InventoryItem(item);
            enemyInventory.Add(newInvItem);
            currentWeight += item.Weight;
            currentItems++;
        }
    }

    public string getRandomItemName()
    {
        System.Random random = new System.Random();
        int randomIndex = random.Next(enemyInventory.Count);
        return enemyInventory[randomIndex].ItemName;
    }

    void ShuffleAllItems(Item[] Items) {
        for (int i = Items.Length - 1; i > 0; i--) {
            int range = Random.Range(0, i);
            Item temp = Items[i];
            Items[i] = Items[range];
            Items[range] = temp;
        }
    }

}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopUI : MonoBehaviour
{
    [SerializeField] private Button townButton;
    [SerializeField] private Button craftButton;
    [SerializeField] private GameObject rootContent;
    [SerializeField] private BlueprintFrame blueprintFramePrefab;
    [SerializeField] private List<Item> availableWorkshopBPs;

    private MainGameManager mainGameManager = null;
    private bool wasCreated = false;
    private List<Item> selectedWorkshopBPsForCurrentDay;

    private void Start()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();

        townButton.onClick.AddListener(OnWorkshopButtonClicked);
        craftButton.onClick.AddListener(OnCraftButtonClicked);

        UpdateWorksopBPsByDay(mainGameManager.DayManager.CurrentDay);
        UpdateCraftingInventory();
        wasCreated = true;
    }

    private void OnEnable()
    {
        if (wasCreated)
        {
            UpdateCraftingInventory();
        }
    }

    private void OnDestroy()
    {
        townButton.onClick.RemoveListener(OnWorkshopButtonClicked);
        craftButton.onClick.RemoveListener(OnCraftButtonClicked);
    }

    private void OnWorkshopButtonClicked()
    {
        mainGameManager.MoveToTown();
    }

    private void UpdateCraftingInventory()
    {
        for (int i = rootContent.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(rootContent.transform.GetChild(i).gameObject);
        }

        int count = 0;
        int maxCount = 7;
        foreach (Item bp in selectedWorkshopBPsForCurrentDay)
        {
            BlueprintFrame newBP = Instantiate(blueprintFramePrefab, rootContent.transform);
            newBP.SetupBlueprintFrame(bp);
            count++;
            if (count > maxCount)
            {
                break;
            }
        }
    }

    private void OnCraftButtonClicked()
    {
        if (mainGameManager.MainPlayer.IsEnoughToCraft(mainGameManager.CurrentlySelectedBP.Item))
        {
            mainGameManager.MainPlayer.RemoveResourcesFromPlayer(mainGameManager.CurrentlySelectedBP.Item.ResourcesToCraft);
            mainGameManager.MainPlayer.AddItemToPlayerInventory(mainGameManager.CurrentlySelectedBP.Item);

            UpdateCraftingInventory();
        }
        else
        {
            Debug.Log("Not enough materials to craft an item");
        }
    }

    public void UpdateWorksopBPsByDay(int currentDay)
    {
        if (mainGameManager == null)
        {
            mainGameManager = FindObjectOfType<MainGameManager>();
        }

        selectedWorkshopBPsForCurrentDay = new List<Item>();
        int currentTier = mainGameManager.EnemyAIManager.dayItemTierDistribution[System.Math.Min(currentDay, mainGameManager.EnemyAIManager.dayItemTierDistribution.Count)];
        selectedWorkshopBPsForCurrentDay.AddRange(availableWorkshopBPs.FindAll(item => item.Tier == currentTier));
        // Shuffle the list
        for (int i = selectedWorkshopBPsForCurrentDay.Count - 1; i > 0; i--)
        {
            int range = Random.Range(0, i);
            var temp = selectedWorkshopBPsForCurrentDay[i];
            selectedWorkshopBPsForCurrentDay[i] = selectedWorkshopBPsForCurrentDay[range];
            selectedWorkshopBPsForCurrentDay[range] = temp;
        }
    }
}

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

    private void Start()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();

        townButton.onClick.AddListener(OnWorkshopButtonClicked);
        craftButton.onClick.AddListener(OnCraftButtonClicked);

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

        foreach (Item bp in availableWorkshopBPs)
        {
            BlueprintFrame newBP = Instantiate(blueprintFramePrefab, rootContent.transform);
            newBP.SetupBlueprintFrame(bp);
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
}

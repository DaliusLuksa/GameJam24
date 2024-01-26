using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopUI : MonoBehaviour
{
    [SerializeField] private Button townButton;
    [SerializeField] private GameObject rootContent;
    [SerializeField] private BlueprintFrame blueprintFramePrefab;
    [SerializeField] private List<Item> availableWorkshopBPs;

    private bool wasCreated = false;

    private void Start()
    {
        townButton.onClick.AddListener(OnWorkshopButtonClicked);

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
    }

    private void OnWorkshopButtonClicked()
    {
        FindObjectOfType<MainGameManager>().MoveToTown();
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
}

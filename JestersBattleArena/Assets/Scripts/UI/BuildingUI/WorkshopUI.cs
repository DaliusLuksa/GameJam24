using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopUI : MonoBehaviour
{
    [SerializeField] private Button townButton;
    [SerializeField] private GameObject rootContent;
    [SerializeField] private BlueprintFrame blueprintFramePrefab;
    [SerializeField] private List<Item> availableWorkshopBPs;

    private void Start()
    {
        townButton.onClick.AddListener(OnWorkshopButtonClicked);

        foreach (Item bp in availableWorkshopBPs)
        {
            BlueprintFrame newBP = Instantiate(blueprintFramePrefab, rootContent.transform);
            newBP.SetupBlueprintFrame(bp);
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
}

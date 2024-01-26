using System.Collections.Generic;
using UnityEngine;

public class WorkshopUI : MonoBehaviour
{
    [SerializeField] private GameObject rootContent;
    [SerializeField] private BlueprintFrame blueprintFramePrefab;
    [SerializeField] private List<Item> availableWorkshopBPs;

    private void Start()
    {
        foreach (Item bp in availableWorkshopBPs)
        {
            BlueprintFrame newBP = Instantiate(blueprintFramePrefab, rootContent.transform);
            newBP.SetupBlueprintFrame(bp);
        }
    }
}

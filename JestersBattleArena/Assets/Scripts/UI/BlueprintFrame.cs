using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintFrame : MonoBehaviour
{
    [SerializeField] private Image blueprintSprite;
    [SerializeField] private TextMeshProUGUI blueprintResourceCostText;

    public void SetupBlueprintFrame(Item item)
    {
        Player mainPlayer = FindObjectOfType<MainGameManager>().MainPlayer;
        blueprintSprite.sprite = item.Icon;
        StringBuilder newString = new StringBuilder();
        foreach (ResourceCost resource in item.ResourcesToCraft)
        {
            newString.AppendLine($"{resource.value}/{mainPlayer.GetResourceCount(resource.resource)} {resource.name}");
        }
        blueprintResourceCostText.text = newString.ToString();
    }
}

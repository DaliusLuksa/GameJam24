using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlueprintFrame : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image blueprintSprite;
    [SerializeField] private TextMeshProUGUI blueprintResourceCostText;

    private MainGameManager mainGameManager = null;
    private Item item = null;

    public Item Item => item;

    private void Awake()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();
    }

    public void SetupBlueprintFrame(Item item)
    {
        this.item = item;
        Player mainPlayer = mainGameManager.MainPlayer;
        blueprintSprite.sprite = item.Icon;
        StringBuilder newString = new StringBuilder();
        foreach (ResourceCost resource in item.ResourcesToCraft)
        {
            newString.AppendLine($"{resource.value}/{mainPlayer.GetResourceCount(resource.resource)} {resource.name}");
        }
        blueprintResourceCostText.text = newString.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mainGameManager.UpdateLatestSelectedWorkshopBP(this);
    }
}

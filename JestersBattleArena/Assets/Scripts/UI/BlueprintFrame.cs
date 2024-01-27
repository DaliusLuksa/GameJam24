using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlueprintFrame : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image blueprintSprite;
    [SerializeField] private TextMeshProUGUI blueprintResourceCostText;

    private MainGameManager mainGameManager = null;
    private ItemInfoWindowUI itemInfoWindowUI = null;
    private Item item = null;

    public Item Item => item;

    private void Awake()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();
        itemInfoWindowUI = FindObjectOfType<ItemInfoWindowUI>();
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoWindowUI.UpdateInfo(item);
        itemInfoWindowUI.Root.SetActive(true);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoWindowUI.Root.SetActive(false);
    }
}

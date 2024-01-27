using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class BarracksUI : MonoBehaviour
{
    [SerializeField] private Button townButton;
    [SerializeField] private Button startFightButton;
    [SerializeField] private GameObject contentRoot;
    [SerializeField] private StorageItemSlot storageItemSlotPrefab;
    [SerializeField] private List<TextMeshProUGUI> characterStatsList;

    private MainGameManager mainGameManager = null;
    // This shit is needed because OnEnable() runs first but we want to only update shit there after the initial load
    private bool wasCreated = false;

    private void Start()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();

        townButton.onClick.AddListener(OnTownButtonClicked);
        startFightButton.onClick.AddListener(OnStartFightButtonClicked);

        UpdatePlayerStorageInv();
        UpdateCharacterStatsInfo();
        wasCreated = true;
    }

    private void OnEnable()
    {
        if (wasCreated)
        {
            UpdatePlayerStorageInv();
            UpdateCharacterStatsInfo();
        }
    }

    private void OnDestroy()
    {
        townButton.onClick.RemoveListener(OnTownButtonClicked);
        startFightButton.onClick.RemoveListener(OnStartFightButtonClicked);
    }

    private void OnTownButtonClicked()
    {
        FindObjectOfType<MainGameManager>().MoveToTown();
    }

    private void OnStartFightButtonClicked()
    {
        FindObjectOfType<MainGameManager>().StartFight();
    }

    private void UpdatePlayerStorageInv()
    {
        for (int i = contentRoot.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(contentRoot.transform.GetChild(i).gameObject);
        }
        
        foreach (InventoryItem item in mainGameManager.MainPlayer.PlayerInventory)
        {
            StorageItemSlot newSlot = Instantiate(storageItemSlotPrefab, contentRoot.transform);
            newSlot.SetupStorageItemSlot(item);
        }
    }

    private void UpdateCharacterStatsInfo()
    {
        for (int i = 0; i < characterStatsList.Count; i++)
        {
            characterStatsList[i].text = $"{mainGameManager.MainPlayer.CharacterStats[i].charStat} - {mainGameManager.MainPlayer.CharacterStats[i].value}";
        }
    }
}

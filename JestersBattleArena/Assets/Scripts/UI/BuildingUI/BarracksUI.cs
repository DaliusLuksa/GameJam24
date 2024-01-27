using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BarracksUI : MonoBehaviour
{
    public UnityEvent OnStorageInvUpdated { get; private set; } = new UnityEvent();

    [SerializeField] private Button townButton;
    [SerializeField] private Button startFightButton;
    [SerializeField] private GameObject contentRoot;
    [SerializeField] private TextMeshProUGUI storageWeightText;
    [SerializeField] private StorageItemSlot storageItemSlotPrefab;
    [SerializeField] private List<TextMeshProUGUI> characterStatsList;
    [SerializeField] private CharacterEquipmentSlotUI helmetSlot;
    [SerializeField] private CharacterEquipmentSlotUI plateSlot;
    [SerializeField] private CharacterEquipmentSlotUI pantsSlot;
    [SerializeField] private CharacterEquipmentSlotUI bootsSlot;
    [SerializeField] private CharacterEquipmentSlotUI mainHandSlot;
    [SerializeField] private CharacterEquipmentSlotUI offHandSlot;

    private MainGameManager mainGameManager = null;
    // This shit is needed because OnEnable() runs first but we want to only update shit there after the initial load
    private bool wasCreated = false;

    private void Start()
    {
        OnStorageInvUpdated.AddListener(UpdateStorageUI);

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
        OnStorageInvUpdated.RemoveListener(UpdateStorageUI);
        townButton.onClick.RemoveListener(OnTownButtonClicked);
        startFightButton.onClick.RemoveListener(OnStartFightButtonClicked);
    }

    private void UpdateStorageUI()
    {
        UpdatePlayerStorageInv();
        UpdateCharacterStatsInfo();
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

        storageWeightText.text = $"Weight: {mainGameManager.MainPlayer.CurrentInventoryWeight().ToString()}";
    }

    private void UpdateCharacterStatsInfo()
    {
        for (int i = 0; i < characterStatsList.Count; i++)
        {
            characterStatsList[i].text = $"{mainGameManager.MainPlayer.CharacterStats[i].charStat} - {mainGameManager.MainPlayer.CharacterStats[i].value}";
        }
    }

    public CharacterEquipmentSlotUI GetCorrectEquipmentSlotUI(CharacterEquipmentSlot slot)
    {
        switch (slot)
        {
            case CharacterEquipmentSlot.Helmet:
                return helmetSlot;
            case CharacterEquipmentSlot.Plate:
                return plateSlot;
            case CharacterEquipmentSlot.Pants:
                return pantsSlot;
            case CharacterEquipmentSlot.Boots:
                return bootsSlot;
            case CharacterEquipmentSlot.MainHand:
                return mainHandSlot;
            case CharacterEquipmentSlot.OffHand:
                return offHandSlot;
        }

        return null;
    }
}

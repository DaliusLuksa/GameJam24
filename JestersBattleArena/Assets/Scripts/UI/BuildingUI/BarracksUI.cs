using UnityEngine;
using UnityEngine.UI;

public class BarracksUI : MonoBehaviour
{
    [SerializeField] private Button townButton;
    [SerializeField] private Button startFightButton;
    [SerializeField] private GameObject contentRoot;
    [SerializeField] private StorageItemSlot storageItemSlotPrefab;

    private MainGameManager mainGameManager = null;
    // This shit is needed because OnEnable() runs first but we want to only update shit there after the initial load
    private bool wasCreated = false;

    private void Start()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();

        townButton.onClick.AddListener(OnTownButtonClicked);
        startFightButton.onClick.AddListener(OnStartFightButtonClicked);

        UpdatePlayerStorageInv();
        wasCreated = true;
    }

    private void OnEnable()
    {
        if (wasCreated)
        {
            UpdatePlayerStorageInv();
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
}

using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private GameObject characterCreationUI;
    [SerializeField] private GameObject workshopUI;
    [SerializeField] private GameObject palaceUI;
    [SerializeField] private GameObject innUI;
    [SerializeField] private GameObject barracksUI;
    [SerializeField] private GameObject townUI;
    [SerializeField] private GameObject fightArenaUI;
    [SerializeField] private GameObject mainMenuUi;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Player mainPlayer;

    private BlueprintFrame currentlySelectedBP = null;

    public Player MainPlayer => mainPlayer;
    public BlueprintFrame CurrentlySelectedBP => currentlySelectedBP;

    public void SetupPlayer(Character characterSO)
    {
        mainPlayer = Instantiate(playerPrefab).GetComponent<Player>();
        mainPlayer.gameObject.SetActive(false);
        mainPlayer.SetupPlayer(characterSO);

        characterCreationUI.SetActive(false);
        barracksUI.SetActive(true);

        EnemyAIManager.instance.AddItemsBasedOnDay(1);
    }

    /// <summary>
    /// Player comes from Town ONLY
    /// </summary>
    public void MoveToWorkshop()
    {
        workshopUI.SetActive(true);
        townUI.SetActive(false);
    }

    /// <summary>
    /// Player comes from Town ONLY
    /// </summary>
    public void MoveToPalace()
    {
        palaceUI.SetActive(true);
        townUI.SetActive(false);
    }

    /// <summary>
    /// Player comes from Town ONLY
    /// </summary>
    public void MoveToInn()
    {
        innUI.SetActive(true);
        townUI.SetActive(false);
    }

    /// <summary>
    /// Player comes from Town ONLY
    /// </summary>
    public void MoveToBarracks()
    {
        townUI.SetActive(false);
        barracksUI.SetActive(true);
    }

    /// <summary>
    /// Player comes from multiple places...
    /// </summary>
    public void MoveToTown()
    {
        workshopUI.SetActive(false);
        palaceUI.SetActive(false);
        innUI.SetActive(false);
        townUI.SetActive(true);
        barracksUI.SetActive(false);
    }

    /// <summary>
    /// Player comes from main menu
    /// </summary>
    public void MoveToCharacterCreation()
    {
        characterCreationUI.SetActive(true);
        mainMenuUi.SetActive(false);
    }

    public void StartFight()
    {
        barracksUI.SetActive(false);
        fightArenaUI.SetActive(true);
    }

    public void UpdateLatestSelectedWorkshopBP(BlueprintFrame selectedBP)
    {
        currentlySelectedBP = selectedBP;
    }
}

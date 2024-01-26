using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] private GameObject characterCreationUI;
    [SerializeField] private GameObject barracksUI;
    [SerializeField] private GameObject townUI;
    [SerializeField] private GameObject fightArenaUI;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Player mainPlayer;

    public void SetupPlayer(Character characterSO)
    {
        mainPlayer = Instantiate(playerPrefab).GetComponent<Player>();
        mainPlayer.gameObject.SetActive(false);
        mainPlayer.SetupPlayer(characterSO);

        characterCreationUI.SetActive(false);
        barracksUI.SetActive(true);
    }

    public void MoveToBarracks()
    {
        townUI.SetActive(false);
        barracksUI.SetActive(true);
    }

    public void MoveToTown()
    {
        townUI.SetActive(true);
        barracksUI.SetActive(false);
    }

    public void StartFight()
    {
        barracksUI.SetActive(false);
        fightArenaUI.SetActive(true);
    }
}

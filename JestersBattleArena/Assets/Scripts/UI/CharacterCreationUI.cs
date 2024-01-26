using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationUI : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Character currentlySelectedCharacter;

    public Character GetCurrentlySelectedCharacter => currentlySelectedCharacter;

    private void Start()
    {
        startGameButton.onClick.AddListener(OnStartGameButtonClicked);
    }

    private void OnDestroy()
    {
        startGameButton.onClick.RemoveListener(OnStartGameButtonClicked);
    }

    public void UpdateCurrentlySelectedCharacter(Character character)
    {
        currentlySelectedCharacter = character;
        startGameButton.gameObject.SetActive(true);
    }

    private void OnStartGameButtonClicked()
    {
        if (currentlySelectedCharacter == null)
        {
            Debug.Log("Select the character first.");
            return;
        }

        FindObjectOfType<MainGameManager>().SetupPlayer(currentlySelectedCharacter);
    }
}

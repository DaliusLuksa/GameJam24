using UnityEngine;
using UnityEngine.UI;

public class BarracksUI : MonoBehaviour
{
    [SerializeField] private Button townButton;
    [SerializeField] private Button startFightButton;

    private void Start()
    {
        townButton.onClick.AddListener(OnBarracksButtonClicked);
        startFightButton.onClick.AddListener(OnStartFightButtonClicked);
    }

    private void OnDestroy()
    {
        townButton.onClick.RemoveListener(OnBarracksButtonClicked);
        startFightButton.onClick.RemoveListener(OnStartFightButtonClicked);
    }

    private void OnBarracksButtonClicked()
    {
        FindObjectOfType<MainGameManager>().MoveToTown();
    }

    private void OnStartFightButtonClicked()
    {
        FindObjectOfType<MainGameManager>().StartFight();
    }
}

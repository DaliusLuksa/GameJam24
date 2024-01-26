using UnityEngine;
using UnityEngine.UI;

public class BarracksUI : MonoBehaviour
{
    [SerializeField] private Button townButton;

    private void Start()
    {
        townButton.onClick.AddListener(OnBarracksButtonClicked);
    }

    private void OnDestroy()
    {
        townButton.onClick.RemoveListener(OnBarracksButtonClicked);
    }

    private void OnBarracksButtonClicked()
    {
        FindObjectOfType<MainGameManager>().MoveToTown();
    }
}

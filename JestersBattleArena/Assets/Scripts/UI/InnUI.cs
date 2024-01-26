using UnityEngine;
using UnityEngine.UI;

public class InnUI : MonoBehaviour
{
    [SerializeField] private Button townButton;
    private void Start()
    {
        townButton.onClick.AddListener(OnInnButtonClicked);
    }

    private void OnDestroy()
    {
        townButton.onClick.RemoveListener(OnInnButtonClicked);
    }

    private void OnInnButtonClicked()
    {
        FindObjectOfType<MainGameManager>().MoveToTown();
    }
}

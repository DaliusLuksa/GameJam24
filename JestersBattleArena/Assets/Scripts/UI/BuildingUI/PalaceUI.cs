using UnityEngine;
using UnityEngine.UI;

public class PalaceUI : MonoBehaviour
{
    [SerializeField] private Button townButton;

    private void Start()
    {
        townButton.onClick.AddListener(OnPalaceButtonClicked);
    }

    private void OnDestroy()
    {
        townButton.onClick.RemoveListener(OnPalaceButtonClicked);
    }

    private void OnPalaceButtonClicked()
    {
        FindObjectOfType<MainGameManager>().MoveToTown();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class TownUI : MonoBehaviour
{
    [SerializeField] private Button barracksButton;

    public void OnInnImageClicked()
    {
        FindObjectOfType<MainGameManager>().MoveToInn();
    }

    private void Start()
    {
        barracksButton.onClick.AddListener(OnBarracksButtonClicked);
    }

    private void OnDestroy()
    {
        barracksButton.onClick.RemoveListener(OnBarracksButtonClicked);
    }

    private void OnBarracksButtonClicked()
    {
        FindObjectOfType<MainGameManager>().MoveToBarracks();
    }
}

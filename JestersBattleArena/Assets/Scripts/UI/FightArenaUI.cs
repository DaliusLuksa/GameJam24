using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightArenaUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerHPText;
    [SerializeField] private TextMeshProUGUI playerAttackText;
    [SerializeField] private TextMeshProUGUI playerArmorText;
    [SerializeField] private TextMeshProUGUI enemyHPText;
    [SerializeField] private TextMeshProUGUI enemyAttackText;
    [SerializeField] private TextMeshProUGUI enemyArmorText;
    [SerializeField] private Image playerImage;
    [SerializeField] private Image enemyImage;

    private MainGameManager mainGameManager = null;

    private void Start()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();

        mainGameManager.MainPlayer.SetupPlayerHealthBeforeFight();
        playerImage.sprite = mainGameManager.MainPlayer.PlayerIcon;

        mainGameManager.EnemyPlayer.SetupPlayerHealthBeforeFight();
        enemyImage.sprite = mainGameManager.EnemyPlayer.PlayerIcon;

        UpdateUIInfo();
    }

    private void UpdateUIInfo()
    {
        playerHPText.text = $"Health points: {mainGameManager.MainPlayer.HealthPoints}";
        playerAttackText.text = $"Attack: {mainGameManager.MainPlayer.GetAttackValue()}";
        playerArmorText.text = $"Armor: {mainGameManager.MainPlayer.GetDefenseValue()}";

        enemyHPText.text = $"Health points: {mainGameManager.EnemyPlayer.HealthPoints}";
        enemyAttackText.text = $"Attack: {mainGameManager.EnemyPlayer.GetAttackValue()}";
        enemyArmorText.text = $"Armor: {mainGameManager.EnemyPlayer.GetDefenseValue()}";
    }
}

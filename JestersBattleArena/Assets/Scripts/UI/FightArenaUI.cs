using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightArenaUI : MonoBehaviour
{
    [SerializeField] private GameObject playerInfoRoot;
    [SerializeField] private TextMeshProUGUI playerHPText;
    [SerializeField] private TextMeshProUGUI playerAttackText;
    [SerializeField] private TextMeshProUGUI playerArmorText;
    [SerializeField] private GameObject enemyInfoRoot;
    [SerializeField] private TextMeshProUGUI enemyHPText;
    [SerializeField] private TextMeshProUGUI enemyAttackText;
    [SerializeField] private TextMeshProUGUI enemyArmorText;
    [SerializeField] private Image playerImage;
    [SerializeField] private Image enemyImage;
    [SerializeField] private GameObject endFightWindowRoot;
    [SerializeField] private Button returnButton;
    [SerializeField] private TextMeshProUGUI endResultText;
    [SerializeField] private TextMeshProUGUI gainedLootText;
    [SerializeField] private DialogueReward victoryRewards;
    [SerializeField] private DialogueReward defeatRewards;

    private MainGameManager mainGameManager = null;

    private float currentPlayerAttackCDTime = 0f;
    private float currentEnemyAttackCDTime = 0f;

    private bool isFightActive = false;
    private bool wasCreated = false;

    private void Start()
    {
        returnButton.onClick.AddListener(OnReturnButtonClicked);

        mainGameManager = FindObjectOfType<MainGameManager>();

        mainGameManager.MainPlayer.SetupPlayerHealthBeforeFight();
        playerImage.sprite = mainGameManager.MainPlayer.PlayerIcon;

        mainGameManager.EnemyPlayer.SetupPlayerHealthBeforeFight();
        enemyImage.sprite = mainGameManager.EnemyPlayer.PlayerIcon;

        UpdateUIInfo();

        isFightActive = true;
        wasCreated = true;
    }

    private void OnEnable()
    {
        if (wasCreated)
        {
            mainGameManager.MainPlayer.SetupPlayerHealthBeforeFight();
            playerImage.sprite = mainGameManager.MainPlayer.PlayerIcon;

            mainGameManager.EnemyPlayer.SetupPlayerHealthBeforeFight();
            enemyImage.sprite = mainGameManager.EnemyPlayer.PlayerIcon;

            UpdateUIInfo();

            isFightActive = true;
        }
    }

    private void OnDestroy()
    {
        returnButton.onClick.RemoveListener(OnReturnButtonClicked);
    }

    private void OnReturnButtonClicked()
    {
        mainGameManager.ReturnFromTheFightArena();

        endFightWindowRoot.SetActive(false);
        playerInfoRoot.SetActive(true);
        enemyInfoRoot.SetActive(true);
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

    private void FixedUpdate()
    {
        if (!isFightActive)
        {
            return;
        }

        var playerMaxTime = mainGameManager.MainPlayer.GetAttackSpeedTime();
        var enemyMaxTime = mainGameManager.EnemyPlayer.GetAttackSpeedTime();

        if (currentPlayerAttackCDTime >= playerMaxTime)
        {
            // Deal damage and go on cd
            if (mainGameManager.EnemyPlayer.DealDamage(mainGameManager.MainPlayer.GetAttackValue(), mainGameManager.EnemyPlayer.GetDefenseValue()))
            {
                // Enemy died
                isFightActive = false;
                EndFightResults(true);
            }
            else
            {
                currentPlayerAttackCDTime = 0f;
            }
        }

        if (currentEnemyAttackCDTime >= enemyMaxTime)
        {
            // Deal damage and go on cd
            if (mainGameManager.MainPlayer.DealDamage(mainGameManager.EnemyPlayer.GetAttackValue(), mainGameManager.MainPlayer.GetDefenseValue()))
            {
                // Player died
                isFightActive = false;
                EndFightResults(false);
            }
            else
            {
                currentEnemyAttackCDTime = 0f;
            }
        }

        currentPlayerAttackCDTime += Time.fixedDeltaTime;
        currentEnemyAttackCDTime += Time.fixedDeltaTime;

        UpdateUIInfo();
    }

    private void EndFightResults(bool playerWon)
    {
        playerInfoRoot.SetActive(false);
        enemyInfoRoot.SetActive(false);

        StringBuilder newString = new StringBuilder();
        if (playerWon)
        {
            endResultText.text = "Victory";
            foreach (ResourceCost resourceReward in victoryRewards.resourcesToAward)
            {
                mainGameManager.MainPlayer.AwardResourceToPlayer(resourceReward.resource, resourceReward.value);
                newString.AppendLine($"+{resourceReward.value} {resourceReward.resource}");
            }
            if (victoryRewards.itemToAward != null)
            {
                mainGameManager.MainPlayer.AddItemToPlayerInventory(victoryRewards.itemToAward);
            }
        }
        else
        {
            endResultText.text = "Defeat";
            foreach (ResourceCost resourceReward in defeatRewards.resourcesToAward)
            {
                mainGameManager.MainPlayer.AwardResourceToPlayer(resourceReward.resource, resourceReward.value);
                newString.AppendLine($"+{resourceReward.value} {resourceReward.resource}");
            }
            if (defeatRewards.itemToAward != null)
            {
                mainGameManager.MainPlayer.AddItemToPlayerInventory(defeatRewards.itemToAward);
            }
        }

        gainedLootText.text = newString.ToString();
        endFightWindowRoot.SetActive(true);
    }
}

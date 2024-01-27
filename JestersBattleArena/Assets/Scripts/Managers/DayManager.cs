using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public TMP_Text currentDayObject;

    private MainGameManager mainGameManager = null;
    private int currentDay = 1;

    public int CurrentDay => currentDay;

    private void Start()
    {
        mainGameManager = FindObjectOfType<MainGameManager>();
    }

    public void nextDay() {
        currentDay++;
        DialogManager.instance.Reset();
        EnemyAIManager.instance.AddItemsBasedOnDay(currentDay);
        currentDayObject.text = "Day " + currentDay.ToString();

        // Update bps for workshop
        mainGameManager.WorkshopUI.UpdateWorksopBPsByDay(currentDay);
    }
}

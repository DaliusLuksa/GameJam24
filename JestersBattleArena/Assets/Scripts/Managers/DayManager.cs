using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int currentDay = 1;
    public TMP_Text currentDayObject;
    public void nextDay() {
        currentDay++;
        DialogManager.instance.Reset();
        EnemyAIManager.instance.AddItemsBasedOnDay(currentDay);
        currentDayObject.text = "Day " + currentDay.ToString();
    }
}

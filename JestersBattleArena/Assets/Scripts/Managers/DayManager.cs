using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int currentDay = 1;
    public void nextDay() {
        currentDay++;
        DialogManager.instance.Reset();
        EnemyAIManager.instance.AddItemsBasedOnDay(currentDay);
    }
}

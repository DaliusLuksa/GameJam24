using UnityEngine;

public class HappinessManager : MonoBehaviour
{
    public int maxHappiness = 100;
    public int currentHappiness = 50;
    public GameObject maximumHappinessBar;
    public GameObject currentHappinessBar;

    void Start() {
        UpdateHappiness(currentHappiness);
    }
    public void UpdateHappiness(int newHappiness)
    {
        currentHappiness = newHappiness;

        if(currentHappiness > maxHappiness) {
            currentHappiness = maxHappiness;
        }

        float maximumWidth = ((RectTransform) maximumHappinessBar.transform).rect.width;
        
        ((RectTransform) currentHappinessBar.transform).SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentHappiness*maximumWidth/maxHappiness);
    }

    public void AddHappiness(int addHappiness) {
        currentHappiness += addHappiness;
        UpdateHappiness(currentHappiness);
    }
}

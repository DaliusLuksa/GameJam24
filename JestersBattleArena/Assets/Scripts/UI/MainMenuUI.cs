using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartGameClicked() 
    {
        FindObjectOfType<MainGameManager>().MoveToCharacterCreation();
    }
    public void OnSettingsClicked()
    {
        FindObjectOfType<MainGameManager>().MoveToPalace();
    }
    public void OnCreditsClicked()
    {
        FindObjectOfType<MainGameManager>().MoveToInn();
    }
    public void OnExitGameClicked()
    {
        Application.Quit();
    }
}

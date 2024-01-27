using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartGameClicked() 
    {
        FindObjectOfType<MainGameManager>().MoveToCharacterCreation();
    }
    public void OnExitGameClicked()
    {
        Application.Quit();
    }
}

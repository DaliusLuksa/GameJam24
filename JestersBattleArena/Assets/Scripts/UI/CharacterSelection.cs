using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CharacterCreationUI characterCreationUI;
    [SerializeField] private Character characterSO;
    [SerializeField] private Image characterPreviewImage;
    [SerializeField] private List<TextMeshProUGUI> characterStatsList;

    public void OnPointerClick(PointerEventData eventData)
    {
        characterCreationUI.UpdateCurrentlySelectedCharacter(characterSO);

        characterPreviewImage.sprite = characterSO.Icon;
        characterPreviewImage.gameObject.SetActive(true);

        for (int i = 0; i < characterStatsList.Count; i++)
        {
            characterStatsList[i].text = $"{characterSO.CharacterStats[i].charStat} - {characterSO.CharacterStats[i].value}";
        }
    }
}

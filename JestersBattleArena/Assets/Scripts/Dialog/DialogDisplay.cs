using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogDisplay : MonoBehaviour
{
    public TMP_Text speakerName;
    public TMP_Text dialogText;
    public GameObject choices;
    private Dialog dialog;
    public GameObject choiceButtonPrefab;
    public GameObject DialogNextButton;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void loadDialog(Dialog newDialog)
    {
        dialog = newDialog;
        updateDialog(dialog);
        gameObject.SetActive(true);
    }

    public void showNextDialog()
    {
        dialog = dialog.nextDialog;

        if (dialog == null) {
            gameObject.SetActive(false);

            return;
        }

        updateDialog(dialog);
    }

    void updateDialog(Dialog dialog) {
        dialogText.text = dialog.line.text;
        speakerName.text = dialog.character.characterName;

        if(dialog.choices.Any(x => x != null)) {
            updateChoices();
            DialogNextButton.SetActive(false);
        }
    }

    void updateChoices() {
        for (int i = 0; i < dialog.choices.Length; i++) 
        {
            int choiceIndex = i;

            GameObject choiceObj = Instantiate(choiceButtonPrefab); 
            Button choiceButton = choiceObj.GetComponent<Button>();
            TMP_Text choiceText = choiceButton.GetComponentInChildren<TMP_Text>();
            choiceText.text = dialog.choices[i].line.text;
            choiceButton.onClick.AddListener(() => onChoiceSelection(dialog.choices[choiceIndex]));
            choiceObj.transform.SetParent(choices.transform,false); 
        }
    }

    void removeChoices() {
        foreach (Transform choiceObj in choices.transform)
        {
            Destroy(choiceObj.gameObject);
        }
    }

    void onChoiceSelection(Dialog dialog) {
        updateDialog(dialog);
        removeChoices();
        DialogNextButton.SetActive(true);
    }
}

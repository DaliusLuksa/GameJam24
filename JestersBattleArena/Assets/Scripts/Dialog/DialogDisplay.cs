using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogDisplay : MonoBehaviour
{
    public TMP_Text speakerName;
    public TMP_Text dialogText;
    public GameObject choices;
    private Dialog dialog;
    public GameObject choiceButtonPrefab;
    public GameObject DialogNextButton;
    public GameObject DialogSelection;
    public Image speakerImage;

    public void loadDialog(Dialog newDialog)
    {
        dialog = newDialog;
        if(closeDialogIfEmpty(dialog)) {
            return;
        };
        updateDialog(dialog);
        gameObject.SetActive(true);
        DialogSelection.gameObject.SetActive(false);
    }

    public void showNextDialog()
    {
        dialog = dialog.nextDialog;
        if(closeDialogIfEmpty(dialog)) {
            return;
        };
        updateDialog(dialog);
    }

    void updateDialog(Dialog dialog) {
        dialogText.text = dialog.line.text;
        speakerName.text = dialog.character.characterName;
        speakerImage = dialog.character.image;

        if(dialog.isClueGiven) {
            string randomColor = ClueManager.instance.addEmperorsColorClue();
            dialogText.text = dialogText.text.Replace("{insert color}", randomColor);
        }

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

    bool closeDialogIfEmpty(Dialog dialog) {
        if (dialog == null) {
            gameObject.SetActive(false);
            DialogSelection.gameObject.SetActive(true);

            return true;
        }
         return false;
    }

    void onChoiceSelection(Dialog newDialog) {
        removeChoices();
        loadDialog(newDialog.nextDialog);
        
        DialogNextButton.SetActive(true);
    }
}

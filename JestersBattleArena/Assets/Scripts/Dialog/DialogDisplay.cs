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
    public Dialog noInteractionCountDialog;
    public GameObject choiceButtonPrefab;
    public GameObject DialogNextButton;
    public GameObject DialogSelection;
    public TMP_Text InteractionCount;
    public Image speakerImage;
   

    public void loadDialog(Dialog newDialog)
    {
        dialog = newDialog;
        if(closeDialogIfEmpty(dialog)) {
            return;
        };
        updateNoInteractionDialogIfNotEnoughInteractions();
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
        updateNoInteractionDialogIfNotEnoughInteractions();
        updateDialog(dialog);
    }

    void updateDialog(Dialog dialog) {
        dialogText.text = dialog.line.text;
        speakerName.text = dialog.character.characterName;
        speakerImage.sprite = dialog.character.sprite;
        DialogManager.instance.interactionNumber -= dialog.interactionCost;
        InteractionCount.text = DialogManager.instance.maxInteractionNumber.ToString()+"/"+DialogManager.instance.interactionNumber.ToString();

        Player mainPlayer = FindObjectOfType<MainGameManager>().MainPlayer;
        foreach (ResourceCost resourceReward in dialog.reward.resourcesToAward)
        {
            mainPlayer.AwardResourceToPlayer(resourceReward.resource, resourceReward.value);
        }
        if (dialog.reward.itemToAward != null)
        {
            mainPlayer.AddItemToPlayerInventory(dialog.reward.itemToAward);
        }

        if(dialog.isClueGiven) {
            string randomColor = DialogManager.instance.addEmperorsColorClue();
            dialogText.text = dialogText.text.Replace("{insert color}", randomColor);
        }
        if(dialog.isGladiotorInfoGiven) {
            dialogText.text += " You find that enemy gladiator has a " + EnemyAIManager.instance.getRandomItemName();  
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

    void updateNoInteractionDialogIfNotEnoughInteractions()
    {
        if(DialogManager.instance.interactionNumber == 0 || DialogManager.instance.interactionNumber - dialog.interactionCost < 0) {
            dialog = noInteractionCountDialog;
            updateDialog(dialog);
        }
    }
}

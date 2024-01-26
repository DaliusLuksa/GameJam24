using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Dialog")]
public class Dialog : ScriptableObject
{   
    public Line line;
    public NPCCharacter character;
    public Dialog nextDialog;
    public Dialog[] choices;
}

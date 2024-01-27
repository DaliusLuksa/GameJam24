using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DialogueReward
{
    public List<ResourceCost> resourcesToAward;
    public Item itemToAward;
}

[CreateAssetMenu(menuName = "Dialog/Dialog")]
public class Dialog : ScriptableObject
{   
    public Line line;
    public NPCCharacter character;
    public Dialog nextDialog;
    public Dialog[] choices;

    public DialogueReward reward;

    public bool isClueGiven;
    public bool isGladiotorInfoGiven;

    public int interactionCost;
    public int happinessAddition;
}
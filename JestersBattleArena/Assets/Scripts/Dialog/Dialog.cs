using System;
using UnityEngine;

public enum rewardEnum {Leather, Steel, Wood};

[CreateAssetMenu(menuName = "Dialog/Dialog")]
public class Dialog : ScriptableObject
{   
    public Line line;
    public NPCCharacter character;
    public Dialog nextDialog;
    public Dialog[] choices;

    [SerializeField]
    public Reward[] rewards;

    public bool isClueGiven;
}

[Serializable]
public class Reward
{
    public rewardEnum reward;
    
    [SerializeField]
    public int quantity = 0;
}
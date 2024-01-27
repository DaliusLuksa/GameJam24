using System;
using System.Collections.Generic;
using UnityEngine;

public enum Stat
{
    Str,
    Int,
    Dex,
    Def,
    CrtR,
    CrtDmg,
    Luck,
    Fun,
    Attack,
    AttackSpeed
}

[Serializable]
public struct CharacterStat
{
    public string name;
    public Stat charStat;
    public float value;
}

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Sprite icon;
    [SerializeField] private List<CharacterStat> characterStats;

    public Sprite Icon => icon;
    public List<CharacterStat> CharacterStats => characterStats;
}

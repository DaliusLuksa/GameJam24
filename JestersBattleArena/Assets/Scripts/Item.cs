using System;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    Wood,
    Steel,
    Leather
}

public enum ItemType
{
    Armor,
    Weapon,
    None
}

[Serializable]
public struct RandomStat
{
    public string name;
    public Stat stat;
    public int minValue;
    public int maxValue;
    [Range(0, 1)] public float chanceToGive;
}

[Serializable]
public struct ResourceCost
{
    public string name;
    public Resource resource;
    public int value;
}

public abstract class Item : ScriptableObject
{
    public ItemType itemType = ItemType.None;
    [SerializeField] private new string name;
    [SerializeField] private Sprite icon;
    [SerializeField] private int minAttackDamage;
    [SerializeField] private int maxAttackDamage;
    [SerializeField] private int minDefense;
    [SerializeField] private int maxDefense;
    [SerializeField] private int weight;
    [SerializeField] private int tier;

    [SerializeField] private List<ResourceCost> resourcesToCraft;
    [SerializeField] private List<RandomStat> optionalRandomStats;

    public List<RandomStat> OptionalRandomStats => optionalRandomStats;
    public Sprite Icon => icon;
    public List<ResourceCost> ResourcesToCraft => resourcesToCraft;
    public int Tier => tier;
    public int Weight => weight;
    public int MinAttackDamage => minAttackDamage;
    public int MaxAttackDamage => maxAttackDamage;
    public int MinDefense => minDefense;
    public int MaxDefense => maxDefense;
    public string Name => name;
}

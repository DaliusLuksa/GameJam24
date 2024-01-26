using System;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    Wood,
    Steel,
    Leather
}

[Serializable]
public struct RandomStat
{
    public string name;
    public Stat stat;
    public int minValue;
    public int maxValue;
    public float chanceToGive;
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
    [SerializeField] private new string name;
    [SerializeField] private Sprite icon;
    [SerializeField] private int minAttackDamage;
    [SerializeField] private int maxAttackDamage;
    [SerializeField] private int minDefense;
    [SerializeField] private int maxDefense;
    [SerializeField] private List<ResourceCost> resourcesToCraft;
    [SerializeField] private List<RandomStat> optionalRandomStats;
}

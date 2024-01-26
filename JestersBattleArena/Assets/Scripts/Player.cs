using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<CharacterStat> CharacterStats { get; private set; }

    private Character characterSO;

    public void SetupPlayer(Character newCharacterSO)
    {
        characterSO = newCharacterSO;
        CharacterStats = newCharacterSO.CharacterStats;
    }
}

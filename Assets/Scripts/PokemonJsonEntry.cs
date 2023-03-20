using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PokemonJsonEntry 
{
    string id;
    Name name;
    string[] type;

    [Serializable]
    private class Name
    {
        string english;
        string japanese;
        string chiense;
        string french;
    }

    [Serializable]
    private class BaseStats
    {
        string hp;
        string attack;
        string defense;
        string spAttack;
    }
}

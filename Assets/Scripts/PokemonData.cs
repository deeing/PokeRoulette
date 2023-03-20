using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PokemonData
{
    public string name;
    public PokemonMove[] moves;

    public override string ToString()
    {
        return name + " Moves:" + moves.Length;
    }
}

[Serializable]
public class PokemonMove
{
    public Move move;
}

[Serializable]
public class Move
{
    public string name;
    public string url;
}
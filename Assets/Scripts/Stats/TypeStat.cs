using Defective.JSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeStat : Stat
{
    public override void ChooseStat()
    {
        stats.ChooseTypes(GetPokeType());
    }

    private List<string> GetPokeType()
    {
        List<string> result = new();

        JSONObject currPoke = PokeApi.GetCurrentPokemon();
        JSONObject types = currPoke["types"];

        foreach(JSONObject type in types)
        {
            result.Add(type["type"]["name"].stringValue);
        }

        return result;
    }
}

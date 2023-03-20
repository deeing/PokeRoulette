using Defective.JSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private GameObject contentArea;
    [SerializeField]
    private Roulette roulette;

    private Dictionary<StatType, string> chosenPokemonStats;
    private List<string> types;

    private void Awake()
    {
        chosenPokemonStats = new Dictionary<StatType, string>();
        types = new List<string>();
    }

    public void Toggle(bool status)
    {
        contentArea.SetActive(status);
    }

    public void ChooseStat(StatType statType, string value)
    {
        chosenPokemonStats.Add(statType, value);
        NextRoll();
    }

    public void ChooseTypes(List<string> types)
    {
        this.types = types;
        NextRoll();
    }

    private void NextRoll()
    {
        PokeApi.currentPokemonIndex++;

        roulette.SetupRoll();
    }

    public bool HasReleasedPokemon()
    {
        return chosenPokemonStats.ContainsKey(StatType.Release);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Stat : MonoBehaviour
{
    [SerializeField]
    private RawImage pokemonDisplay;
    [SerializeField]
    protected Stats stats;
    [SerializeField]
    private Button statButton;

    public void SetDisplay(Texture2D pokeImage)
    {
        pokemonDisplay.texture = pokeImage;
        pokemonDisplay.enabled = true;
    }

    public void BTN_ChooseStat()
    {
        SetDisplay(PokeApi.GetCurrentPokemonImage());
        statButton.interactable = false;

        ChooseStat();
    }

    public abstract void ChooseStat();

    public void BTN_DecrementIndex()
    {
        PokeApi.currentPokemonIndex--;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    [SerializeField]
    private RawImage displayImage;
    [SerializeField]
    private float rollInterval = .25f;
    [SerializeField]
    private GameObject rollButton;
    [SerializeField]
    private GameObject stopButton;
    [SerializeField]
    private GameObject loadingText;
    [SerializeField]
    private Stats stats;

    private Texture2D[] pokemonImages;
    private WaitForSeconds rollWait;
    private Coroutine rollCoroutine;

    private const int NUM_POKEMON = 7;

    private void Awake()
    {
        pokemonImages = Resources.LoadAll<Texture2D>("Images");

        rollWait = new WaitForSeconds(rollInterval);


        StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        // get total pokemon
        yield return StartCoroutine(PokeApi.GetTotalPokemon());
        // load all of the pokemon
        int numLoaded = 0;
        while(numLoaded < NUM_POKEMON)
        {
            yield return StartCoroutine(PokeApi.LoadPokemon());
            numLoaded++;
        }

        PokeApi.DebugPokemonList();
        SetupRoll();
    }

    public void SetupRoll()
    {
        // check for end game
        if (HasGameEnded())
        {
            EndGame();
            return;
        }

        rollButton.SetActive(true);
        loadingText.SetActive(false);
        stats.Toggle(false);
        displayImage.enabled = false;

        if (PokeApi.currentPokemonIndex > 0 )
        {
            BTN_Roll();
        }
    }

    public void BTN_Roll()
    {
        rollButton.SetActive(false);
        stopButton.SetActive(true);
        rollCoroutine = StartCoroutine(Roll());
    }

    public void BTN_Stop()
    {
        stopButton.SetActive(false);
        StopCoroutine(rollCoroutine);
        StartCoroutine(ShowChosenPokemon(PokeApi.currentPokemonIndex));
        //ShowChosenPokemon(PokeApi.currentPokemonIndex);
        stats.Toggle(true);
    }

    private IEnumerator ShowChosenPokemon(int index)
    {
        yield return new WaitForEndOfFrame();
        displayImage.texture = PokeApi.chosenPokemonImages[index];
    }

    private IEnumerator Roll()
    {
        displayImage.enabled = true;
        while (enabled)
        {
            yield return rollWait;
            DisplayRandomPokemon();
        }
    }

    private void DisplayRandomPokemon()
    {
        Texture2D randomPoke = pokemonImages[Random.Range(0, pokemonImages.Length)];
        displayImage.texture = randomPoke;
    }

    private bool HasGameEnded()
    {
        return PokeApi.currentPokemonIndex >= NUM_POKEMON || 
            (PokeApi.currentPokemonIndex >= NUM_POKEMON -1 && !stats.HasReleasedPokemon());
    }

    private void EndGame()
    {
        // PUT SOME STUFF HERE??
        displayImage.enabled = false;
    }
}

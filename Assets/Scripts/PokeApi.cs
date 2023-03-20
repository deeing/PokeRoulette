using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Defective.JSON;

// created to interface with PokeApi
public class PokeApi
{
    public static JSONObject allPossiblePokemon;

    public static List<JSONObject> chosenPokemon = new List<JSONObject>();
    public static List<Texture2D> chosenPokemonImages = new List<Texture2D>();

    public static int currentPokemonIndex = 0;

    public static IEnumerator GetTotalPokemon()
    {
        UnityWebRequest uwr = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon-species?limit=10000");
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error While Sending: " + uwr.error);
        }
        else
        {
            allPossiblePokemon = new JSONObject(uwr.downloadHandler.text);
        }
    }

    public static IEnumerator LoadPokemon()
    {
        List<JSONObject> allPokemon = allPossiblePokemon["results"].list;
        int randomPokemon = UnityEngine.Random.Range(0, allPokemon.Count);
        UnityWebRequest uwr = UnityWebRequest.Get(allPokemon[randomPokemon]["url"].stringValue);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Error While Sending: " + uwr.error);
        }
        else
        {
            JSONObject pokeData = new JSONObject(uwr.downloadHandler.text);

            // extract the first variety
            JSONObject firstVariety = pokeData["varieties"][0]["pokemon"];

            UnityWebRequest req = UnityWebRequest.Get(firstVariety["url"].stringValue);
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error While Sending: " + req.error);
            }
            else
            {
                JSONObject chosenPoke = new JSONObject(req.downloadHandler.text);

                // extract the first variety
                chosenPokemon.Add(chosenPoke);

                // load pokemon image
                UnityWebRequest imageReq = UnityWebRequestTexture.GetTexture(chosenPoke["sprites"]["other"]["official-artwork"]["front_default"].stringValue);
                yield return imageReq.SendWebRequest();

                if (imageReq.result == UnityWebRequest.Result.ConnectionError)
                {
                    chosenPokemonImages.Add(null);
                }
                else
                {
                    Texture2D pokeImage = DownloadHandlerTexture.GetContent(imageReq);

                    // extract the first variety
                    chosenPokemonImages.Add(pokeImage);
                }
            }
        }
    }

    public static void DebugPokemonList()
    {
        for(int i=0; i < chosenPokemon.Count; i++)
        {
            Debug.Log(chosenPokemon[i]["name"] + ":" + chosenPokemon[i]);
            Debug.Log(chosenPokemonImages[i]);
        }
    }

    public static JSONObject GetCurrentPokemon()
    {
        return chosenPokemon[currentPokemonIndex];
    }

    public static Texture2D GetCurrentPokemonImage()
    {
        return chosenPokemonImages[currentPokemonIndex];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSaveShop : MonoBehaviour
{
    public playerInfo playerData; // set the save data to this obj
    public AudioManager audio;
    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        // set the level and menu currency
        // easier in tower info, so if need modification to tower check that
        PlayerPrefs.SetInt("menuCurr", playerData.GetMenuCurrency());
        PlayerPrefs.SetInt("levelCurr", playerData.GetLevelCurrency());
        PlayerPrefs.SetFloat("audioLevels", audio.GetVolume());
        
    }

    public void LoadGame()
    {
        // load the keys of player data, towers loaded in tower info, its easier
        // dont need to create an array of towers etc because its within the tower obj
        playerData.SetMenuCurrency(PlayerPrefs.GetInt("menuCurr"));
        playerData.SetLevelCurrency(PlayerPrefs.GetInt("levelCurr"));
        playerData.SetHas(PlayerPrefs.GetInt("Intro")); // See if player has gone to 
        audio.changeVolume(PlayerPrefs.GetFloat("audioLevels"));
    }
}

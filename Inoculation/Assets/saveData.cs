using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class saveData : MonoBehaviour
{
    public playerInfo playerData;

    public GameObject player;
    // Start is called before the first frame update
    public void save()
    {
        // Save the player data
        PlayerPrefs.SetInt("playerLevelCurr", playerData.GetLevelCurrency());
        PlayerPrefs.SetInt("playerMenuCurr", playerData.GetMenuCurrency());
        PlayerPrefs.SetInt("completeLevels", playerData.GetCompleteLevel());
        
        // save the players x and y positions
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        
    }
    
}

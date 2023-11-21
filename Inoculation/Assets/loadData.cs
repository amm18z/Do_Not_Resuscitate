using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class loadData : MonoBehaviour
{
    public playerInfo playerData;

    public GameObject player;
    // Start is called before the first frame update
    public void load()
    {
        
        // sets level currency
        playerData.SetLevelCurrency(PlayerPrefs.GetInt("playerLevelCurr"));
        // sets menu currency
        playerData.SetMenuCurrency(PlayerPrefs.GetInt("playerMenuCurr"));
        // set level count
        playerData.SetCompleteLevel(PlayerPrefs.GetInt("completeLevels"));
        // set player position. In 2d Array, Sets X and Y positions moving the player
        player.transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"));

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveGame : MonoBehaviour
{
   
    public playerInfo playerData;

    public GameObject player;
    // Start is called before the first frame update
    public void SaveGame()
    {
        // set the players location var
        Vector3 playerLoc = player.transform.position;
        PlayerPrefs.SetFloat("playerX", playerLoc.x); // add x, y, z
        PlayerPrefs.SetFloat("playerY", playerLoc.y); // save player loc
        PlayerPrefs.SetFloat("playerZ", playerLoc.z);
        // save currencys
        PlayerPrefs.SetInt("menuCurr", playerData.GetMenuCurrency());
        PlayerPrefs.SetInt("levelCurr", playerData.GetLevelCurrency());
        PlayerPrefs.SetInt("completedLevels", playerData.GetCompletedLevels());
    }

    public void LoadGame()
    {
        
        Vector3 playerLoc; // gather players location in var
        playerLoc.x = PlayerPrefs.GetFloat("playerX"); // set the x, y, z
        playerLoc.y = PlayerPrefs.GetFloat("playerY");
        playerLoc.z = PlayerPrefs.GetFloat("playerZ");
        player.transform.position = playerLoc; // set to the player loc var
        // set the currencys from key
        playerData.SetMenuCurrency(PlayerPrefs.GetInt("menuCurr"));
        playerData.SetLevelCurrency(PlayerPrefs.GetInt("levelCurr"));
        // set levels completed
        playerData.SetCompletedLevels(PlayerPrefs.GetInt("completedLevels"));
    }

    public void AddMoney()
    {
        playerData.AddMoney();
    }
    public void SubMoney()
    {
        playerData.SubMoney();
    }
}

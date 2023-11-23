using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSaveShop : MonoBehaviour
{
    public playerInfo playerData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("menuCurr", playerData.GetMenuCurrency());
        PlayerPrefs.SetInt("levelCurr", playerData.GetLevelCurrency());
    }

    public void LoadGame()
    {
        playerData.SetMenuCurrency(PlayerPrefs.GetInt("menuCurr"));
        playerData.SetLevelCurrency(PlayerPrefs.GetInt("levelCurr"));
        
    }
}

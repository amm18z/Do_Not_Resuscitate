using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerInfo : MonoBehaviour
{
    public int price; // price of tower
    public string TowerName; // tower key for finding in player pref
    // key can be set within the template object
    public GameObject TowerUI;
    // Start is called before the first frame update
    void Start()
    {
        // check saved towers
        if (PlayerPrefs.GetInt(TowerName) == 1)
        { // remove the tower if purchase
            RemoveTower();
        }
        
    }
    

    public int GetTowerPrice()
    { 
        // get tower price 
        return price;
    }
    

    public void RemoveTower()
    { 
        // set the tower obj active
        TowerUI.SetActive(false);
        
    }

    public void SaveTower()
    {
        // saves boolean 1, since bool type isnt in player prefs for some reason
        PlayerPrefs.SetInt(TowerName, 1);
    }
    public void UnSaveTower()
    { // remove purchase , debug
        PlayerPrefs.SetInt(TowerName, 0);
        AddTower();
    }

    public int isTowerPurchased()
    { 
        // check if the player has it
        // 1 - true
        // 0 - false
        // can make into bool if anyone needs
        return PlayerPrefs.GetInt(TowerName); 
    }
        
    public void AddTower()
    {
        TowerUI.SetActive(true);
    }
    
}

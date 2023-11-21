using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public int completeLevel;
    public int levelCurrency;
    public int menuCurrency;
    public GameObject player;
    private Vector2 playerPos;
    private Rigidbody2D rb;
    

    public static playerInfo Instance;

    public void Start()
    {
        
    }

    private void Awake()
    {
        // Only have one instance at a time
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    

    // Set number of completed levels up to
    public void SetCompleteLevel(int numLevel)
    {
        completeLevel = numLevel;
    }

    // Return number of completed levels
    public int GetCompleteLevel()
    {
        return completeLevel;
    }

    // increases completed level count by one
    public void NewCompleteLevel()
    {
        this.completeLevel++;
    }

    // get the number of menu currency
    public int GetMenuCurrency()
    {
        return menuCurrency;
    }
    
    // get the level currency
    public int GetLevelCurrency()
    {
        return levelCurrency;
    }

    // set the level currency
    public void SetLevelCurrency(int money)
    {
        levelCurrency = money;
    }
    
    // set the menu currency
    public void SetMenuCurrency(int money)
    {
        menuCurrency = money;
    }

    // add money
    public void ModifyLevelCurrency(int money)
    {
        levelCurrency = levelCurrency + money;
    }
    
    // add mony
    public void ModifyMenuCurrency(int money)
    {
        menuCurrency = menuCurrency + money;
    }
    // adds 100 for debug
    public void AddMoney()
    {
        this.ModifyLevelCurrency(100);
        this.ModifyMenuCurrency(100);
    }
    // removes 100 for debug
    public void RemoveMoney()
    {
        this.ModifyLevelCurrency(-100);
        this.ModifyMenuCurrency(-100);
    }


}

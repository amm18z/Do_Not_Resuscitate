using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public int completeLevels;
    public int menuCurrency;
    public int levelCurrency;

    public purchaseItem UpdateObject;
    
    

    public static playerInfo Instance;

    public void Start()
    {
        
    }

    private void Awake()
    {
        // ensure single instance
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int GetLevelCurrency()
    {
        return levelCurrency;
    }

   
    
    public int GetMenuCurrency()
    {
        return menuCurrency;
    }

    public int GetCompletedLevels()
    {
        return completeLevels;
    }

    public void SetLevelCurrency(int money)
    {
        // set level curr
        levelCurrency = money;
        
        
    }
    public void SetMenuCurrency(int money)
    {
        // set memu curr
        menuCurrency = money;
        
    }

    public void SetCompletedLevels(int levels)
    {
        // reset completed levels
        completeLevels = levels;
    }

    public void AddMoney()
    {
        // add button for debug
        menuCurrency = menuCurrency + 100;
        levelCurrency = levelCurrency + 100;
        UpdateObject.UpdateMenuCurrency(); // update money
    }
    public void SubMoney()
    {
        // sub button for debug
        menuCurrency = menuCurrency - 100;
        levelCurrency = levelCurrency - 100;
        UpdateObject.UpdateMenuCurrency(); // update money
    }

    public void IncrementCompletedLevels()
    {
        // complete another level
        completeLevels++;
        
    }
    

    public void ModifyLevelCurrency(int money)
    {
        // add money
        levelCurrency = levelCurrency + money;
        
    }
    public void ModifyMenuCurrency(int money)
    {
        // add money
        menuCurrency = menuCurrency + money;
        
    }

   
    


}

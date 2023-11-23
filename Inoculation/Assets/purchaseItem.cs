using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class purchaseItem : MonoBehaviour
{

    public TextMeshProUGUI MenuCurrencyText;
    
    public gameSaveShop SaveShop;

    public playerInfo PlayerData;

    
    // Start is called before the first frame update
    void Start()
    {
        SaveShop.LoadGame();
        
        
        // only called on start so it doesnt need to be repeatedly updated
        MenuCurrencyText.text = "x" + PlayerData.GetMenuCurrency().ToString();
        // update only when purchase / load into shop
    }
    
    public void MakePurchase(towerInfo TowerObject)
    {
        int TowerPrice = TowerObject.GetTowerPrice(); // get price
        if (PlayerData.GetMenuCurrency() >= TowerPrice) // if player can afford
        {
            TowerObject.SaveTower(); // save the tower when purchased
            TowerObject.RemoveTower(); // remove the tower from ui
            PlayerData.ModifyMenuCurrency(-(TowerPrice)); // remove price from bal
            SaveShop.SaveGame(); // save game upon purchases
            MenuCurrencyText.text = "x" + PlayerData.GetMenuCurrency().ToString(); // update currency
        }
        else
        {
         SaveShop.SaveGame();   // otherwise might as well save when cant afford
         // need to add "Cant afford etc"
        }
    }

    

    // Update is called once per frame
    void Update()
    { 
        
    }
}

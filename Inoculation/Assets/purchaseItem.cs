using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class purchaseItem : MonoBehaviour
{

    public TextMeshProUGUI MenuCurrencyText;
    public GameObject UnableToPurchaseScreen;
    public gameSaveShop SaveShop; 
    public playerInfo PlayerData;

    
    // Start is called before the first frame update
    void Start()
    {
        SaveShop.LoadGame(); // load saved progress
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
            UpdateMenuCurrency();
        }
        else
        {
         SaveShop.SaveGame();   // otherwise might as well save when cant afford
         UnableToPurchaseScreen.SetActive(true); // Turn on unable to purchase canvas
        }
    }

    

    // Only Update When Needed
    public void UpdateMenuCurrency()
    { 
        MenuCurrencyText.text = "x" + PlayerData.GetMenuCurrency().ToString(); // update currency
    }
}

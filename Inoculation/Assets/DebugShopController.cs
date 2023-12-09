using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class DebugShopController : MonoBehaviour
{
    public bool IsDebugOn;
    public GameObject ResetButtonUi;
    public playerInfo PlayerData; 
    public TextMeshProUGUI levelCurrencyText;
    public TextMeshProUGUI menuCurrencyText;
    public GameObject DebugUI;
    private void Start()
    {
        DebugStart();
    }

    public void DebugStart()
    {
        if (IsDebugOn)
        {
            TurnOnObject(ResetButtonUi); // Turn on Reset Button
            TurnOnObject(DebugUI); // Turn on Debug Canvas
            TurnOnPlayerData(); // Update Player Data once
        }
        
    }

    public void TurnOnObject(GameObject UiObject)
    {
        UiObject.SetActive(true);
    }
    public void TurnOffObject(GameObject UiObject)
    {
        UiObject.SetActive(false);
    }

    public void TurnOnPlayerData()
    {
        // get loaded bal -- shouldnt update unless called
        levelCurrencyText.text = "x" + PlayerData.levelCurrency.ToString();
        menuCurrencyText.text = "x" + PlayerData.menuCurrency.ToString();
    }

  

}

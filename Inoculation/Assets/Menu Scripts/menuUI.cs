using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menuUI : MonoBehaviour
{
    public TextMeshProUGUI levelCurrencyText;
    public TextMeshProUGUI menuCurrencyText;
    public playerInfo PlayerData;
    

    // Start is called before the first frame update
    void Start()
    {
        levelCurrencyText.text = "x" + PlayerData.GetLevelCurrency().ToString();
        menuCurrencyText.text = "x" + PlayerData.GetLevelCurrency().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
        // text parameters should be updated here by calling PlayerData.GetMenuCurrency() when the PlayerData class is implemented
        levelCurrencyText.text = "x" + PlayerData.levelCurrency.ToString();
        menuCurrencyText.text = "x" + PlayerData.menuCurrency.ToString();
    }

    
}

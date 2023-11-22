using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menuUI : MonoBehaviour
{
    public TextMeshProUGUI levelCurrencyText;
    public TextMeshProUGUI menuCurrencyText;

    int bandaids = 0; // temporary, will delete when PlayerData class is implemented
    int prescriptions = 0; // temporary ^

    // Start is called before the first frame update
    void Start()
    {
        levelCurrencyText.text = "x" + bandaids.ToString();
        menuCurrencyText.text = "x" + prescriptions.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // text parameters should be updated here by calling PlayerData.GetMenuCurrency() when the PlayerData class is implemented
        // levelCurrencyText.text = "x" + PlayerData.GetLevelCurrency().ToString();
        // menuCurrencyText.text = "x" + PlayerData.GetMenuCurrency().ToString();
    }
}

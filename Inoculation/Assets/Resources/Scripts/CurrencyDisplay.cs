using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelCurrencyText;
    void Start()
    {
        levelCurrencyText.text = playerInfo.Instance.GetLevelCurrency().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        levelCurrencyText.text = playerInfo.Instance.GetLevelCurrency().ToString();
    }
}

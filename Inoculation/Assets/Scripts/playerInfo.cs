using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] completeLevels;
    public int completeLevelsSize;
    public int bal;
    public int levelCurrency;
    public int menuCurrency;

    public static playerInfo Instance;

    private void Awake()
    {
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

    public void SetLevelCurrency(int money)
    {
        levelCurrency = money;
    }

    public void ModifyLevelCurrency(int money)
    {
        levelCurrency = levelCurrency + money;
    }


}

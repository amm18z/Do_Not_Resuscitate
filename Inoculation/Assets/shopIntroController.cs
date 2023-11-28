using UnityEngine;

public class ShopIntroController : MonoBehaviour
{
    public GameObject firstScreen;
    public GameObject secondScreen;
    public GameObject thirdScreen;
    public playerInfo playerData;
    private bool hasPlayed;
    public bool debugMode;

    private void Start()
    {
        hasPlayed = playerData.HasShopIntroPlayed();
        if (hasPlayed || (!debugMode)) // debug mode overrides
        {
            SetAllInactive(); // turn off all screens (1 is on by default)
        }
    }

    // Modularized method to turn off screens
    private void TurnOffScreen(GameObject screenToTurnOff, GameObject nextScreen)
    {
        
        screenToTurnOff.SetActive(false);  // turn off screen
        nextScreen.SetActive(true); // turn on next
        
    }

    public void TurnOffFirstScreen()
    {
        TurnOffScreen(firstScreen, secondScreen); // 1 to 2
    }

    public void TurnOffSecondScreen()
    {
        TurnOffScreen(secondScreen, thirdScreen);
    }

    public void TurnOffThirdScreen()
    {
        thirdScreen.SetActive(false);
        PlayerPrefs.SetInt("shopIntro", 1); // shop intro has played fully
    }

    private void SetAllInactive()
    {
        // turn off all
        firstScreen.SetActive(false);
        secondScreen.SetActive(false);
        thirdScreen.SetActive(false);
    }
}
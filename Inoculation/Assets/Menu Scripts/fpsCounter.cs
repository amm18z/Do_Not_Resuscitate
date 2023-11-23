using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombinedScript : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private Dictionary<int, string> CachedNumberStrings = new();
    private int[] frameRates;
    private int cacheNum = 300;
    private int _averageFromAmount = 30;
    private int _averageCounter = 0;
    private int currAvg;
    private bool isF3Pressed = false;

    private GameObject player;
    public GameObject add;
    public GameObject sub;
    

    private KeyCode toggleKey = KeyCode.F3; // Key to toggle the Text

    private void Awake()
    {
        for (int i = 0; i < cacheNum; i++)
        {
            CachedNumberStrings[i] = i.ToString();
        }
        frameRates = new int[_averageFromAmount];

        player = GameObject.Find("playerObject"); // get player obj
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isF3Pressed = !isF3Pressed;
            Text.enabled = isF3Pressed; // Enable or disable the Text component
            //locationText.enabled = isF3Pressed; // Enable or disable the locationText component
        }

        if (isF3Pressed)
        {
            // FPS Counter
            // Sample
            var currFrame = (int)Math.Round(1f / Time.smoothDeltaTime);
            frameRates[_averageCounter] = currFrame;

            // Average
            var average = 0f;
            foreach (var frameRate in frameRates)
            {
                average += frameRate;
            }
            currAvg = (int)Math.Round(average / _averageFromAmount);
            _averageCounter = (_averageCounter + 1) % _averageFromAmount;

            // Assign to UI
            Text.text = currAvg switch
            {
                var x when x >= 0 && x < cacheNum => CachedNumberStrings[x],
                var x when x >= cacheNum => $"> {cacheNum}",
                var x when x < 0 => "< 0",
                _ => "?"
            };

            // Player Location
            //locationText.text = player.transform.position.ToString();
            // Turn on Add and Remove button
            add.gameObject.SetActive(true);
            sub.gameObject.SetActive(true);
        }
        else
        {   
            // Turn Off Add And Remove 
            add.gameObject.SetActive(false);
            sub.gameObject.SetActive(false);
            Text.text = ""; // Clear the text when F3 is not pressed
            //locationText.text = ""; // Clear the text when F3 is not pressed
        }
    }
}

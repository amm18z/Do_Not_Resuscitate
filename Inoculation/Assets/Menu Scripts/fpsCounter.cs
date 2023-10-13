using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI Text;
 
    private Dictionary<int, string> CachedNumberStrings = new();
    private int[] frameRates;
    private int cacheNum = 300;
    private int _averageFromAmount = 30;
    private int _averageCounter = 0;
    private int currAvg;
    
   
 
    void Awake()
    {
        // Cache strings and create array
        {
            for (int i = 0; i < cacheNum; i++) {
                CachedNumberStrings[i] = i.ToString();
            }
            frameRates = new int[_averageFromAmount];
        }
        
    }
    void Update()
    {
        // Sample
        {
            var currFrame = (int)Math.Round(1f / Time.smoothDeltaTime); 
            frameRates[_averageCounter] = currFrame;
        }
 
        // Average
        {
            var average = 0f;
 
            foreach (var frameRate in frameRates) {
                average += frameRate;
            }
 
            currAvg = (int)Math.Round(average / _averageFromAmount);
            _averageCounter = (_averageCounter + 1) % _averageFromAmount;
        }
 
        // Assign to UI
        {
            Text.text = currAvg switch
            {
                var x when x >= 0 && x < cacheNum => CachedNumberStrings[x],
                var x when x >= cacheNum => $"> {cacheNum}",
                var x when x < 0 => "< 0",
                _ => "?"
            };
        }
    }
}
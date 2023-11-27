using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimePlayer : MonoBehaviour
{
    public GameObject FirstTimeGameObject;
    public GameObject CanvasFirstTime;

    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("hasPlayed") != 1)
        {
            FirstTimeGameObject.SetActive(true);
            
            
        }
        else
        {
            CanvasFirstTime.SetActive(false);
        }
    }

    
}

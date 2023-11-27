using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimePlayer : MonoBehaviour
{
    public GameObject FirstTimeGameObject;
    public GameObject CanvasFirstTime;

    public bool DebugMode;

    
    // Start is called before the first frame update
    void Start()
    {
        if ((PlayerPrefs.GetInt("hasPlayed") != 1) || (DebugMode))
        {
            FirstTimeGameObject.SetActive(true);
            
            
        }
        else
        {
            CanvasFirstTime.SetActive(false);
        }
    }

    public bool isOn()
    {
        if (CanvasFirstTime.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

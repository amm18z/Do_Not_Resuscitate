using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] completeLevels;
    public int completeLevelsSize;
    public int bal;

    public static playerInfo instance;

    playerInfo()
    {
        if (instance != null)
            GameObject.Destroy(this.gameObject);
        else
            instance = this;
    }

}

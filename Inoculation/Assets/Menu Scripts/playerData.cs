using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class playerData
{
    public float[] playerPosition;
    public int[] completedLevels;
    public int balCount;

    public playerData(playerInfo player)
    {
        for (int i = 0; i < player.completeLevelsSize; i++)
        {
            completedLevels[i] = player.completeLevels[i];
            
            //playerPosition[0] = pos.transform.position.x;
            //playerPosition[1] = pos.transform.position.y;
            
        }
        balCount = player.bal;
        playerPosition = new float[2];
    }


}

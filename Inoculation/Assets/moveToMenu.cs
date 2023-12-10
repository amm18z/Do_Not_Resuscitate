using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class moveToMenu : MonoBehaviour
{
    public int sceneIndex; 
    public void Move()
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single); // Load the shop scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

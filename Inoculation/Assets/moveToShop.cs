using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToShop : MonoBehaviour
{
    public int sceneIndex;
    public GameObject doorPrompt;

    private void OnTriggerEnter2D(Collider2D other) // If the player enters the collider move them
    {
        print("Trigger Entered"); // For Debug
        if (other.tag == "Player") // Identify if the obj passed is a player
        {
            doorPrompt.SetActive(true);
        }
    }
    public void YesChoice()
    {
        print("Switching Scenes");
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single); // Load the shop scene
    }
    public void NoChoice()
    {
        doorPrompt.SetActive(false);
    }
}
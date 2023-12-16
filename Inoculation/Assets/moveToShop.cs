using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToShop : MonoBehaviour
{
    public int sceneIndex;
    public GameObject scenePrompt;
    public PlayerController playerLogic;
    public saveGame game;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D other) // If the player enters the collider move them
    {
        print("Trigger Entered"); // For Debug
        if (other.tag == "Player") // Identify if the obj passed is a player
        {
            audioManager.PlaySFX(audioManager.Prompt);
            scenePrompt.SetActive(true);
            playerLogic.moveSpeed = 0.0f;
        }
        
    }
    public void YesChoice()
    {
        audioManager.PlaySFX(audioManager.Confirm);
        game.SaveGame();
        print("Switching Scenes");
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single); // Load the shop scene
    }
    public void NoChoice()
    {
        audioManager.PlaySFX(audioManager.Decline);
        scenePrompt.SetActive(false);
        playerLogic.moveSpeed = 2.0f;
    }
}
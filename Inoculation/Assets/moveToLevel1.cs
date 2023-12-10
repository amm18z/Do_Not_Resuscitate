using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    public int sceneIndex;
    public GameObject scenePrompt;
    public PlayerController playerLogic;
    public saveGame SaveGame;
    void Start()
    {
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D other) // If the player enters the collider move them
    {
        print("Trigger Entered"); // For Debug
        if (other.tag == "Player") // Identify if the obj passed is a player
        {
            scenePrompt.SetActive(true);
            playerLogic.moveSpeed = 0.0f;
        }
    }
    public void YesChoice()
    {
        print("Switching Scenes");
        SaveGame.SaveGame(); // Saves Game on entry
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single); // Load the level scene
    }
    public void NoChoice()
    {
        scenePrompt.SetActive(false);
        playerLogic.moveSpeed = 2.0f;
    }
}

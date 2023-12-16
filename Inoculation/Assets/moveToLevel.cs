using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public int sceneIndex;
    public float minimumLoadTime;
    public GameObject sceneLoad;
    public GameObject iconLoad;
    public GameObject scenePrompt;
    public PlayerController playerLogic;
    public saveGame SaveGame;
    private AsyncOperation asyncLoad;

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
        //print("Trigger Entered"); // For Debug
        int lastCompletedLevel = playerInfo.Instance.GetCompletedLevels();
        if ((sceneIndex - 3) <= lastCompletedLevel && other.tag == "Player") // Identify if the obj passed is a player
        {
            audioManager.PlaySFX(audioManager.Prompt);
            scenePrompt.SetActive(true);
            playerLogic.moveSpeed = 0.0f;
        }
    }
    public void YesChoice()
    {
        audioManager.PlaySFX(audioManager.Confirm);
        print("Switching Scenes");
        SaveGame.SaveGame(); // Saves Game on entry
        StartCoroutine(LoadAsyncScene()); // Load the level scene
    }
    public void NoChoice()
    {
        audioManager.PlaySFX(audioManager.Decline);
        scenePrompt.SetActive(false);
        playerLogic.moveSpeed = 2.0f;
    }

    private IEnumerator LoadAsyncScene()
    {
        sceneLoad.SetActive(true);
        iconLoad.SetActive(true);
        float currentLoadTime = 0f;
        while (currentLoadTime < minimumLoadTime)
        {
            currentLoadTime += Time.deltaTime;
            yield return null;
        }
        asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single); // Load the level scene
        while (!asyncLoad.isDone)
        {
            currentLoadTime += Time.deltaTime;
            yield return null;
        }

    }

}

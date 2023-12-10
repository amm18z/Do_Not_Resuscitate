using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveToLevel1 : MonoBehaviour
{
    // Start is called before the first frame update
    public int sceneIndex;
    public float minimumLoadTime;  
    public GameObject scenePrompt;
    public GameObject sceneLoad;
    public GameObject iconLoad;
    public PlayerController playerLogic;
    private AsyncOperation asyncLoad;
   

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
        scenePrompt.SetActive(false);
        StartCoroutine(LoadAsyncScene()); //waits for loading to be true after scene loading call
    }
    
    public void NoChoice()
    {
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
        while(!asyncLoad.isDone)
        {
          currentLoadTime += Time.deltaTime;
          yield return null;
        }

    }
   
}

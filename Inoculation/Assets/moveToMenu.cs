using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class moveToMenu : MonoBehaviour
{
    public int sceneIndex;
    public float minimumLoadTime;
    public GameObject sceneLoad;
    public GameObject iconLoad;
    public GameObject screenDisable1;
    public GameObject screenDisable2;
    public GameObject screenDisable3;
    public GameObject screenDisable4;
    private AsyncOperation asyncLoad;
    public void Move()
    {
        screenDisable1.SetActive(false);
        screenDisable2.SetActive(false);
        screenDisable3.SetActive(false);
        screenDisable4.SetActive(false);
        StartCoroutine(LoadAsyncScene()); // Load the shop scene
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

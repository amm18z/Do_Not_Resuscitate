using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{ 
    // Scene Index
    public int sceneToLoad; 
   // Call with the button CLick 
    public void LoadSceneOnClick()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
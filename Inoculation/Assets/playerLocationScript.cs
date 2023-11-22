using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class playerLocationScript : MonoBehaviour
{
    private GameObject player;
    public TextMeshProUGUI Text;
    public GameObject Button;
    public playerInfo playerData;

    private bool isTextVisible = false; // Flag to toggle the Text visibility
    private KeyCode toggleKey = KeyCode.F4; // Key to toggle the Text

    private void Awake()
    {
        player = GameObject.Find("playerObject");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleKey)) // Use GetKeyDown to toggle the visibility
        {
            isTextVisible = !isTextVisible;
        }

        if (isTextVisible)
        {
            Text.text = player.transform.position.ToString();
            Text.gameObject.SetActive(true); // Show the Text component
            Button.gameObject.SetActive(true);
        }
        else
        {
            Text.gameObject.SetActive(false); // Hide the Text component
            Button.gameObject.SetActive(false);
        }
    }

    
}
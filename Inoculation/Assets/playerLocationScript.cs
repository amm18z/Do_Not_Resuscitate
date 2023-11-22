using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerLocationScript : MonoBehaviour
{
    private GameObject player;
    public TextMeshProUGUI Text;

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
        }
        else
        {
            Text.gameObject.SetActive(false); // Hide the Text component
        }
    }
}
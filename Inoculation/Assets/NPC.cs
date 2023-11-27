using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;
    public FirstTimePlayer FirstPlayer;

    public float wordSpeed;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && playerInRange && !FirstPlayer.isOn()) 
        {
            if (!dialoguePanel.activeInHierarchy) 
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
            else if(dialogueText.text == dialogue[index])
            {
                NextLine();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && dialoguePanel.activeInHierarchy) 
        {
            ZeroText();
        }
    }

    public void ZeroText() 
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing() 
    {
        foreach(char letter in dialogue[index].ToCharArray()) 
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine() 
    {
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            ZeroText();
        }
    }
}

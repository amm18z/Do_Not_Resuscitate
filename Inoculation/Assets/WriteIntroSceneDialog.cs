
using System.Collections;
using TMPro;
using UnityEngine;

public class WriteIntroSceneDialog : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public string[] dialogArray;
    public float typingSpeed = 0.05f;

    private void Start()
    {
        StartCoroutine(TypeDialog());
    }

    public void run()
    {
        StartCoroutine(TypeDialog());
    }

    IEnumerator TypeDialog()
    {
        foreach (string sentence in dialogArray)
        {
            yield return StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // You can add a delay before moving to the next sentence if needed
        yield return new WaitForSeconds(1f);
    }
}

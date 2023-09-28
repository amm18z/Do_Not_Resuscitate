using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class playerLocationScript : MonoBehaviour
{
    private GameObject player;
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake(){
        player = GameObject.Find("playerObject");
    }
    // Update is called once per frame
    void Update()
    {
        Text.text = player.transform.position.ToString();

    }
}

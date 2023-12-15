using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacement : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject selectedObject;

    private Vector3 pos;

    private RaycastHit hit;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private GameObject tower1LockCover;
    [SerializeField] private GameObject tower2LockCover;
    [SerializeField] private GameObject tower3LockCover;
    [SerializeField] private GameObject tower4LockCover;
    [SerializeField] private GameObject tower5LockCover;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInfo.Instance.SetLevelCurrency(250);

        if (PlayerPrefs.GetInt("crossbow") == 0)
        {
            tower1LockCover.SetActive(true);
        }
        if (PlayerPrefs.GetInt("sanitizer") == 0)
        {
            tower2LockCover.SetActive(true);
        }
        if (PlayerPrefs.GetInt("sodamachine") == 0)
        {
            tower3LockCover.SetActive(true);
        }
        if (PlayerPrefs.GetInt("bandaidminigun") == 0)
        {
            tower4LockCover.SetActive(true);
        }

        if (PlayerPrefs.GetInt("jimmy") == 0)
        {

            tower5LockCover.SetActive(true);
        }
        // waiting for towers 4 and 5 to be added to the shop before they can be unlocked
    }
        
    

    // Update is called once per frame
    void Update()
    {
        if(selectedObject != null)
        {
            selectedObject.transform.position = pos;

            if (Input.GetMouseButtonDown(0))
            {
                TowerIsAwake(true);
                if (playerInfo.Instance.GetLevelCurrency() >= selectedObject.GetComponent<Tower>().getCost())
                {
                    playerInfo.Instance.ModifyLevelCurrency(-(selectedObject.GetComponent<Tower>().getCost()));
                    selectedObject.GetComponent<Tower>().isPlaced = true;   //neccessary to make sure Tower5
                    PlaceObject();
                    audioManager.PlaySFX(audioManager.PlaceTower);
                }
                else
                {
                    Destroy(selectedObject);
                    audioManager.PlaySFX(audioManager.Denied);
                }
                
            }
        }
    }

    public void PlaceObject()
    {
        selectedObject = null;  // tower had been placed
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        pos = Camera.main.ScreenToWorldPoint(mousePos); // Tower prefab will follow mouse until placed.
    }

    public void SelectObject(int index)
    {
        selectedObject = Instantiate(objects[index], pos, Quaternion.identity);
        TowerIsAwake(false);
    }
    
    // ------------------------------ How To Get Towers ------------------------------ //  
    // To Check if Tower is purchased Check The player prefs.
    // Just Get the tower key from the shop info. Its stored as an int (1- true, 0- false)
    // To Check: PlayerPrefs.GetInt("CrossBow"); Should be subbed for appropriate key 
    // If You want to modify these look at the tower info script
    // Ask Aiden (me) if you need any help with this process lol
    // ------------------------------------------------------------------------------- // 
    
    private void TowerIsAwake(bool state)
    {
        selectedObject.GetComponent<Tower>().enabled = state;

        if (selectedObject.TryGetComponent<ProjectileShoot>(out ProjectileShoot script1))
        {
            script1.enabled = state;
        }

        if (selectedObject.TryGetComponent<PathSanitize>(out PathSanitize script2))
        {
            script2.enabled = state;
        }

        if (selectedObject.TryGetComponent<BoostActionSpeed>(out BoostActionSpeed script3))
        {
            script3.enabled = state;
        }
    }

}

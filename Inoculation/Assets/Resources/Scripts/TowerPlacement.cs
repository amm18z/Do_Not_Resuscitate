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

    // Start is called before the first frame update
    void Start()
    {
        playerInfo.Instance.SetLevelCurrency(200);
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
                    PlaceObject();
                }
                else
                {
                    Destroy(selectedObject);
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
    }

}

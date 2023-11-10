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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedObject != null)
        {
            selectedObject.transform.position = pos;

            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }





        /*Vector3 worldPosition;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(towerPrefab, worldPosition, Quaternion.identity);
        }*/

    }

    public void PlaceObject()
    {
        selectedObject = null;
    }

    private void FixedUpdate()
    {
        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }*/

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        pos = Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void SelectObject(int index)
    {
        
        selectedObject = Instantiate(objects[index], pos, Quaternion.identity);
    }
    
}
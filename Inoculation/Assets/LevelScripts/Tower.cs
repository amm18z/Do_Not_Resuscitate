using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public interface TowerAction
    {
        void performAction(Collider2D target);
    }

    [SerializeField]
    private int id = -1;

    [SerializeField]
    private int strength = 5;
    
    [SerializeField]
    private int cost = 0;
    
    [SerializeField]
    private TowerAction towerAction;

    private List<Enemy> visibleEnemies;

    // Start is called before the first frame update
    void Start()
    {
        visibleEnemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        // Choose an enemy from in range list
        // Perform action towards the chosen enemy
    }

    public int getId() { return id; }
    public int getStrength() { return strength; }
    public int getCost() { return cost; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When an object enters the tower's range, see if it's an enemy
        if(collision.gameObject.tag == "enemy")
        {
            // If it's an enemy, add the enemy data to the list of in range enemies
            Enemy temp = collision.gameObject.GetComponent<Enemy>();
            visibleEnemies.Add(temp);
        }
    }

    public static GameObject getTower(int id)
    {
        GameObject prefab = Resources.Load<GameObject>("tower_" + id.ToString());
        
        if (prefab == null)
        {
            Debug.LogError("Couldn't find tower prefab");
        }

        return prefab;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public abstract class TowerAction : MonoBehaviour
    {
        abstract public void performAction(GameObject enemy);
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
        if(visibleEnemies.Count > 0)
        {
            // Picks the enemy at the front of the list
            Enemy chosen_enemy = visibleEnemies[0];
            // Performs the tower's action against the chosen enemy
            towerAction.performAction(chosen_enemy.gameObject);
        }

        // Loop through all stored enemies to see if any are now dead
        for (int i = 0; i < visibleEnemies.Count; i++)
        {
            Enemy currEnemy = visibleEnemies[i];
            if (currEnemy.getHealth() <= 0)
            {
                visibleEnemies.RemoveAt(i);
            }
        }
    }

    public int getId() { return id; }
    public int getStrength() { return strength; }
    public int getCost() { return cost; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something entered trigger");
        // When an object enters the tower's range, see if it's an enemy
        if(collision.gameObject.tag == "enemy")
        {
            Debug.Log("Enemy entered range");
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

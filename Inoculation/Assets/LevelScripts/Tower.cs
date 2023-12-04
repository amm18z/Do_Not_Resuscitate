using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //animation thing
    public Animator animator;
    public abstract class TowerAction : MonoBehaviour
    {
        abstract public void performAction(GameObject enemy);
        abstract public void SetAnimationDelay(float delay);
    }

    [SerializeField]
    private int id = -1;

    [SerializeField]
    private int strength = 5;
    
    [SerializeField]
    private int cost = 0;
    
    [SerializeField]
    private TowerAction towerAction;

    public float shootingDelay = 0.0f;

    private Enemy chosen_enemy;

    private List<Enemy> enemy_queue;

    // Start is called before the first frame update
    void Start()
    {
        towerAction.SetAnimationDelay(shootingDelay);
        
    }

    private void Awake()
    {
        enemy_queue = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < enemy_queue.Count; i++)
        {
            if (enemy_queue[i] == null)
            {
                enemy_queue.RemoveAt(i);
                i--;
            }
        }

        towerAction.SetAnimationDelay(shootingDelay);
        // Rotate the tower towards chosen enemy and perform action
        if (chosen_enemy != null)
        {
            Vector2 shootDirection = chosen_enemy.transform.position - this.transform.position;
            shootDirection = shootDirection.normalized;
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            angle += 90;

            this.transform.rotation = Quaternion.Euler(0, 0, angle);

            towerAction.performAction(chosen_enemy.gameObject);
        }
    }

    public int getId() { return id; }
    public int getStrength() { return strength; }
    public int getCost() { return cost; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something entered trigger");
        // When an object enters the tower's range, see if it's an enemy
        if(collision.gameObject.tag == "enemy" && chosen_enemy == null)
        {
            Debug.Log("Enemy entered range");
            // If it's an enemy, add the enemy data to the list of in range enemies
            Enemy temp = collision.gameObject.GetComponent<Enemy>();
            chosen_enemy = temp;
        }
        else if(collision.gameObject.tag == "enemy")
        {
            Enemy temp = collision.gameObject.GetComponent<Enemy>();
            if (temp != null)
                enemy_queue.Add(temp);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            Enemy temp = collision.gameObject.GetComponent<Enemy>();
            if (chosen_enemy == temp)
            {
                chosen_enemy = null;
                if (enemy_queue.Count > 0)
                {
                    chosen_enemy = enemy_queue[0];
                    enemy_queue.RemoveAt(0);
                }
            }
                
            else if(enemy_queue.IndexOf(temp) > -1)
            {
                enemy_queue.Remove(temp);
            }
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

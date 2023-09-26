using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int levelDifficulty = 100;

    [SerializeField]
    private List<Transform> pathWaypoints;

    [SerializeField]
    private int waveCount = 5;
    private int currentWave = 0;
    private List<int> waveStrengths;
    private bool waveIsActive = false;

    [SerializeField]
    private EnemyType levelEnemyType;
    private List<GameObject> activeEnemies;
    

    // Start is called before the first frame update
    void Start()
    {
        // Initializes the lists for storing each wave's strength and the spawned enemies
        waveStrengths = new List<int>();
        activeEnemies = new List<GameObject>();

        // Splits the total difficulty evenly into the number of waves
        // A more complex function will be employed in the future to ensure waves increase in difficulty
        for(int i = 0; i < waveCount; i++)
        {
            waveStrengths.Add(levelDifficulty / waveCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Temporary action to trigger the start of the next wave
        if (!waveIsActive && Input.GetKeyDown(KeyCode.Space))
        {
            nextWave();
        }

        // If there is an active wave then the enemies should be moved along the path
        if (waveIsActive)
        {
            pathStep();
        }
    }

    private void pathStep()
    {
        // Goes through each stored enemy on the screen to move them along the path
        for(int i = 0; i < activeEnemies.Count; i++)
        {

            // Obtains the enemy data from the current enemy gameobject
            Enemy enemyData = activeEnemies[i].GetComponent<Enemy>();

            // Checks the enemy's health to see whether they should be destroyed
            if (enemyData.getHealth() <= 0)
            {
                // If the enemy is out of health destroy the enemy
                enemyData.destroyEnemy();

                // Remove the enemy from the list and step back an index to reorient the counter
                activeEnemies.Remove(enemyData.gameObject);
                i--;
                continue; // Skip the rest of the checks since the enemy is now destroyed
            }

            int waypointIndex = enemyData.getWaypointIndex();   // Waypoint index which the enemy last reached
            int nextWaypointIndex = waypointIndex + 1;          // Next waypoint for the enemy to move towards

            // Retrieves the position of the next waypoint which the enemy should move towards
            Vector2 nextPosition = pathWaypoints[nextWaypointIndex].position;
            // Calculates the next position which the enemy should be in
            // Moves the enemy's position toward the next point with a maximum step value derived from their speed
            Vector2 newEnemyPosition = Vector2.MoveTowards(enemyData.gameObject.transform.position, nextPosition, enemyData.getSpeed() * Time.deltaTime);

            // Moves the enemy to the newly calcualted position
            enemyData.move(newEnemyPosition);

            // Determines how far from the next waypoint the enemy is
            // If close enought, the enemy will now target the next waypoint
            float distance = Vector2.Distance(newEnemyPosition, nextPosition);
            if (Mathf.Abs(distance) < .01)
            {
                enemyData.nextWaypointIndex();
            }

            // If the enemy reaches the end of the path, the enemy is killed and removed from the list
            if(enemyData.getWaypointIndex() == pathWaypoints.Count - 1)
            {
                enemyData.damage(99999);
            }

        }
    }

    public void nextWave()
    {
        waveIsActive = true;
        StartCoroutine("runWave");
    }

    IEnumerator runWave()
    {
        // Wave strength will be the counter for how many enemies are spawned or their total strength
        while(waveStrengths[currentWave] > 0)
        {
            // Retrieves the list of enemy prefabs from the enemy type for the level
            List<GameObject> enemyPrefabs = levelEnemyType.getEnemyPrefabs();

            // Instantiates a new enemy
            GameObject spawnedEnemy = GameObject.Instantiate(enemyPrefabs[0]); // Currently selects the first enemy in the set though this can be later improved to consider variable strength enemies
            // Moves the enemy to the first waypoint along the path
            spawnedEnemy.transform.position = pathWaypoints[0].position;
            // Adds the enemy to the list of active enemies in the game
            activeEnemies.Add(spawnedEnemy);

            // Removes the enemy strength from the current wave strength
            waveStrengths[currentWave] -= 10;

            // Waits for 1 second before continuing the loop
            yield return new WaitForSeconds(1);
        }
        
        // After all the enemies have been spawned, continue waiting until there are no longer any enemies on the screen
        while (activeEnemies.Count > 0)
            yield return new WaitForSeconds(0.1f);


        currentWave += 1;
        waveIsActive = false;
    }
}

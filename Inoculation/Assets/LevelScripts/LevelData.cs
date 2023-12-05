using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField]
    private bool debugMode = false;

    public int levelNumber = 1;

    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int baseLevelDifficulty = 100;

    [SerializeField]
    private float waveDifficultyMultiplier = 1.5f;

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

    [SerializeField]
    private int enemyIndexLower = 0;
    [SerializeField]
    private int enemyIndexUpper = 0;

    private int baseEnemySpawningDelay = 6;

    private List<List<int>> waveEnemyCounts;

    [SerializeField]
    private int speedMultiplier; // enemy speed multiplier

    public TextMeshProUGUI waveCounter;
    public GameObject nextWaveLabel;
    
    // Start is called before the first frame update
    void Start()
    {

        // Initializes the lists for storing each wave's strength and the spawned enemies
        waveStrengths = new List<int>();
        activeEnemies = new List<GameObject>();
        waveEnemyCounts = new List<List<int>>();
        speedMultiplier = 1; // default speed
        // Splits the total difficulty evenly into the number of waves
        // A more complex function will be employed in the future to ensure waves increase in difficulty
        int tempWaveStrength = 0;
        for(int i = 0; i < waveCount; i++)
        {
            tempWaveStrength = Mathf.CeilToInt(baseLevelDifficulty * waveStrengthMultiplier(i));


            waveStrengths.Add(tempWaveStrength);
        }

        waveCounter.text = currentWave + " / " + waveCount;
        nextWaveLabel.SetActive(true);
    }

    private float waveStrengthMultiplier(int wave)
    {
        float finalMultiplier = 1;
        for (int i = 0; i < wave; i++)
            finalMultiplier *= waveDifficultyMultiplier;

        return finalMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        // Temporary action to trigger the start of the next wave
        if (debugMode && !waveIsActive && Input.GetKeyDown(KeyCode.Space))
        {
            nextWave();
        }

        // If there is an active wave then the enemies should be moved along the path
        if (waveIsActive)
        {
            pathStep();
            stillAlive();
        }
        else if(stillAlive() && currentWave == waveCount)
        {
            // Have completed all waves in the level
            // Trigger game won UI screen
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
                Enemy temp = enemyData;
                playerInfo.Instance.ModifyLevelCurrency(25);

                // Remove the enemy from the list and step back an index to reorient the counter
                activeEnemies.Remove(enemyData.gameObject);

                temp.destroyEnemy();
                i--;
                continue; // Skip the rest of the checks since the enemy is now destroyed
            }

            int waypointIndex = enemyData.getWaypointIndex();   // Waypoint index which the enemy last reached
            int nextWaypointIndex = waypointIndex + 1;          // Next waypoint for the enemy to move towards

            // Retrieves the position of the next waypoint which the enemy should move towards
            Vector2 nextPosition = pathWaypoints[nextWaypointIndex].position;
            // Calculates the next position which the enemy should be in
            // Moves the enemy's position toward the next point with a maximum step value derived from their speed
            Vector2 newEnemyPosition = Vector2.MoveTowards(enemyData.gameObject.transform.position, nextPosition, enemyData.getSpeed() * Time.deltaTime * speedMultiplier);

            // Moves the enemy to the newly calcualted position
            enemyData.move(newEnemyPosition);
            enemyData.GetComponent<Renderer>().sortingOrder = enemyData.GetComponent<Renderer>().sortingOrder + 1;

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
                Enemy temp = enemyData;

                activeEnemies.Remove(enemyData.gameObject);

                temp.destroyEnemy();
                i--;
            }

        }
    }

    public void nextWave()
    {
        if (currentWave == waveCount || waveIsActive) return;
        waveIsActive = true;
        currentWave += 1;

        nextWaveLabel.SetActive(false);

        waveCounter.text = currentWave + " / " + waveCount;
        // Hide the begin wave UI elements

        StartCoroutine("runWave");
    }

    IEnumerator runWave()
    {
        // Wave strength will be the counter for how many enemies are spawned or their total strength
        while(waveStrengths[currentWave] > 0)
        {
            // Retrieves the list of enemy prefabs from the enemy type for the level
            List<GameObject> enemyPrefabs = levelEnemyType.getEnemyPrefabs();


            // Picks a random enemy from the available prefabs
            //This line is what made some levels not work as enemyPrefabs's count is higher than the highest index possible
            //and because of the way it was calculated the enemyIndexUpper before it would lead to errors
            //It has been modified in order to prevent further errors
            int enemyIndex = Mathf.FloorToInt(Random.Range(enemyIndexLower, Mathf.Min(enemyIndexUpper, enemyPrefabs.Count - 1)));

            // Instantiates a new enemy
            GameObject spawnedEnemy = GameObject.Instantiate(enemyPrefabs[enemyIndex]);
            
            // Moves the enemy to the first waypoint along the path
            spawnedEnemy.transform.position = pathWaypoints[0].position;
            // Adds the enemy to the list of active enemies in the game
            activeEnemies.Add(spawnedEnemy);

            Enemy enemyData = spawnedEnemy.GetComponent<Enemy>();

            // Removes the enemy strength from the current wave strength
            waveStrengths[currentWave] -= enemyData.getStrength();

            // Waits for 1 second before continuing the loop
            yield return new WaitForSeconds(baseEnemySpawningDelay - currentWave);
        }
        
        // After all the enemies have been spawned, continue waiting until there are no longer any enemies on the screen
        while (activeEnemies.Count > 0)
            yield return new WaitForSeconds(0.1f);


        
        waveIsActive = false;
        nextWaveLabel.SetActive(true);
    }

    private bool stillAlive()
    {
        if (health <= 0)
        {
            // End level and trigger the lost game UI screen
            return false;
        }
        return true;
    }

    public void IncreaseSpeed()
    {
        speedMultiplier = 2;
        Tower[] towers = GameObject.FindObjectsOfType<Tower>();
        foreach(Tower t in towers)
        {
            t.IncreaseSpeed();
        }
    }

    public void DecreaseSpeed()
    {
        speedMultiplier = 1;
        Tower[] towers = GameObject.FindObjectsOfType<Tower>();
        foreach (Tower t in towers)
        {
            t.DecreaseSpeed();
        }
    }

    public bool isWaveActive()
    {
        return waveIsActive;
    }
}

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
        waveStrengths = new List<int>();
        activeEnemies = new List<GameObject>();

        for(int i = 0; i < waveCount; i++)
        {
            waveStrengths.Add(levelDifficulty / waveCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!waveIsActive && Input.GetKeyDown(KeyCode.Space))
        {
            nextWave();
        }

        if (waveIsActive)
        {
            pathStep();
        }
    }

    private void pathStep()
    {
        for(int i = 0; i < activeEnemies.Count; i++)
        {
            Enemy enemyData = activeEnemies[i].GetComponent<Enemy>();
            int waypointIndex = enemyData.getWaypointIndex();
            int nextWaypointIndex = waypointIndex + 1;

            Vector2 nextPosition = pathWaypoints[nextWaypointIndex].position;
            Vector2 newEnemyPosition = Vector2.MoveTowards(enemyData.gameObject.transform.position, nextPosition, enemyData.getSpeed() * Time.deltaTime);

            enemyData.move(newEnemyPosition);

            float distance = Vector2.Distance(newEnemyPosition, nextPosition);
            if (Mathf.Abs(distance) < .01)
            {
                enemyData.nextWaypointIndex();
            }

            if(enemyData.getWaypointIndex() == pathWaypoints.Count - 1)
            {
                enemyData.damage(99999);
                activeEnemies.Remove(enemyData.gameObject);
                i--;
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
        while(waveStrengths[currentWave] > 0)
        {
            // Spawn the next enemy
            List<GameObject> enemyPrefabs = levelEnemyType.getEnemyPrefabs();

            GameObject spawnedEnemy = GameObject.Instantiate(enemyPrefabs[0]);
            spawnedEnemy.transform.position = pathWaypoints[0].position;
            activeEnemies.Add(spawnedEnemy);

            waveStrengths[currentWave] -= 10;

            yield return new WaitForSeconds(1);
        }

        while (activeEnemies.Count > 0)
            yield return new WaitForSeconds(0.1f);

        currentWave += 1;
        waveIsActive = false;
    }
}

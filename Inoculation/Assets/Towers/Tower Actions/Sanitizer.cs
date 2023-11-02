using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sanitizer : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;

    private LevelData levelData;

    // Start is called before the first frame update
    void Start()
    {
        GameObject levelDataObj = GameObject.Find("LevelData");
        levelData = levelDataObj.GetComponent<LevelData>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the wave is no longer active, destroy all sanitizer splashes
        if (levelData.isWaveActive() == false)
            GameObject.Destroy(this);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If an enemy hits the sanitizer, damage the enemy
        if (collision.gameObject.tag == "enemy")
        {
            Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();
            hitEnemy.damage(damage);
            GameObject.Destroy(this.gameObject);
        }
    }
}

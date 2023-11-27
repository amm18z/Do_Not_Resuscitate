using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSanitize : Tower.TowerAction
{
    private bool active = false;

    [SerializeField]
    private int sanitizeDelay = 2;

    [SerializeField]
    private GameObject sanitizer;

    private LevelData levelData;

    private float animationDelay = 0.0f;

    public Animator animator;

    public override void performAction(GameObject enemy)
    {
        // Unnecessary since the tower will be constantly active throughout the wave
        return;
    }

    public override void SetAnimationDelay(float delay)
    {
        animationDelay = delay;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve reference to the level data class for wave status checking
        GameObject levelDataObj = GameObject.Find("LevelData");
        levelData = levelDataObj.GetComponent<LevelData>();
        animator.GetComponent<Animator>();
        animator.SetBool("isShoot", false);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isShoot", false);
        // If tower is currently not active and wave has been initiated, activate tower
        if (!active && levelData.isWaveActive())
        {
            active = true;
            StartCoroutine("sanitizePath");
            animator.SetBool("isShoot", true);
        }
        // If tower is active, repeatedly check to see if the wave is still active
        else
        {
            // Once the wave is no longer active, tower will be automatically deactivated
            active = levelData.isWaveActive();
            animator.SetBool("isShoot", active);
        }
    }

    IEnumerator sanitizePath()
    {
        // Repeatedly spawn sanitizer while wave is active
        while (active)
        {
            // Spawn sanitizer on the path
            GameObject splash = GameObject.Instantiate(sanitizer, this.transform);

            float angle = Random.Range(0, 360);
            float length = Random.Range(1, 3);
            
            Vector2 offset = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            offset *= length;

            Vector2 currPosition = this.transform.position;
            Vector2 newPosition = offset + currPosition;

            splash.transform.position = newPosition;

            // Delay
            yield return new WaitForSeconds(sanitizeDelay);
        }
        
    }
}

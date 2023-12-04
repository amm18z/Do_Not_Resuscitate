using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : Tower.TowerAction
{
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Transform shootPosition;

    [SerializeField]
    private float projectileSpeed = 4;

    [SerializeField]
    private float reloadDelay = 1;

    private bool reloading = false;
    private bool waitingForShot = false;

    private float animationDelay = 0.0f;

    public Animator animator;

    private LevelData level;
    private int speedMultiplier = 1;

    private void Start()
    {
        level = GameObject.Find("LevelData").GetComponent<LevelData>();
        //speedMultiplier = level.getSpeedMultiplier();

        Tower temp = this.GetComponent<Tower>();
        try
        {
            animator = temp.animator;
            animator.SetBool("isShoot", false);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        
    }

    private void Update()
    {
        //speedMultiplier = level.getSpeedMultiplier();
    }

    public override void SetAnimationDelay(float delay)
    {
        animationDelay = delay;
    }

    public override void performAction(GameObject enemy)
    {
        if (reloading || waitingForShot)
        {
            try
            {
                animator.SetBool("isShoot", false);
            }catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
            
            return;
        }


        try
        {
            animator.SetBool("isShoot", true);
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
        
        StartCoroutine(delayShooting(enemy));
        
    }

    IEnumerator reload()
    {
        waitingForShot = false;
        yield return new WaitForSeconds(reloadDelay);
        reloading = false;
    }

    IEnumerator delayShooting(GameObject enemy)
    {
        waitingForShot = true;
        reloading = true;
        Debug.Log("Delaying action for animation");
        yield return new WaitForSeconds(animationDelay);
        
        if (enemy != null)
        {
            // Shoot projectile towards the enemy
            GameObject shotProjectile = GameObject.Instantiate(projectile, shootPosition);

            Tower mainTower = this.GetComponent<Tower>();

            Vector3 initialPosition = shotProjectile.transform.position;
            Vector3 shootDirection = enemy.transform.position - shootPosition.position;
            shootDirection = shootDirection.normalized;
            shootDirection *= (projectileSpeed * speedMultiplier);

            shotProjectile.transform.parent = null;

            shotProjectile.transform.position = initialPosition;
            shotProjectile.GetComponent<Rigidbody2D>().velocity = shootDirection;
        }

        StartCoroutine("reload");
    }

    public void increaseSpeedCrossBow()
    {
        projectileSpeed = 24;
        reloadDelay = 0.25f;
    }
    public void decreaseSpeedCrossBow()
    {
        projectileSpeed = 12;
        reloadDelay = .5f;
    }

    public void increaseSpeedSoda()
    {
        projectileSpeed = 24;
        reloadDelay = 0.25f;
    }
    public void decreaseSpeedSoda()
    {
        projectileSpeed = 12;
        reloadDelay = 0.5f;
    }

    public override void IncreaseSpeed()
    {
        increaseSpeedCrossBow();
    }

    public override void DecreaseSpeed()
    {
        decreaseSpeedCrossBow();
    }
}

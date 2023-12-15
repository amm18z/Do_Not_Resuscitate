using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : Tower.TowerAction
{
    [SerializeField]
    private Tower tower;
    
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

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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

            if (projectile.name == "projectile")
            {
                audioManager.PlaySFX(audioManager.CrossbowShot);
            }
            else if(projectile.name == "sodacan")
            {
                audioManager.PlaySFX(audioManager.SodaLaunch);
            }
            else if(projectile.name == "Bandage Bullet")
            {
                audioManager.PlaySFX(audioManager.BandaidShot);
            }

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
        //projectileSpeed = 5;
        //reloadDelay = 0.45f;
        //float delay = 0.7f;
        //tower.modifyShootingDelay(delay);

        projectileSpeed *= 2;
        reloadDelay /= 2;
        float delay = 2f;
        tower.divDecreaseShootingDelay(delay);
    }
    public void decreaseSpeedCrossBow()
    {
        //projectileSpeed = 10;
        //reloadDelay = .9f;
        //float delay = 0.35f;
        //tower.modifyShootingDelay(delay);

        projectileSpeed /= 2;
        reloadDelay *= 2;
        float delay = 2f;
        tower.multIncreaseShootingDelay(delay);
    }

    public void increaseSpeedSoda()
    {
        //projectileSpeed = 12;
        //reloadDelay = 6;
        //float delay = 0.625f;
        //tower.modifyShootingDelay(delay);

        projectileSpeed *= 2;
        reloadDelay /= 2;
        float delay = 2f;
        tower.divDecreaseShootingDelay(delay);
    }
    public void decreaseSpeedSoda()
    {
        //projectileSpeed = 12;
        //reloadDelay = 12;
        //float delay = 1.25f; // set delay
        //tower.modifyShootingDelay(delay); // change shooting delay

        projectileSpeed /= 2;
        reloadDelay *= 2;
        float delay = 2f;
        tower.multIncreaseShootingDelay(delay);
    }
    public void increaseGunSpeed()
    {
        //projectileSpeed = 64; // >
        //reloadDelay = 0.25f;
        // no delay changed bc its 0

        projectileSpeed *= 2;
        reloadDelay /= 2;
        // increase gun delay to .001 so that this code will work
        float delay = 2f;
        tower.divDecreaseShootingDelay(delay);
    }
    public void decreaseGunSpeed()
    {
        //projectileSpeed = 32; // <
        //reloadDelay = 0.5f;
        // no delay changed bc its 0

        projectileSpeed /= 2;
        reloadDelay *= 2;
        // increased gun delay to .001 so that this code will work
        float delay = 2f;
        tower.multIncreaseShootingDelay(delay);
    }

    public override void IncreaseSpeed()
    {
        /* old:
        
        // call all - one is left out because it doesnt use this script 
        increaseSpeedCrossBow();
        increaseSpeedSoda();
        increaseGunSpeed();

        */

        // new:
        projectileSpeed *= 2;
        reloadDelay /= 2;
        float delay = 2f;
        tower.divDecreaseShootingDelay(delay);

    }

    public override void DecreaseSpeed()
    {
        /* old:

        // call all - one is left out because it doesnt use this script <_>
        decreaseSpeedCrossBow();
        decreaseSpeedSoda();
        decreaseGunSpeed();

        */

        // new:
        projectileSpeed /= 2;
        reloadDelay *= 2;
        float delay = 2f;
        tower.multIncreaseShootingDelay(delay);
    }
}

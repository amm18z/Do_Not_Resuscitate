using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float maxAge = 5f;
    
    private float age;
    private int damage;
    // Start is called before the first frame update
    void Start()
    {
        age = 0;
        damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;

        if(age >= maxAge)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Projectile hit something");
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("Projectile hit enemy");
            Enemy hitEnemy = collision.gameObject.GetComponent<Enemy>();
            hitEnemy.damage(damage);
            GameObject.Destroy(this.gameObject);
        }

    }
}

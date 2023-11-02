using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float maxAge = 5f;
    
    private float age;
    private int damage;
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        age = 0;
        damage = 20;
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;

        Vector2 moveDirection = rigidbody.velocity;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        angle += 90;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (age >= maxAge)
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

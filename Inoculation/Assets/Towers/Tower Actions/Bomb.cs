using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private float maxAge = 3f;
    private float age;

    [SerializeField]
    private int damage = 5;
    
    private Rigidbody2D rigidbody;
    private CircleCollider2D collider;

    [SerializeField]
    private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        age = 0;
        rigidbody = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<CircleCollider2D>();
        collider.enabled = false;
        explosion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;

        Vector2 moveDirection = rigidbody.velocity;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        angle += 90;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (age >= maxAge && !collider.enabled)
        {
            // Enable bomb collider
            collider.enabled = true;
            explosion.SetActive(true);
        }
        else if (age >= maxAge + 0.5f)
        {
            // Allow half a second of enemies to get hit
            GameObject.Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            Enemy temp = collision.gameObject.GetComponent<Enemy>();
            temp.damage(damage);
        }
    }
}

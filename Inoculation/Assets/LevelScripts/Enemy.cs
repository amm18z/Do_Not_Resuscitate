using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int speed = 5;
    [SerializeField]
    private int health = 100;
    [SerializeField]
    private int strength = 10;

    private int currentWaypointIndex = 0;

    public int getSpeed() { return speed / 10; }
    public int getHealth() { return health; }
    public int getStrength() { return strength; }
    public void damage(int damagetaken)
    {
        health -= damagetaken;
    }

    public void destroyEnemy()
    {
        GameObject.Destroy(this.gameObject);
    }

    public int getWaypointIndex() { return currentWaypointIndex; }
    public void nextWaypointIndex() { currentWaypointIndex += 1; }
    public void move(Vector2 newPosition)
    {
        this.transform.position = newPosition;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int health = 50;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Remaining: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Replace with your death effect or logic
        Destroy(gameObject);
    }
}

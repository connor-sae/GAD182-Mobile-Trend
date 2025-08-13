using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int health = 50;
    public float moveSpeed = 3f;
    public int damage = 10;

    private Transform player;

    void Start()
    {
        //find the player by finding an object with the player tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure the Player GameObject is tagged 'Player'");
        }
    }

    void Update()
    {
        if (player != null)
        {
            //move toward the player every frame
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            //enemy faces the player 
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
    }

    //check if damage taken is less than or equal to the enemy's health and if the enemy's health is less than 0, the enemy dies
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
        Destroy(gameObject);
    }

    // Deal damage when colliding with the player
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}

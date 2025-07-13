using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifetime = 5f;

    private void Start()
    {
        // Destroy the projectile after a few seconds to avoid clutter
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the projectile forward every frame
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        // Check if the object has an Enemy component
        Enemies enemy = other.GetComponent<Enemies>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Destroy the projectile on impact
        Destroy(gameObject);
    }
}

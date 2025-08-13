using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifetime = 5f;
    public GameObject explosionVisualPrefab;
    private int hitCount = 0;

    private void Start()
    {
        var upgrades = PlayerUpgrades.Instance;

        //increase speed with Faster upgrade level
        speed *= 1f + 0.5f * upgrades.fasterLevel;

        //destroy projectile after a while
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        //move the projectile forward every frame
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        //check if the object has an Enemy component
        Enemies enemy = other.GetComponent<Enemies>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            // Exploding upgrade
            if (PlayerUpgrades.Instance.explodingLevel > 0)
                Explode();

            // Piercing logic
            if (hitCount >= PlayerUpgrades.Instance.piercingLevel)
                Destroy(gameObject);
            else
                hitCount++;

            return;
        }

        // Hit upgrade object
        Upgrades upgrade = other.GetComponent<Upgrades>();
        if (upgrade != null)
        {
            upgrade.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    
    void Explode()
    {
        float radius = 3f + PlayerUpgrades.Instance.explodingLevel * 1.5f;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            Enemies enemy = hit.GetComponent<Enemies>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }

        //visualize the radius by using the explosion radius' stat and destroy the game object after 0.3 seconds
        if (explosionVisualPrefab != null)
        {
            GameObject visual = Instantiate(explosionVisualPrefab, transform.position, Quaternion.identity);
            visual.transform.localScale = Vector3.one * radius * 2f; 

            Destroy(visual, 0.3f); 
        }

        Debug.Log("Explosion triggered. Radius: " + radius);
    }
}

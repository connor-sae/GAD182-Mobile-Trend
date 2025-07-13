using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject projectilePrefab;  // Assign your projectile prefab in Inspector
    public Transform firePoint;          // Where the projectile spawns (e.g., a point in front of player)

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Left mouse or Ctrl
        {
            Debug.Log("shooting");
            Shoot();
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("missing projectilePrefab or firePoint");
            return;
        }

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("projectile instantiated");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject projectilePrefab;  
    public Transform firePoint;          

    void Update()
    {
        //if player is clicling left click shoot projectile
        if (Input.GetButtonDown("Fire1")) 
        {
            Debug.Log("shooting");
            Shoot();
        }
    }

    //when called instantiate the projectile prefab
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

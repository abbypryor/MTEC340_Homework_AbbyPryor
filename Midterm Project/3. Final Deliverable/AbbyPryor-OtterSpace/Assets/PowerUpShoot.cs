using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public float projectileSpeed = 20f;
    public float fireRate = 0.2f;
    public float shootingDuration = 3f;

    private bool isShootingEnabled = false;
    private float shootingTimer = 0f;

    void Update()
    {
        if (isShootingEnabled)
        {
            if (shootingTimer > shootingDuration)
            {
                isShootingEnabled = false;
                shootingTimer = 0f;
            }
            else
            {
                shootingTimer += Time.deltaTime;
                Shoot(projectileSpawn.right);
                Shoot(-projectileSpawn.right);
                Shoot(projectileSpawn.up);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            isShootingEnabled = true;
        }
    }
    void Shoot(Vector2 direction)
    {
        float angleStep = 120f;
        for (int i = 0; i < 3; i++)
        {
            Quaternion rotation = Quaternion.AngleAxis(angleStep * i, Vector3.forward);
            Vector2 rotatedDirection = rotation * direction;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = rotatedDirection * projectileSpeed;
        }
    }






}

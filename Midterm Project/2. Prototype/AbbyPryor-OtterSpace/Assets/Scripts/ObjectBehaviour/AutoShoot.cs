using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoShoot : MonoBehaviour
{
    public AudioClip shootClip;
    public float fireRate = 0.4f; // Slower rate of fire
    public float powerUpDuration = 5.0f;
    public float powerUpFireRateMultiplier = 2.0f;

    private bool isShooting = false;
    private AudioSource audioSource;
    private Coroutine shootingCoroutine;
    private bool hasPowerUp = false;
    private float originalFireRate;
    private float powerUpEndTime;
    private float nextShotTime = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalFireRate = fireRate;
    }

 
    void StartShooting()
    {
        if (isShooting)
        {
            return;
        }

        isShooting = true;
        shootingCoroutine = StartCoroutine(PlayShootingSound());
    }

    void StopShooting()
    {
        if (!isShooting)
        {
            return;
        }

        isShooting = false;

        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null;
        }
    }
    IEnumerator PlayShootingSound()
    {
        float fireDelay = fireRate / 2f;
        float audioDelay = (fireRate - fireDelay) / 2f;

        while (true)
        {
            foreach (Weapon w in GetComponentsInChildren<Weapon>())
            {
                w.Shoot();
            }

            if (audioSource != null && shootClip != null)
            {
                audioSource.pitch = hasPowerUp ? powerUpFireRateMultiplier : 1f;
                audioSource.PlayOneShot(shootClip);
                yield return new WaitForSecondsRealtime(audioDelay);
                audioSource.pitch = 1f;
            }

            yield return new WaitForSecondsRealtime(fireDelay * (hasPowerUp ? 0.5f : 1f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            fireRate /= powerUpFireRateMultiplier;
            hasPowerUp = true;
            powerUpEndTime = Time.time + powerUpDuration;
            audioSource.pitch = powerUpFireRateMultiplier;
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartShooting();
        }
        else if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            StopShooting();
        }

        if (isShooting && Time.time >= nextShotTime)
        {
            foreach (Weapon w in GetComponentsInChildren<Weapon>())
            {
                w.Shoot();
            }
            nextShotTime = Time.time + fireRate;
        }

        if (hasPowerUp && Time.time >= powerUpEndTime)
        {
            fireRate = originalFireRate;
            hasPowerUp = false;
            audioSource.pitch = 2.0f;
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public float delay;
    public ObjectSpawnRate[] enemies;

    private List<GameObject> enemyList;


    private void Awake()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.enabled = true;

        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            if (GameManager.GetInstance().isPlaying)
            {
                Spawn();
                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return null;
            }
        }
    }

    private GameObject getEnemy()
    {
        int limit = 0;

        foreach (ObjectSpawnRate osr in enemies)
        {
            limit += osr.rate;
        }

        int random = Random.Range(0, limit);

        foreach (ObjectSpawnRate osr in enemies)
        {
            if (random < osr.rate)
            {
                return osr.prefab;
            }
            else
            {
                random -= osr.rate;
            }
        }
        return null;
    }

    public void Spawn()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Random.Range(-7.5f, 7.5f);

        GameObject newEnemy = Instantiate(getEnemy(), newPosition, transform.rotation);
        enemyList.Add(newEnemy);
    }

    public void clearEnemies()
    {
        StartCoroutine(ClearEnemiesWithAudio());
    }

    private IEnumerator ClearEnemiesWithAudio()
    {
        foreach (GameObject go in enemyList)
        {
            AudioSource audioSource = go.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.clip != null)
            {
                // Play the audio clip before destroying the object
                audioSource.Play();
                // Wait for the audio clip to finish playing before destroying the object
                yield return new WaitForSeconds(audioSource.clip.length);
            }
            Destroy(go);
        }
        enemyList.Clear();
    }



    public UnityEvent onHit;
    public UnityEvent onCollision;  // Added event for collision

    public AudioClip hitSound;
    private AudioSource audioSource;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play hit sound
            audioSource.PlayOneShot(hitSound);

            // Invoke hit event
            onHit.Invoke();

            // Destroy objects
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (enemyList.Contains(gameObject))
            {
                // Trigger onCollision event
                onCollision.Invoke();

                // Remove this enemy from the list
                enemyList.Remove(gameObject);
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(CircleCollider2D))]
public class TriggerEvent : MonoBehaviour
{
    public string targetTag;

    public UnityEvent OnTrigger;
    public UnityEvent<GameObject> OnTriggerWithGameobject;

    public AudioSource audioSource; // Reference to the audio source component

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Target tag: " + targetTag + " collision tag: " + collision.tag);
        if (collision.tag == targetTag)
        {
            if (audioSource != null && audioSource.enabled) // Check if the audio source component is not null and is enabled
            {
                audioSource.Play(); // Play the audio clip
            }

            OnTrigger?.Invoke();
            OnTriggerWithGameobject?.Invoke(collision.gameObject);
        }

    }

    private void OnDestroy()
    {
        // Unregister event listeners
        OnTrigger.RemoveAllListeners();
        OnTriggerWithGameobject.RemoveAllListeners();
    }
}

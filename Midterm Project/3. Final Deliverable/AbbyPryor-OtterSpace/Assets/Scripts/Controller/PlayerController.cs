using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Moveable))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    public InputHandler inputHandler;
    public AudioClip coinSound;

    private Moveable moveable;
    private AudioSource audioSource;

    private void Awake()
    {
        moveable = GetComponent<Moveable>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnSetDirection(Vector2 direction)
    {
        moveable.setDirection(direction);
    }

    private void OnEnable()
    {
        inputHandler.OnMoveAction += OnSetDirection;
    }

    private void OnDisable()
    {
        inputHandler.OnMoveAction -= OnSetDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Coin"))
            {
                AudioSource coinAudioSource = collision.gameObject.GetComponent<AudioSource>();
                coinAudioSource.Play();
                Destroy(collision.gameObject);
            }
        }
    

}
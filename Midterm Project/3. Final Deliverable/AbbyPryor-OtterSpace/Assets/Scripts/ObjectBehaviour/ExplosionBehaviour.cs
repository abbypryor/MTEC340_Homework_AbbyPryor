using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    private Animator animator;
    private PoolObject poolObject;
    private AudioSource audioSource;
    public AudioClip explosionClip;

    public float delay = 1f;  // Increase the delay value
    private bool isDone = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        poolObject = GetComponent<PoolObject>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        // Increase the priority of the audio source
        audioSource.priority = 128;
    }

    void Update()
    {
        if (poolObject.isActive())
        {
            if (animationIsDone())
            {
                if (!isDone)
                {
                    audioSource.PlayOneShot(explosionClip);
                    isDone = true;
                }

                delay -= Time.deltaTime;
                if (delay <= 0)
                {
                    poolObject.deactivate();
                }
            }
        }
    }

    private bool animationIsDone()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            return true;
        }
        return false;
    }
}

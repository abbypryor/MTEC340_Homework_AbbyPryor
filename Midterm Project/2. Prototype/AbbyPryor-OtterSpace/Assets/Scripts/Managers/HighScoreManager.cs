using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            score++; // Increase the score
            if (score > highScore)
            {
                highScore = score; // Update the high score if the current score is higher
            }
            Destroy(gameObject); // Destroy the coin object
        }
    }

    void OnDestroy()
    {
        // Save the high score
        PlayerPrefs.SetInt("HighScore", highScore);
    }


}

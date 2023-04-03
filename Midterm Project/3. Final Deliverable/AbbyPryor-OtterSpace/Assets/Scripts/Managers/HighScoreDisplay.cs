using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;

    void Start()
    {
        // Retrieve the high score from PlayerPrefs, or use 0 if it hasn't been set yet
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Display the high score
        scoreText.text = "High Score: " + highScore.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            // Increase the score
            score++;

            // Update the high score if the current score is higher
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            // Destroy the coin object
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        // Update the score display
        scoreText.text = "Score: " + score.ToString();
    }
}

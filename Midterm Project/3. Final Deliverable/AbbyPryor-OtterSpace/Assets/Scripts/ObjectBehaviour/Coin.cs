using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public ScriptableInteger coin;
   

    public void OnGain()
    {
        coin.value += 1;

    }

    private void UpdateHighScoreUI()
    {
    }
}

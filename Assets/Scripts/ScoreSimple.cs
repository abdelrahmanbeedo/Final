using UnityEngine;

public class ScoreSimple : MonoBehaviour
{
    public static int score;

    public static void Add(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
}

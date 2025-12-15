using UnityEngine;

[CreateAssetMenu(menuName = "SimpleRunner/Obstacle")]
public class ObstacleData : ScriptableObject
{
    public GameObject prefab;
    public float speed = 5f;
    public float nearMissDistance = 2f;
    public int scoreValue = 100;
}

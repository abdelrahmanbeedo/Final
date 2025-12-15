using UnityEngine;

public class SimpleObstacleSpawner : MonoBehaviour
{
    public ObstacleData[] obstacles;
    public float spawnDistanceAhead = 20f;
    public float spawnInterval = 0.6f;
    public float roadWidth = 3f; // total road width
    public float roadY = 0f; // road surface Y position

    Transform player;
    float timer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        ObstacleData data = obstacles[Random.Range(0, obstacles.Length)];

        if (data == null || data.prefab == null)
        {
            Debug.LogError("ObstacleData or prefab is NULL");
            return;
        }

        Vector3 pos = player.position + Vector3.forward * spawnDistanceAhead;

        // Keep obstacle inside road boundaries
        float margin = 0.3f; // prevent spawning too close to edges
        pos.x = Random.Range(-roadWidth / 2 + margin, roadWidth / 2 - margin);

        // Align Y with road
        pos.y = roadY;

        GameObject obj = Instantiate(data.prefab, pos, Quaternion.identity);

        if (obj == null)
        {
            Debug.LogError("Instantiated object is NULL");
            return;
        }

        ObstacleSimple obstacle = obj.GetComponent<ObstacleSimple>();

        if (obstacle == null)
        {
            Debug.LogError("ObstacleSimple component NOT found on prefab");
            return;
        }

        obstacle.Init(data);
    }
}

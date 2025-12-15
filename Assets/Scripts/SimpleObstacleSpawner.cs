using UnityEngine;

public class SimpleObstacleSpawner : MonoBehaviour
{
    public ObstacleData[] obstacles;

    public float spawnDistanceAhead = 40f;
    public float spawnInterval = 1.5f;

    public float roadWidth = 6f; // total usable road width
    public LayerMask roadLayer;  // set to "Road" in Inspector

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
        if (obstacles.Length == 0) return;

        ObstacleData data = obstacles[Random.Range(0, obstacles.Length)];
        if (data == null || data.prefab == null) return;

        // 1️⃣ Z position ahead of player
        Vector3 spawnPos = player.position + Vector3.forward * spawnDistanceAhead;

        // 2️⃣ Restrict X to road only (NO buildings)
        float margin = 0.6f;
        spawnPos.x = Random.Range(
            -roadWidth / 2f + margin,
             roadWidth / 2f - margin
        );

        // 3️⃣ Raycast DOWN to snap to road (perfect Y)
        spawnPos.y = 50f; // start high above road

        if (Physics.Raycast(spawnPos, Vector3.down, out RaycastHit hit, 100f, roadLayer))
        {
            spawnPos.y = hit.point.y;
        }
        else
        {
            return; // no road found → don’t spawn
        }

        GameObject obj = Instantiate(data.prefab, spawnPos, Quaternion.identity);

        ObstacleSimple obstacle = obj.GetComponent<ObstacleSimple>();
        if (obstacle != null)
        {
            obstacle.Init(data);
        }
    }
}

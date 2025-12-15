using UnityEngine;

public class SimpleObstacleSpawner : MonoBehaviour
{
    public ObstacleData[] obstacles;
    public float spawnDistanceAhead = 40f;
    public float spawnInterval = 2f;

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

    if (data == null)
    {
        Debug.LogError("ObstacleData is NULL");
        return;
    }

    if (data.prefab == null)
    {
        Debug.LogError("Prefab inside ObstacleData is NULL");
        return;
    }

    Vector3 pos = player.position + Vector3.forward * spawnDistanceAhead;
    pos.x = Random.Range(-3f, 3f);

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

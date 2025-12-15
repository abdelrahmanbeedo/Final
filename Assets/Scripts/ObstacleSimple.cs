using UnityEngine;

public class ObstacleSimple : MonoBehaviour
{
    public ObstacleData data;

    Transform player;
    bool scored = false;
    bool initialized = false;

    public void Init(ObstacleData obstacleData)
    {
        data = obstacleData;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialized = true;
    }

    void Update()
    {
        if (!initialized)
            return;

        transform.Translate(Vector3.forward * data.speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, player.position);

        if (!scored && distance < data.nearMissDistance)
        {
            ScoreSimple.Add(data.scoreValue);
            scored = true;
        }
    }
}

using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public int numberOfObstacles = 20;
    public float spawnRadius = 100f;

   
    public NavMeshSurface landNavMesh;

    void Start()
    {
        SpawnObstaclesOnNavMesh();
    }

    void SpawnObstaclesOnNavMesh()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 position = GetRandomNavMeshPosition(landNavMesh);
            if (position != Vector3.zero)
            {
                GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
                Instantiate(prefab, position, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            }
        }
    }


    Vector3 GetRandomNavMeshPosition(NavMeshSurface surface)
    {
        int landMask = 1 << NavMesh.GetAreaFromName("Land");

        for (int attempt = 0; attempt < 10; attempt++)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0f,
                Random.Range(-spawnRadius, spawnRadius)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 5f, landMask))
            {
                return hit.position;
            }
        }

        return Vector3.zero;
    }
}


using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.AI;

public class BoatSpawner : MonoBehaviour
{
    [Header("Boat Settings")]
    public GameObject boatPrefab;
    public int numberOfBoats = 5;
    public float spawnRadius = 100f;

    void Start()
    {
        SpawnBoats();
    }

    void SpawnBoats()
    {
        int waterMask = 1 << NavMesh.GetAreaFromName("Water");

        for (int i = 0; i < numberOfBoats; i++)
        {
            for (int attempt = 0; attempt < 10; attempt++)
            {
                Vector3 randomPoint = new Vector3(
                    Random.Range(-spawnRadius, spawnRadius),
                    0f,
                    Random.Range(-spawnRadius, spawnRadius)
                );

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 5f, waterMask))
                {
                    GameObject boat = Instantiate(boatPrefab, hit.position, Quaternion.identity);
                    break;
                }
            }
        }
    }
}



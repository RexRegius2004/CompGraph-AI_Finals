using UnityEngine;
using UnityEngine.AI;

public class SpawnerController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject snailPrefab;

    public Transform spawnArea;
    public float spawnRange = 50f;
    void Start()
    {
        Vector3 playerSpawn = GetRandomNavMeshPos(spawnArea.position, spawnRange);
        Vector3 snailSpawn = GetRandomNavMeshPos(spawnArea.position, spawnRange);


        GameObject player = Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        GameObject snail = Instantiate(snailPrefab, snailSpawn, Quaternion.identity);

   
        snail.GetComponent<SnailNavigation>().player = player.transform;
    }

    Vector3 GetRandomNavMeshPos(Vector3 center, float range = 50f)
    {
        int landAreaMask = 1 << NavMesh.GetAreaFromName("Land");

        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPos = center + Random.insideUnitSphere * range;
            randomPos.y = center.y;

            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 5f, landAreaMask))
            {
                return hit.position;
            }
        }

        Debug.LogError("Could not find valid LAND NavMesh position!");
        return center;
    }
}

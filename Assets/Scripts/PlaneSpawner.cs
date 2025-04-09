using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public GameObject planePrefab;
    public int numberOfPlanes = 3;
    public Vector2 spawnAreaSize = new Vector2(200f, 200f);
    public float planeHeight = 50f;

    void Start()
    {
        SpawnPlanes();
    }

    void SpawnPlanes()
    {
        for (int i = 0; i < numberOfPlanes; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                planeHeight,
                Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f)
            );

            GameObject plane = Instantiate(planePrefab, spawnPos, Quaternion.identity);

            PlaneBehavior behavior = plane.GetComponent<PlaneBehavior>();
            behavior.Initialize(spawnAreaSize.x / 2f);
        }
    }
}






using System.Xml.Serialization;
using Unity.AI.Navigation;
using UnityEngine;
public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private NavMeshSurface meshSurface;

    [SerializeField] private int numberOfSpawn;
    [SerializeField] private Vector2 spawnPosition;
    [SerializeField] private Vector2 spawnSize;
    [SerializeField] private LayerMask floorMask;
    [SerializeField] private GameObject obstaclePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < numberOfSpawn; )
        {
            float xPos = Random.Range(spawnPosition.x, spawnPosition.y);
            float zPos = Random.Range(spawnPosition.x, spawnPosition.y);

            Vector3 newPos = new Vector3(xPos, transform.position.y, zPos);

            float xsize = Random.Range(spawnSize.x, spawnSize.y);
            float zsize = Random.Range(spawnSize.x, spawnSize.y);

            Vector3 newSize = new Vector3(xsize, 1, zsize);

            Ray ray = new Ray(newPos, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, floorMask))
            {
                {
                    Collider[] colliders = Physics.OverlapSphere(hit.point, SphereRadius(xsize, zsize));
                    if (colliders.Length == 1)
                    {
                        Vector3 newObsPos = new Vector3(hit.point.x, 0.5f, hit.point.z);
                        GameObject newObstacle = Instantiate(obstaclePrefab, newObsPos, Quaternion.identity);
                        newObstacle.transform.localScale = new Vector3 (xsize, 1, zsize);
                        i++;
                    }
                }
            }
        }
        meshSurface.BuildNavMesh();
    }
    private void Update()
    {

    }

    private float SphereRadius(float xsize, float zsize)
    {
        if (xsize > zsize)
        {
            return xsize;
        }
        else
        {
            return zsize;
        }
    }
}

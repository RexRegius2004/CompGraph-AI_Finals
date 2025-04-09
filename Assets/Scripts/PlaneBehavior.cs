using UnityEngine;

public class PlaneBehavior : MonoBehaviour
{
    public float speed = 5f;
    private float boundaryX;
    private Vector3 direction;

    public void Initialize(float mapHalfWidth)
    {
        boundaryX = mapHalfWidth;
        direction = Random.value > 0.5f ? Vector3.right : Vector3.left;

        transform.rotation = Quaternion.Euler(0, direction == Vector3.right ? 90f : -90f, 0);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (transform.position.x >= boundaryX || transform.position.x <= -boundaryX)
        {
            direction = -direction;
            transform.rotation = Quaternion.Euler(0, direction == Vector3.right ? 90f : -90f, 0);
        }
    }
}









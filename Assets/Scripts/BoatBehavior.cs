using UnityEngine;
using UnityEngine.AI;

public class BoatAI : MonoBehaviour
{
    public float roamRadius = 30f;
    public float checkInterval = 3f;

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetRandomDestination();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                SetRandomDestination();
            }
        }

        if (timer >= checkInterval)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.1f)
            {
                SetRandomDestination();
            }
            timer = 0f;
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        randomDirection.y = 0f;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
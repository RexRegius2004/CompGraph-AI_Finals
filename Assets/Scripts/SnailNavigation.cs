using UnityEngine;
using UnityEngine.AI;

public class SnailNavigation : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float updateRate = 0.2f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] public GameManager gameManager;

    private void OnValidate()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        gameManager = FindFirstObjectByType<GameManager>();

        InvokeRepeating(nameof(UpdateDestination), 0f, updateRate);
    }

    private void UpdateDestination()
    {
        if (player && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }
    }

    private void Update()
    {
        if (agent.velocity.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.GameOver();
        }
    }

}




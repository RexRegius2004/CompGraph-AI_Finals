using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AIRootMotionController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField] private float maxTargetDistance = 100f;

    private bool isTraversingLink = false;

    private void OnValidate()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (agent.isOnNavMesh && !isTraversingLink)
        {
            if (agent.hasPath && agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                Vector3 dir = (agent.steeringTarget - transform.position).normalized;
                Vector3 aniDir = transform.InverseTransformDirection(dir);

                bool isFacingMoveDirection = Vector3.Dot(dir, transform.forward) > 0.5f;

                animator.SetFloat("Horizontal", isFacingMoveDirection ? aniDir.x : 0f, 0.5f, Time.deltaTime);
                animator.SetFloat("Vertical", isFacingMoveDirection ? aniDir.z : 0f, 0.5f, Time.deltaTime);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), 100f * Time.deltaTime);

                if (Vector3.Distance(transform.position, agent.destination) < agent.stoppingDistance + 0.1f)
                {
                    agent.ResetPath();
                }
            }
            else
            {
                animator.SetFloat("Horizontal", 0f, 0.25f, Time.deltaTime);
                animator.SetFloat("Vertical", 0f, 0.25f, Time.deltaTime);

                if (agent.hasPath && (agent.isPathStale || agent.pathStatus != NavMeshPathStatus.PathComplete))
                {
                    agent.ResetPath();
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, maxTargetDistance))
                {
                    if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 2f, NavMesh.AllAreas))
                    {
                        agent.destination = navHit.position;
                    }
                }
            }


            if (agent.isOnOffMeshLink && !isTraversingLink)
            {
                StartCoroutine(TraverseLink());
            }
        }
    }

    private IEnumerator TraverseLink()
    {
        isTraversingLink = true;

        OffMeshLinkData linkData = agent.currentOffMeshLinkData;
        Vector3 startPos = transform.position;
        Vector3 endPos = linkData.endPos;

        agent.isStopped = true;

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        agent.CompleteOffMeshLink();

        agent.ResetPath();

        agent.isStopped = false;
        isTraversingLink = false;
    }


    private void OnDrawGizmos()
    {
        if (agent != null && agent.hasPath)
        {
            for (int i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.cyan);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            transform.SetParent(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }
}



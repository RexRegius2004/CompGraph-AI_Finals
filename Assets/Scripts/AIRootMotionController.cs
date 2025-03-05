using UnityEngine;
using UnityEngine.AI;

public class AIRootMotionController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private float maxtarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnValidate()
    {
        if (!agent) agent = GetComponent<NavMeshAgent>();
        if (!animator) animator = GetComponent<Animator>();   
    }
    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath) 
        {
            var dir = (agent.steeringTarget - transform.position).normalized;
            var aniDir = transform.InverseTransformDirection(dir);
            var isFacingMoveDirection = Vector3.Dot(dir, transform.forward) > .5f;

            animator.SetFloat("Horizontal", isFacingMoveDirection ? aniDir.x : 0, .5f, Time.deltaTime);
            animator.SetFloat("Vertical", isFacingMoveDirection ? aniDir.z : 0, .5f, Time.deltaTime);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), 100 * Time.deltaTime);
            if (Vector3.Distance(transform.position, agent.destination) < agent.radius)
            {
                agent.ResetPath();
            }
        }
        else
        {
            animator.SetFloat("Horizontal", 0, .25f, Time.deltaTime);
            animator.SetFloat("Vertical", 0, .25f, Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var isHit = Physics.Raycast(ray, out RaycastHit hit, maxtarget);
            if (isHit) 
            { 
                agent.destination = hit.point;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (agent.hasPath) 
        {
            for (int i = 0; i < agent.path.corners.Length - 1; i++) 
            {
                Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.blue);
            }
        }
    }
}

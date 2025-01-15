using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour
{
    private NavMeshAgent m_agent;

    [SerializeField] private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
           Ray ray = cam.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                m_agent.SetDestination(hit.point);
            }
        }
    }
}

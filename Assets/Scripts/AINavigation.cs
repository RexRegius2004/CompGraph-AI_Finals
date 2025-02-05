using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour
{
   
    [SerializeField] private Camera cam;
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
                 agent.SetDestination(hit.point);
             }
         } 

        
    }
}

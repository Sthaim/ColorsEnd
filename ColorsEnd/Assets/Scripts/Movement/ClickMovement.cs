using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ClickMovement : MonoBehaviour
{
    private NavMeshAgent m_navMeshAgent;

    private Vector3 m_destinationNavMesh;

    // Start is called before the first frame update
    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform != null)
                {
                    m_destinationNavMesh = hit.point;
                    
                }
            }
        }
        if (Vector3.Distance(m_destinationNavMesh, transform.position) > 1)
        {
            GetComponent<NavMeshAgent>().SetDestination(m_destinationNavMesh);
        }
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(NavMeshAgent))]

public class Follower : MonoBehaviour
{
    public ClickMovement m_leaderToFollow;

    private Transform _parent;

    private NavMeshAgent m_agent;

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        _parent = new GameObject("Unit Parent").transform;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetLeader(ClickMovement p_leader)
    {
        m_leaderToFollow = p_leader;
    }

    
}


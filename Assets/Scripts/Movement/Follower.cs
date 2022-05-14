using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class Follower : MonoBehaviour
{
    private ClickMovement m_leaderToFollow;

    private NavMeshAgent m_agent;

    private int m_addedArrive;

    // Start is called before the first frame update
    void Start()
    {
        m_addedArrive = 0;
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_leaderToFollow)
        {
            if (Vector3.Distance(m_leaderToFollow.GetLeaderPosition(), transform.position) > (2 + (m_leaderToFollow.m_nombreArrive * 0.3)))
            {
                m_agent.SetDestination(m_leaderToFollow.GetLeaderPosition());
                if (m_addedArrive != 2)
                {
                    m_leaderToFollow.m_nombreArrive--;
                    m_addedArrive = 2;
                }
            }
            else
            {
                if(m_addedArrive != 1)
                {
                    m_agent.SetDestination(gameObject.transform.position);
                    m_leaderToFollow.m_nombreArrive++;
                    m_addedArrive = 1;
                }
            }
        }
        if (m_leaderToFollow)
        {
            
        }
        
    }

    public void SetLeader(ClickMovement p_leader)
    {
        m_leaderToFollow = p_leader;
    }
}

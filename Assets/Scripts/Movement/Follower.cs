using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(NavMeshAgent))]

public class Follower : MonoBehaviour
{
    public ClickMovement m_leaderToFollow;

    private NavMeshAgent m_agent;

    [SerializeField]
    private LayerMask m_layerToIgnore;

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.avoidancePriority = 99;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetLeader(ClickMovement p_leader)
    {
        m_leaderToFollow = p_leader;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (((1 << collision.gameObject.layer) & m_layerToIgnore) != 0)
        {
            if (ReferenceEquals(collision.GetComponent<Follower>()?.m_leaderToFollow, m_leaderToFollow)==false)
                collision.gameObject.GetComponent<Follower>().m_leaderToFollow.SubUnit(collision.gameObject);  
            Debug.Log("Je suis en collision");
        }
        
    }

}


using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LifeSpan))]
public class Follower : MonoBehaviour
{
    public ClickMovement m_leaderToFollow;

    public NavMeshAgent m_agent;

    [SerializeField]
    private LayerMask m_layerToIgnore;

    private Animator m_anim;

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.avoidancePriority = 99;
        m_anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
    }
    public void SetLeader(ClickMovement p_leader)
    {
        if (m_leaderToFollow != null && m_leaderToFollow != p_leader)
        {
            m_leaderToFollow.DelistUnit(gameObject);
            p_leader.AddUnit(gameObject);
        }
        m_leaderToFollow = p_leader;

        m_anim = p_leader.GetComponent<Animator>();


    }



    /*
    public void SetLeader(ClickMovement p_leader)
    {
        m_leaderToFollow = p_leader;
        if (m_leaderToFollow != null && m_leaderToFollow != p_leader)
        {
            
        }

    }
            /**/

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Je suis en collision");
        if (((1 << collision.gameObject.layer) & m_layerToIgnore) != 0)
        {

            Debug.Log($"leader collision: {collision.GetComponent<Follower>()?.m_leaderToFollow}");
            Debug.Log($"leader: {m_leaderToFollow}");
            if (ReferenceEquals(collision.GetComponent<Follower>()?.m_leaderToFollow, m_leaderToFollow) == false)
            {
                Debug.Log("Je m'accouple");

                if (m_leaderToFollow.m_attack)
                {
                    collision.GetComponent<Follower>().m_leaderToFollow.SubUnit(collision.gameObject);
                    m_leaderToFollow.SubUnit(gameObject);
                }
                else
                {
                    
                    StartCoroutine(m_leaderToFollow.Accouplement(gameObject, collision.gameObject));

                }
            }
        }

    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ClickMovement : MonoBehaviour
{
    private NavMeshAgent m_agent;

    [HideInInspector]
    public int m_nombreArrive = 0;

    private Vector3 m_destination;

    private float m_timer = 0f;

    [SerializeField]
    private float m_tempsSpawnNew;

    [SerializeField]
    private GameObject m_prefAlly;

    private float m_currentTempsSpawn;

    [HideInInspector]
    public State m_curState = State.searchDest;
    [HideInInspector]
    public State m_lastState = State.searchDest;


    public enum State
    {
        searchDest, //0 
        moveToDest, //1
        arrived,    //2
        chasePlayer,//3
        runAway     //4
    }

    private void Start()
    {
        if (!GetComponent<NavMeshAgent>()) return;
        m_agent = GetComponent<NavMeshAgent>();
        //m_agent.SetDestination(this.transform.position + new Vector3(5f, 0f, 0f));
        Debug.Log(State.moveToDest);
        m_currentTempsSpawn = m_tempsSpawnNew;
    }


    // Update is called once per frame
    void Update()
    {
        //if (!GetComponent<NavMeshAgent>()) Debug.Log("PasDeNavMesh"); return;

        switch (m_curState)
        {
            case State.searchDest:  //start getDest function

                GetDestination();
                m_curState = State.moveToDest;


                break;
            case State.moveToDest:  //Go to the destination

                m_agent.SetDestination(m_destination);


                CheckDestination();


                break;
            case State.arrived:


                Timer();

                break;
            case State.chasePlayer:

                break;
            case State.runAway:

                break;
            default:
                break;


        }

        m_currentTempsSpawn -= Time.deltaTime;
        if (m_currentTempsSpawn < 0)
        {
            m_currentTempsSpawn = m_tempsSpawnNew;
            SpawnAlly();
        }
        Debug.Log(m_nombreArrive);

        //GetDestination();

    }
    void GetDestination()
    {
        Debug.Log("yayayo");
        m_destination = new Vector3(Random.Range(0, 20), 0f, Random.Range(0, 20));
    }

    public Vector3 GetLeaderPosition()
    {
        return transform.position;
    }

    void CheckDestination()
    {
        if (Vector3.Distance(transform.position, m_agent.destination) < 1)
        {
            Debug.Log("testDist");
            m_curState = State.arrived;

        }
    }

    void SpawnAlly()
    {
        GameObject obj = Instantiate(m_prefAlly,transform.position,Quaternion.identity);
        obj.GetComponent<Follower>().SetLeader(this);

    }

    void Timer()
    {

        m_timer += Time.deltaTime;
        float rdm = Random.Range(0.2f, 3);
        Debug.Log(rdm);


        if (m_timer > 3)
        {
            m_curState = State.searchDest;
            return;
        }

    }
}

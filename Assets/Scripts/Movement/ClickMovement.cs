using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Linq;

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
    private GameObject m_child;

    [SerializeField]
    private GameObject m_prefAlly;

    private float m_currentTempsSpawn;

    public List<GameObject> _spawnedUnits = new List<GameObject>();

    [HideInInspector]
    public State m_curState = State.searchDest;
    [HideInInspector]
    public State m_lastState = State.searchDest;

    private FormationBase _formation;

    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private float _unitSpeed = 2;

    private List<Vector3> _points = new List<Vector3>();

    [SerializeField]
    private LayerMask m_layerEnnemy;

    public ClickMovement pRef;

    //player information
    public bool isPlayer = false;

    public Camera cam;

    public bool m_attack = false;

    //refs
    [SerializeField]
    private GameObject m_spriteAtk;
    [SerializeField]
    private GameObject m_spritePaix;
    [SerializeField]
    private GameObject m_mesh;

    public FormationBase Formation
    {
        get
        {
            if (_formation == null) _formation = GetComponent<FormationBase>();
            return _formation;
        }
        set => _formation = value;
    }


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
        m_agent.avoidancePriority=99;
    }


    // Update is called once per frame
    void Update()
    {

        if (m_agent.velocity.x > 0)
        {
            //m_mesh.transform.localScale = new Vector3(-m_mesh.transform.localScale.x, 0.5f, 0.5f);
            m_mesh.GetComponent<SpriteRenderer>().flipX = false;

        }
        else
        {
            m_mesh.GetComponent<SpriteRenderer>().flipX = true;
        }

        SetFormation();
        //if (!GetComponent<NavMeshAgent>()) Debug.Log("PasDeNavMesh"); return;
        m_child.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);


        m_currentTempsSpawn -= Time.deltaTime;
        if (m_currentTempsSpawn < 0)
        {
            m_currentTempsSpawn = m_tempsSpawnNew;
            SpawnAlly();
        }

        if(isPlayer)
        {
            UpdatePlayer();
            
            
            return;
        }

        #region IA

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

        //GetDestination();

        #endregion

    }

    private void UpdatePlayer()
    {



        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                m_agent.SetDestination(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            m_attack = !m_attack;

            if (m_attack)
            {
                m_spriteAtk.SetActive(true);
                m_spritePaix.SetActive(false);
                return;
            }
            //m_spriteAtk.GetComponent<Image>().alp;

            m_spriteAtk.SetActive(false);
            m_spritePaix.SetActive(true);
            /*
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if(hit.collider.gameObject.GetComponent<Reciever>())
                {
                    if (!hit.collider.gameObject.GetComponent<Reciever>().canChangeLeader) return;
                    hit.collider.gameObject.GetComponent<Reciever>().SetLeader(this);
                }
            }
            /**/
        }
/*

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_attack = !m_attack;

            if (m_attack)
            {
                m_spriteAtk.SetActive(true);
                m_spritePaix.SetActive(false);
                return;
            }

            m_spriteAtk.SetActive(false);
            m_spritePaix.SetActive(true);

        }
/**/


    }
    void GetDestination()
    {
        m_destination = new Vector3(Random.Range(0, 20), 0f, Random.Range(0, 20));
    }

    #region SpawnChilds

    private void SetFormation()
    {
        _points = Formation.EvaluatePoints().ToList();
        for (var i = 0; i < _spawnedUnits.Count; i++)
        {
            _spawnedUnits[i].GetComponent<NavMeshAgent>().SetDestination(_points[i] + (m_child.transform.position + new Vector3(-1.7f, -0f, -0.7f)));
        }
    }

    public void AddUnit(GameObject obj)
    {
        Debug.Log("J'ajoute une entité");
        GetComponent<RadialFormation>()._amount++;
        _spawnedUnits.Add(obj);
        if(obj.GetComponent<Reciever>())
        {
            obj.GetComponent<Reciever>().m_Leader = this;
            if (isPlayer)
            {
                obj.GetComponent<Reciever>().canChangeLeader = false;
            }
        }
    }

    public void SubUnit(GameObject obj)
    {
        Debug.Log("Je supprime une entité");
        GetComponent<RadialFormation>()._amount--;
        _spawnedUnits.Remove(obj);
        Destroy(obj);
    }

    public Vector3 GetLeaderPosition()
    {
        return transform.position;
    }

    void CheckDestination()
    {
        if (Vector3.Distance(transform.position, m_agent.destination) < 1)
        {
            m_curState = State.arrived;

        }
    }

    void SpawnAlly()
    {

        GameObject obj = Instantiate(m_prefAlly,transform.position,Quaternion.identity);
        obj.GetComponent<Follower>().SetLeader(this);   // init du leader au cas ou c'est une ia
        AddUnit(obj);

    }

    #endregion

    void Timer()
    {

        m_timer += Time.deltaTime;
        float rdm = Random.Range(0.2f, 3);


        if (m_timer > 3)
        {
            m_curState = State.searchDest;
            return;
        }

    }

    public void SetPlayerRef(ClickMovement _ref)
    {
        _ref.SetNewChilds(_spawnedUnits, this);
    }

    public void SetNewChilds(List<GameObject> _list , ClickMovement _ref)
    {

        /*
        foreach (GameObject i in _list)
        {
            GetComponent<RadialFormation>()._amount++;
            Debug.Log(_list[i]);
            _spawnedUnits.Add();

        }
        /**/

        for (int i = 0; i< _list.Count; i++)
        {

            AddUnit(_list[i]);
           if(_list[i].GetComponent<Follower>())
           {
                _list[i].GetComponent<Follower>().SetLeader(this);
           }
            //_ref.SubUnit(_ref._spawnedUnits[i]);
            Debug.Log(_ref);


            Debug.Log("Je m'ajjouttte");
        }
        for (int i = 0; i < _list.Count; i++)
        {

            _ref.DeleteFromList(_ref._spawnedUnits[i]);

            Debug.Log("Je m'ajjouttte");
        }

        SpawnAlly();
        _ref.DestroySelf();
        Debug.Log(_ref);
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
    }

    public void DeleteFromList(GameObject obj)
    {
        Debug.Log("Je supprime une entité");
        GetComponent<RadialFormation>()._amount--;
        _spawnedUnits.Remove(obj);
        
    }
    public IEnumerator Accouplement(GameObject membreCouple1, GameObject membreCouple2)
    {
        if (membreCouple1 != null && membreCouple2 != null)
        {
            membreCouple1.GetComponent<Collider>().enabled = false;
            membreCouple2.GetComponent<Collider>().enabled = false;
            membreCouple1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            membreCouple2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if (membreCouple1.GetComponent<NavMeshAgent>().pathStatus != NavMeshPathStatus.PathInvalid && membreCouple2.GetComponent<NavMeshAgent>().pathStatus != NavMeshPathStatus.PathInvalid)
            {
                membreCouple1.GetComponent<NavMeshAgent>().isStopped = true;
                membreCouple2.GetComponent<NavMeshAgent>().isStopped = true;
            }
        }
        yield return new WaitForSeconds(3f);

        if (membreCouple1 != null)
        {
            membreCouple1.GetComponent<Follower>().m_leaderToFollow.SpawnAlly2().transform.position = membreCouple1.transform.position;
            membreCouple1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            if (membreCouple1.GetComponent<NavMeshAgent>().pathStatus != NavMeshPathStatus.PathInvalid)
            {
                membreCouple1.GetComponent<NavMeshAgent>().isStopped = false;
            }
        }
        if (membreCouple2 != null)
        {
            if(membreCouple2.GetComponent<NavMeshAgent>().pathStatus != NavMeshPathStatus.PathInvalid)
            {
                 membreCouple2.GetComponent<NavMeshAgent>().isStopped = false;
            }
            membreCouple2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }

        yield return new WaitForSeconds(3f);
        if (membreCouple1 != null)
        {
            membreCouple1.GetComponent<Collider>().enabled = true;
        }

        if (membreCouple2 != null)
        {
            membreCouple2.GetComponent<Collider>().enabled = true;
        }

        yield return new WaitForSeconds(1f);
    }

    GameObject SpawnAlly2()
    {
        GameObject obj = Instantiate(m_prefAlly, transform.position, Quaternion.identity);
        obj.GetComponent<Follower>().SetLeader(this);
        AddUnit(obj);
        return obj;

    }

    public void DelistUnit(GameObject obj)
    {
        Debug.Log("Je supprime une entité");
        GetComponent<RadialFormation>()._amount--;
        _spawnedUnits.Remove(obj);
    }

    public void ActivateFromButton(bool bol)
    {
        m_attack = bol;
        Debug.Log(m_attack);
    }

}

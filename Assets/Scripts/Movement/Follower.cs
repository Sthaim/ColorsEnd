using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;



[RequireComponent(typeof(NavMeshAgent))]

public class Follower : MonoBehaviour
{
    private FormationBase _formation;

    public FormationBase Formation
    {
        get
        {
            if (_formation == null) _formation = GetComponent<FormationBase>();
            return _formation;
        }
        set => _formation = value;
    }

    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private float _unitSpeed = 2;

    private readonly List<GameObject> _spawnedUnits = new List<GameObject>();
    private List<Vector3> _points = new List<Vector3>();
    private Transform _parent;

    private ClickMovement m_leaderToFollow;

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
        SetFormation();
        /*if (m_leaderToFollow)
        {
            if (Vector3.Distance(m_leaderToFollow.GetLeaderPosition(), transform.position) > (3 + (m_leaderToFollow.m_nombreArrive * 0.7)))
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
        }*/
    }

    public void SetLeader(ClickMovement p_leader)
    {
        m_leaderToFollow = p_leader;
    }

    private void SetFormation()
    {
        _points = Formation.EvaluatePoints().ToList();

        /*if (_points.Count > _spawnedUnits.Count)
        {
            var remainingPoints = _points.Skip(_spawnedUnits.Count);
            Spawn(remainingPoints);
        }
        else if (_points.Count < _spawnedUnits.Count)
        {
            Kill(_spawnedUnits.Count - _points.Count);
        }*/

        for (var i = 0; i < _spawnedUnits.Count; i++)
        {
            _spawnedUnits[i].transform.position = Vector3.MoveTowards(_spawnedUnits[i].transform.position, transform.position + _points[i], _unitSpeed * Time.deltaTime);
        }
    }

    public void Spawn(IEnumerable<Vector3> points)
    {
        foreach (var pos in points)
        {
            var unit = Instantiate(_unitPrefab, transform.position + pos, Quaternion.identity);
            _spawnedUnits.Add(unit);
        }
    }

    private void Kill(int num)
    {
        for (var i = 0; i < num; i++)
        {
            var unit = _spawnedUnits.Last();
            _spawnedUnits.Remove(unit);
            Destroy(unit.gameObject);
        }
    }
}
}

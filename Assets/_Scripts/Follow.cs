using UnityEngine;

/// <summary>
/// Instructs NavMeshAgent to follow the Target
/// </summary>
public class Follow : MonoBehaviour
{
    protected NavMeshAgent _agent;

    protected bool _canFollow = false;
    public bool CanFollow
    {
        get
        {
            return _canFollow;
        }

        set
        {
            _canFollow = value;
        }
    }

    protected GameObject _target;
    public GameObject Target
    {
        get
        {
            return _target;
        }

        set
        {
            _target = value;

            // agent settings
            this._agent.autoBraking = true;
            this._agent.stoppingDistance = 1.5f;
        }
    }

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_target != null && _canFollow) {
            _agent.destination = _target.transform.position;
        }
    }
}

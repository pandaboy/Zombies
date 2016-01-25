using UnityEngine;
using System.Collections;

public class Chase : MonoBehaviour
{
    // Comma-separated list of tags the agent will follow
    public string follow = "Citizen";

    private string[] _targetTags;
    
    private GameObject _target;
    public GameObject Target
    {
        get
        {
            return _target;
        }

        set
        {
            _target = value;
        }
    }

    private NavMeshAgent _agent;

	void Awake ()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetFollow(follow);
	}

    // splits the follow string and assigns the tags to targetTags
    public void SetFollow(string s = null)
    {
        if (s == null)
            return;

        follow = s;
        _targetTags = s.Split(',');
    }
	
	void Update ()
    {
        if (_target != null) 
            _agent.destination = _target.transform.position;
	}

    void OnTriggerEnter(Collider other)
    {
        // check if we have any tags first
        if (_targetTags == null || _targetTags.Length == 0)
            return;

        // we don't chase Zombies
        if (other.gameObject.tag == "Zombie")
            _target = null;

        // follow the gameobjects with the given tags (comma separated)
        foreach (string tt in _targetTags)
        {
            if (other.gameObject.tag == tt) {
                _target = other.gameObject;
            }
        }
    }
}

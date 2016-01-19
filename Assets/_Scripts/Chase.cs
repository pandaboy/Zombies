using UnityEngine;
using System.Collections;

public class Chase : MonoBehaviour
{
    public string follow = "Citizen";

    private string[] targetTags;
    private GameObject target;
    private NavMeshAgent agent;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        SetFollow(this.follow);
	}

    public void SetFollow(string s = null)
    {
        if (s == null)
            return;

        this.follow = s;
        this.targetTags = s.Split(',');
    }
	
	void Update ()
    {
        if (target != null) 
            agent.destination = target.transform.position;

	}

    void OnTriggerEnter(Collider other)
    {
        // check if we have any tags first
        if (this.targetTags.Length == 0)
            return;

        // we don't chase Zombies
        if (other.gameObject.tag == "Zombie")
            this.target = null;

        // follow the gameobjects with the given tags (comma separated)
        foreach (string tt in this.targetTags)
        {
            if (other.gameObject.tag == tt)
            {
                this.target = other.gameObject;
            }
        }
    }
}

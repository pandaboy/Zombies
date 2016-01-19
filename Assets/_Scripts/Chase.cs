using UnityEngine;
using System.Collections;

public class Chase : MonoBehaviour {

    public GameObject target;
    private NavMeshAgent agent;

	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
        if (target) {
            agent.destination = target.transform.position;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.target = other.gameObject;
        }
    }
}

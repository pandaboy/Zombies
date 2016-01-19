using UnityEngine;

public class Citizen : MonoBehaviour
{
    private GameObject target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target)
        {
            agent.destination = target.transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // tell agent to chase player
        if (other.gameObject.tag == "Player")
        {
            this.target = other.gameObject;
        }
    }
}

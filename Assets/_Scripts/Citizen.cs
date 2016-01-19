using UnityEngine;

public class Citizen : MonoBehaviour
{
    public Transform[] pointsOfInterest;

    private int currentInterest = 0;
    private GameObject target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToPointOfInterest();
    }

    void Update()
    {
        if (target)
        {
            agent.destination = target.transform.position;
        }
        else
        {
            GoToPointOfInterest();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // tell agent to chase player
        if (other.gameObject.tag == "Player")
        {
            this.target = other.gameObject;
            agent.autoBraking = true;
        }
    }

    void GoToPointOfInterest()
    {
        // check if there's anywhere to go
        if (pointsOfInterest.Length == 0)
        {
            return;
        }

        if (agent.remainingDistance < 0.5f || agent.destination == Vector3.zero)
        {
            currentInterest = (currentInterest + 1) % pointsOfInterest.Length;
            Debug.Log(currentInterest);
            agent.destination = pointsOfInterest[currentInterest].position;
        }

        agent.autoBraking = false;

    }
}

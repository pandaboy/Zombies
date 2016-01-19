using UnityEngine;

public class Citizen : MonoBehaviour
{
    public Transform[] pointsOfInterest;

    private int currentInterest = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToPointOfInterest();
    }

    void Update()
    {
        GoToPointOfInterest();
    }

    void GoToPointOfInterest()
    {
        // check if there's anywhere to go
        if (pointsOfInterest.Length == 0)
            return;

        if (agent.remainingDistance < 0.5f || agent.destination == Vector3.zero) {
            currentInterest = (currentInterest + 1) % pointsOfInterest.Length;
            agent.destination = pointsOfInterest[currentInterest].position;
        }

    }
}

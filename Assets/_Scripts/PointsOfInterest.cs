using UnityEngine;
using System.Collections;

public class PointsOfInterest : MonoBehaviour
{
    public Transform[] points;

    private int current = 0;
    private NavMeshAgent agent;

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.stoppingDistance = 0.5f;
	}
	
    void Update ()
    {
        if (points.Length != 0)
        {
            GoToNextPoint();
        }
	}

    void GoToNextPoint ()
    {
        if (agent.destination == Vector3.zero)
        {
            agent.destination = points[current].position;
        }

        // if we are close to the destination,
        // update to target next point of interest
        if (agent.remainingDistance < 0.5f)
        {
            // update to next location
            current = (current + 1) % points.Length;
            agent.destination = points[current].position;
        }
    }
}

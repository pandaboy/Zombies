using UnityEngine;

/// <summary>
/// Moves the NavMeshAgent to pre-designated points of interest
/// </summary>
public class PointsOfInterest : MonoBehaviour
{
    public Transform[] points;      // points of interest to navigate between

    protected int _current = 0;     // current point of interest
    protected NavMeshAgent _agent;

    void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking      = false; // no slowing down near destinations
        _agent.stoppingDistance = 0.5f;  // distance before switching current
	}
	
    void Update ()
    {
        // provided we have places to go, go to them
        if (points.Length != 0) {
            GoToNextPoint();
        }
	}

    /// <summary>
    /// Moves character to current point of interest
    /// </summary>
    /// <returns></returns>
    void GoToNextPoint ()
    {
        // if we have no destination set, set the first point as the destination
        if (_agent.destination == Vector3.zero) {
            _agent.destination = points[_current].position;
        }

        // if we are close to the destination,
        // update to target next point of interest
        if (_agent.remainingDistance < 0.5f) {
            // update to next location, loop back to the first point when done.
            _current = (_current + 1) % points.Length;
            _agent.destination = points[_current].position;
        }
    }
}

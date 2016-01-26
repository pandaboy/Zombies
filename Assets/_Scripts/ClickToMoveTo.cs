using UnityEngine;

/// <summary>
/// Sets the NavMeshAgent's destination to where the user clicked.
/// </summary>
public class ClickToMoveTo : MonoBehaviour
{
    protected NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                /// tell the agent to go to the mouse click position
                agent.destination = hit.point;
            }
        }
    }
}

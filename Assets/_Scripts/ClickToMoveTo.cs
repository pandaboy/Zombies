using UnityEngine;

public class ClickToMoveTo : MonoBehaviour
{

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                /// tell the agent to go to the mouse click position
                agent.destination = hit.point;
            }
        }
    }
}

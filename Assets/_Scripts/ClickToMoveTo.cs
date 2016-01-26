using UnityEngine;

/// <summary>
/// Sets the NavMeshAgent's destination to where the user clicked.
/// </summary>
public class ClickToMoveTo : MonoBehaviour
{
    // detect double clicks
    protected bool _oneClick = false;
    protected float _timer;
    protected float _clickDelay = 0.5f;

    protected NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (!_oneClick) {
                _oneClick = true;
                _timer = Time.time;
            }
            else {
                _oneClick = false;

                // move on double click
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                    /// tell the agent to go to the mouse click position
                    _agent.destination = hit.point;
                }
            }
        }

        if (_oneClick) {
            if ((Time.time - _timer) > _clickDelay) {
                _oneClick = false;
            }
        }
    }
}

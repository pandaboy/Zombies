using UnityEngine;
using Zombies;

/// <summary>
/// Detects if the actor was clicked on, and displays their relationships
/// if true
/// </summary>
public class ClickCharacter : MonoBehaviour
{
    protected Actor _actor;
    protected GameController _gc;
    protected ZombieGraph _graph;

    void Start()
    {
        _graph = ZombieGraph.Instance;
        _actor = GetComponent<Actor>();
        _gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                if (hit.transform.tag == "NPC") {
                    _actor.DisplayRelationships();
                }
            }
        }
    }
}

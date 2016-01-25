using UnityEngine;
using Zombies;

// detects if the character was clicked on
public class ClickCharacter : MonoBehaviour
{
    private Actor _actor;

    private GameController _gc;
    private ZombieGraph _graph;

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

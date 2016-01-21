using UnityEngine;
using Zombies;

// detects if the character was clicked on
public class ClickCharacter : MonoBehaviour
{
    private Actor actor;

    private GameController _gc;
    public GameController GC
    {
        set
        {
            _gc = value;
        }

        get
        {
            return _gc;
        }
    }

    void Start()
    {
        this.actor = GetComponent<Actor>();
    }
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.transform.tag == "NPC")
                {
                    GameController.UpdateDialog(
                        "Hi! I'm a " + this.actor.actorType + " type!"
                    );
                }
            }
        }
    }
}

using UnityEngine;
using Zombies;

/// <summary>
/// Detects if the actor was (double) clicked on, and displays their relationships
/// if true
/// </summary>
public class ClickCharacter : MonoBehaviour
{
    // detect double clicks
    protected bool _oneClick = false;
    protected float _timer;
    protected float _clickDelay = 0.5f;
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (!_oneClick) {
                _oneClick = true;
                _timer = Time.time;

                // single click
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    if (hit.transform.tag == "NPC")
                    {
                        hit.transform.GetComponent<Actor>().DisplayRelationships();
                    }

                    if (hit.transform.tag == "Player")
                    {
                        hit.transform.GetComponent<Actor>().DisplayRelationships();
                    }
                }
            }
            else {
                _oneClick = false;
            }
        }

        if(_oneClick) {
            if((Time.time - _timer) > _clickDelay) {
                _oneClick = false;
            }
        }
    }
}

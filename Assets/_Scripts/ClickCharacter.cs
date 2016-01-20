using UnityEngine;

// detects if the character was clicked on
public class ClickCharacter : MonoBehaviour {
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.transform.tag == "NPC")
                {
                    hit.transform.gameObject.GetComponent<Stats>().ShowStats();
                }
            }
        }
	}
}

using UnityEngine;
using System.Collections.Generic;
using Zombies;

public class DriveOff : MonoBehaviour
{
    private ZombieGraph _graph;
    
    public float speed = 10.0f;
    
    private bool drive_off;
    public bool Drive_Off
    {
        get
        {
            return drive_off;
        }

        set
        {
            drive_off = value;
        }
    }

	void Start ()
    {
        _graph = ZombieGraph.Instance;
        drive_off = false;
	}
	
	void Update ()
    {
        if (drive_off) {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
	}

    public void Escape()
    {
        drive_off = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") {
            Escape();
            Destroy(other.gameObject);
        }
    }
}

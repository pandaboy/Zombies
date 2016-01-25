using UnityEngine;
using System.Collections;

public class DriveOff : MonoBehaviour
{
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

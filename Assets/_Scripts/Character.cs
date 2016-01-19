using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour {

    public float speed = 3.5f;
    private Vector3 movement = Vector3.zero;
    private Rigidbody rb;

	void Awake ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        move(h, v);
        turn();
	}

    private void move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }

    private void turn()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 90f, transform.rotation.z));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, -90f, transform.rotation.z));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180f, transform.rotation.z));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0f, transform.rotation.z));
        }
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(transform.rotation.x, -90, transform.rotation.z);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Rotate(transform.rotation.x, 0, transform.rotation.z);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Rotate(transform.rotation.x, 180, transform.rotation.z);
        }
        */
    }
}

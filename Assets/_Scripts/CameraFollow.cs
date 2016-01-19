using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;

	void Start ()
    {
        offset = transform.position - target.position;
	}
	
	void FixedUpdate ()
    {
        if (target) {
            Vector3 targetCamPos = target.position + offset;

            // interpolate between camera's current position and it's target position
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
	}
}

using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float smoothing = 5f;

    private Transform _target;
    public Transform Target
    {
        get {
            return _target;
        }

        set {
            _target = value;
            offset = transform.position - _target.position;
        }
    }
    private Vector3 offset;
	
	void FixedUpdate ()
    {
        if (Target) {
            Vector3 targetCamPos = Target.position + offset;

            // interpolate between camera's current position and it's target position
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
	}
}

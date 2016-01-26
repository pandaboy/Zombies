using UnityEngine;

/// <summary>
/// Camera follows the target and maintains it's distance
/// to the target.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public float smoothing = 5f;

    protected Transform _target;
    public Transform Target
    {
        get
        {
            return _target;
        }

        set
        {
            _target = value;
            _offset = transform.position - _target.position;
        }
    }

    private Vector3 _offset;
	
	void FixedUpdate ()
    {
        if (Target) {
            Vector3 targetCamPos = Target.position + _offset;

            // interpolate between camera's current position and it's target position
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
	}
}

using UnityEngine;
using System.Collections;
using Completed;

public class SmoothFollow : MonoBehaviour {
	public float dampTime = 0.001f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	// Use this for initialization
	void Start () {
        
	}

    public void setTarget(Transform transform) {
        target = transform;
    }

	
	// Update is called once per frame
	void FixedUpdate () {
		if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
			Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.50f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
            transform.position = destination;
            
            //transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}

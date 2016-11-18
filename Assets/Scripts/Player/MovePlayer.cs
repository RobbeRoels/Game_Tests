using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public float maxSpeed = 20.0f;

    public delegate void ExitReached();
    public static event ExitReached OnExitReached;

    // Update is called once per frame
    void FixedUpdate () {
		ManageMovement ();
	}

	
	void ManageMovement(){
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")){
			//Vector3 direction = transform.up*inputX + transform.right*inputY; //Direction relative to rotation
			Vector3 direction = new Vector3 (inputX, inputY,0); //Direction relative to screen
			direction.Normalize ();
			transform.position = transform.position + direction*(maxSpeed*Time.deltaTime);
		}
	}

    void OnCollisionEnter2D(Collision2D coll) {
        Debug.Log(coll.gameObject.tag);
        if (coll.gameObject.tag == "Exit") {
            if (OnExitReached != null) {
                OnExitReached();
            }
        }

    }
}

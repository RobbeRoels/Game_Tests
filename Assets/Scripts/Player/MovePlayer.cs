using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public float walkSpeed = 3.5f;
	public float runSpeed = 5f;


	public float maxStamina = 100.0f;
	public float stamina = 100.0f;
	public float staminaUsage = 10f;
	public float staminaRegen = 7.5f;

	public bool exhausted = false;


    public delegate void ExitReached();
    public static event ExitReached OnExitReached;

    // Update is called once per frame
    void FixedUpdate () {
		ManageMovement ();
	}

	
	void ManageMovement(){
		bool running = false;
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		float speed = walkSpeed;
		if (Input.GetKey (KeyCode.LeftShift)) {
			if (stamina > 0) {
				if (!exhausted || stamina > 30) {
					exhausted = false;
					speed = runSpeed;
					running = true;
				}
			} else {
				exhausted = true;
			}
		}

		if (Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) {
			//Vector3 direction = transform.up*inputX + transform.right*inputY; //Direction relative to rotation
			Vector3 direction = new Vector3 (inputX, inputY, 0); //Direction relative to screen
			direction.Normalize ();
			transform.position = transform.position + direction * (speed * Time.deltaTime);
			if (running) {
				lowerStamina ();
			} else {
				regenStamina (true);
			}
		} else {
			regenStamina (false);
		}


	}

	void lowerStamina(){
		stamina -= (Time.deltaTime * staminaUsage);
		if (stamina < 0) {
			stamina = 0;
		}
	}
	void regenStamina(bool moving){			
		if (!moving) {
			stamina += (Time.deltaTime * (staminaRegen));
		} else {
			stamina += (Time.deltaTime * (staminaRegen* 0.5f));
		}
		if (stamina > maxStamina) {
			stamina = maxStamina;
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

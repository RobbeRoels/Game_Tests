using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public float walkSpeed = 3.5f;
	public float runSpeed = 5f;
    private float previousInputX = 0;
    private float previousInputY = 0;

    bool moving = false;
    public float speedMultiplier = 0f;

    public float maxStamina = 100.0f;
	public float stamina = 100.0f;
	public float staminaUsage = 10f;
	public float staminaRegen = 7.5f;

	public bool exhausted = false;


    public delegate void ExitReached();
    public static event ExitReached OnExitReached;
    public delegate void EntranceReached();
    public static event EntranceReached OnEntranceReached;

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

        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
        {
            bool forward = false;
            Vector3 direction = new Vector3();
            if (Input.GetButton("Vertical"))
            {
                forward = true;
                //Vector3 direction = transform.up*inputX + transform.right*inputY; //Direction relative to rotation
                //Vector3 direction = new Vector3 (, inputY, 0); //Direction relative to screen
                Vector2 v2direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)) - this.transform.position;
                v2direction *= inputY;
                direction = (Vector3)v2direction;

               

                if (running)
                {
                    lowerStamina();
                }
                else
                {
                    regenStamina(true);
                }
            }
            if (Input.GetButton("Horizontal"))
            {
                direction = direction + ((transform.up*2) * inputX * -1);

                //Quaternion quat = Quaternion.AngleAxis(90, Vector3.direction);
            }
            if (moving)
            {
                if (speedMultiplier < 1f)
                    speedMultiplier += (1.5f * Time.deltaTime);
            }
            else
            {
                moving = true;
            }

            direction.Normalize();
            transform.position = transform.position + direction * ((speed * speedMultiplier) * Time.deltaTime);

        }
        else {
            speedMultiplier = 0;
            moving = false;
            regenStamina(true);
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
        if (coll.gameObject.tag.ToLower() == "exit") {
            if (OnExitReached != null) {
                OnExitReached();
            }
        }
        if (coll.gameObject.tag.ToLower() == "entrance")
        {
            if (OnEntranceReached != null)
            {
                OnEntranceReached();
            }
        }

    }
}

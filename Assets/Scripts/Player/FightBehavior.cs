using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FightBehavior : MonoBehaviour {
	public GameObject bullet;
	public ShootBehavior sBehavior;
	public Text ammoText;
	public Slider ammoSlider;

	public bool reloading = false;
	public float reloadTimer;
	bool released = true;
	float timer = 10000f;

	// Use this for initialization

	void Start () {
		ammoText.text = sBehavior.clipSize + "";
		ammoSlider.maxValue = sBehavior.clipSize;
		ammoSlider.value = sBehavior.clipSize;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButton ("Fire1") && released || Input.GetButton ("Fire1") && sBehavior.auto){
			if(!reloading){
				released = false;
				if(timer >= sBehavior.delay){
					timer = 0.0f;
					if(sBehavior.fire){
						FireProjectiles();
					}else{
						MeleeAttack();
					}
				}
			}
		} else if(Input.GetButton ("Reload")){
			reloading = true;
			reloadTimer = 0.0f;
			ammoText.text = "Reloading...";
		}
		if (reloading) {
			reloadTimer += Time.deltaTime;
			print (reloadTimer >= sBehavior.reloadTime);
			reloading = reloadTimer <= sBehavior.reloadTime;
			if (!reloading) { //reloading done, fill clip
				sBehavior.currentClip = sBehavior.clipSize;
				ammoSlider.value = sBehavior.clipSize;
				ammoText.text = sBehavior.clipSize + "";
			}
		}
		if (!Input.GetButton ("Fire1")) {
			released = true;
		}
		timer+=Time.deltaTime;
	}

	void FireProjectiles(){
		//Vector3 position = Input.mousePosition;
		if (! (sBehavior.currentClip == 0)) {
			GameObject b = Instantiate (bullet, transform.position, transform.rotation) as GameObject;
			b.GetComponent<Damage> ().damage = sBehavior.damagePerProjectile;
			b.GetComponent<Rigidbody2D> ().AddForce (b.transform.right * 500);
			sBehavior.currentClip--;
			ammoSlider.value = sBehavior.currentClip;
			ammoText.text = sBehavior.currentClip + "";
		} else {
			reloading = true;
			reloadTimer = 0.0f;
			ammoText.text = "Reloading...";
		}
	}
	

	void MeleeAttack(){
		//TODO: Not implemented
	}

}

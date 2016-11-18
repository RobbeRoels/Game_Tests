using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

	public int damage = 10;
	public float lifeTimeAfterHit = 0.5f;
	public bool hit = false;

	private float timer = 0.0f;

	public void Update(){
		if(hit){
			timer += Time.deltaTime;
			if(timer >= lifeTimeAfterHit){
				Destroy(transform.gameObject);
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (! (other.GetComponent<HealthManager> ()==null) ) {
			other.GetComponent<HealthManager> ().TakeDamage(damage);
			hit = true;
		}
		if (other.tag == "Env" || other.tag == "Enemy") {
			hit = true;
		}

	}

}

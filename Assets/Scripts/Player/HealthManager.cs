using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthManager : MonoBehaviour {


	public int maxHealth = 150;
	public int health;
	public Slider healthSlider;
	public Text healthText;
	public void Awake(){
		health = maxHealth;
	}

	public void TakeDamage(int damage){
		health -= damage;
		if (healthSlider != null) {
			healthSlider.value = health;
			healthText.text = health + "";
		}
	}
}

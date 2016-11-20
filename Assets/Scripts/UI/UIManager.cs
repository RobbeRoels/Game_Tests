using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
	GameObject UI;

	void awake(){
		UI = GameObject.Find ("UICanvas");
	}

	// Update is called once per frame
	void Update () {
		if (!(GameObject.FindGameObjectWithTag ("Player") == null)) {
			float stamina = GameObject.FindGameObjectWithTag ("Player").GetComponent<MovePlayer> ().stamina;
			GameObject staminaText = GameObject.Find ("StaminaText");
			staminaText.GetComponent<Text> ().text = string.Format ("Stamina: {0:0}", stamina);
			if (stamina == 0) {
				staminaText.GetComponent<Text> ().color = Color.red;
			} else {
				if (stamina >= 30) {
					staminaText.GetComponent<Text> ().color = Color.white;
				}
			}
		}
	}
}

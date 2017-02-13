using UnityEngine;
using System.Collections;

public class ShadowScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.receiveShadows = true;
    }

    // Update is called once per frame
    void Update () {
	
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePerson : InteractableObject {
    public string text = "";

    public override void Interact()
    {
        Debug.Log(text);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



}

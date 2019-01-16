using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScript : MonoBehaviour {

    List<Collider2D> interactionObjects = new List<Collider2D>();
    // Use this for initialization
    void Start() {

    }


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (interactionObjects.Count > 0) {
                if (interactionObjects.Count == 1) {
                    interactionObjects[0].GetComponent<InteractableObject>().Interact();
                }
            }

        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");

        if (collision.tag.ToLower() == "interactable")
        {
            interactionObjects.Add(collision);
            Debug.Log(string.Format("In interaction zone of {0}", collision.name));
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Triggered");

        if (collision.tag.ToLower() == "interactable")
        {
            interactionObjects.Remove(collision);
            Debug.Log(string.Format("Out of interaction zone of {0}", collision.name));
        }
    }


}

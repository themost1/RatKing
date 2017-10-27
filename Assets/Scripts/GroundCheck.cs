using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private PlayerMovement player;

	// Use this for initialization
	void Start () {
        if (GetComponent<PlayerMovement>() == null)
            print("no script!");

        player = GetComponent<PlayerMovement>();
	}
	
	void OnTriggerEnter2D(Collider2D col)
    {
        player.grounded = true;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        player.grounded = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        player.grounded = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int corpsesHeld = 0;
    private Rigidbody2D rb2d;
    public float defaultMass = 3.5f;
    public float massMultiplier = 1f;

    // Use this for initialization
    void Awake()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        corpsesHeld++;
    }

    // Update is called once per frame
    void Update () {
        massMultiplier = 1f + 0.1f * corpsesHeld;

        rb2d.mass = massMultiplier * defaultMass;
        transform.localScale = new Vector3(massMultiplier, massMultiplier, massMultiplier);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "thisistext"+corpsesHeld);
    }
}

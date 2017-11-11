using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public float massHeld = 0f;
    private Rigidbody2D rb2d;
    public float defaultMass = 3.5f;
	public float massScale = 2.0f;
    public float massMultiplier = 2f;

    // Use this for initialization
    void Awake()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start () {
		
	}

    public void changeMass(float delta)
    {
        massHeld = massHeld + delta;
    }

    // Update is called once per frame
    void Update () {
		massMultiplier = massScale*(1f + 0.1f * massHeld);

        rb2d.mass = massMultiplier * defaultMass;
        transform.localScale = new Vector3(massMultiplier, massMultiplier, massMultiplier);
    }

    void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 100, 20), "thisistext"+corpsesHeld);
    }
}

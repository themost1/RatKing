using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public float health = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void dealDamage(float f) {
		health -= f;
		if (health <= 0)
			kill ();
		print (health);
	}

	public void kill() {
		Destroy (gameObject);
	}
}

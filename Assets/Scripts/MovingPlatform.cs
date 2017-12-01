using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Vector2 start;
	public Vector2 end;
	public float rate;
	private bool direction;
	private float position;

	// Use this for initialization
	void Start () {
		direction = true;
		position = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (direction)
		{
			position += rate;
		}
		else
		{
			position -= rate;
		}
		if (position > 1)
		{
			position = 1;
			direction = false;
		}
		else if(position < 0)
		{
			position = 0;
			direction = true;
		}
		transform.position = Vector2.Lerp(start, end, position);
	}
}

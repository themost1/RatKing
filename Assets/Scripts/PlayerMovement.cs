﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
	public float throwSpeed = 100f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public GameObject projectileObj;
    public Rigidbody2D projectile;
	public GameObject jumpParticle;

    private bool hasJetpack;

    private bool canThrow = true;


    private bool grounded = false;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake()
    {
		throwSpeed = 20;
        rb2d = GetComponent<Rigidbody2D>();
        projectile = projectileObj.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if ((Input.GetButtonDown("Jump") && grounded) || (Input.GetButton("Jump") && hasJetpack))
        {
			Collider2D col = GetComponent<Collider2D>();
			Vector2 loc = col.bounds.center;
			loc.y = loc.y - col.bounds.extents.y;
			Vector2 loc2 = col.bounds.center;
			loc2.y = loc2.y + col.bounds.extents.y;
			GameObject child = Instantiate (jumpParticle, loc, Quaternion.identity);
			GameObject child2 = Instantiate (jumpParticle, loc2, Quaternion.AngleAxis(90, Vector3.forward));
			child.transform.parent = transform;
			child2.transform.parent = transform;
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float h = 0;
        if (Input.GetKey(KeyCode.A))
            h = -1f;
        else if (Input.GetKey(KeyCode.D))
            h = 1f;

        float throwDir = 0;
        if (Input.GetKey(KeyCode.RightArrow))
            throwDir = 1;
        else if (Input.GetKey(KeyCode.LeftArrow))
            throwDir = -1;
        if (throwDir == 0) { canThrow = true; }

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        if (throwDir!=0 && gameObject.GetComponent<Inventory>().massHeld>=1 && canThrow)
        {
            gameObject.GetComponent<Inventory>().changeMass(-1);
            GameObject clone = Instantiate(projectileObj, transform.position, transform.rotation);

			clone.GetComponent<Rigidbody2D>().velocity = Vector2.right *throwDir * throwSpeed;
			clone.GetComponent<Rigidbody2D> ().gravityScale = 1;
            clone.GetComponent<CorpsePickup>().thrownByPlayer = true;

            //so particles don't fly out all at once
            canThrow = false;
        }
    }

    public void setJetpack(bool jetpack)
    {
        hasJetpack = jetpack;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
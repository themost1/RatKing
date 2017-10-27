using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float speed;             //Floating point variable to store the player's movement speed.
    public float jumpSpeed;
    public bool grounded = false;
    public bool jump = false;
    public float defaultMass = 3.5f;
    public float massMultiplier = 1f;
    //local scale default x and y
    public float initialLSX;
    public float initialLSY;


    public Transform groundCheck1, groundCheck2, groundCheck3;


    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Awake()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb2d.mass = defaultMass;
        initialLSX = transform.localScale.x;
        initialLSY = transform.localScale.y;
    }

    void Update()
    {
        if (Physics2D.Linecast(transform.position, groundCheck1.position, 1 << LayerMask.NameToLayer("Ground")))
            grounded = true;
        else if (Physics2D.Linecast(transform.position, groundCheck2.position, 1 << LayerMask.NameToLayer("Ground")))
            grounded = true;
        else if (Physics2D.Linecast(transform.position, groundCheck3.position, 1 << LayerMask.NameToLayer("Ground")))
            grounded = true;
        else
            grounded = false;

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (jump)
        {
            rb2d.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
            jump = false;
        }
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, 0);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.W))
        {
            massMultiplier += 0.02f;
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            massMultiplier -= 0.02f;
        }

        rb2d.mass = massMultiplier * defaultMass;
        transform.localScale = new Vector3(massMultiplier, massMultiplier, massMultiplier);
    }
}
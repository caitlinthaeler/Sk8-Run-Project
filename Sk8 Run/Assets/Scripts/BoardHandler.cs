using System;
using System.Collections;
using UnityEngine;

public class BoardHandler : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float maxSpeed;

    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private bool onGround = true;

    private Vector2 right = new Vector2(1, 0);
    private Vector2 left = new Vector2(-1, 0);
    private Vector2 up = new Vector2(0, 1);

    [SerializeField]
    private Transform pivot1;

    [SerializeField]
    private Transform pivot2;

    //1 is forward, -1 is backwards
    [SerializeField]
    private int playerDirection;

    [SerializeField]
    private float minRotation;

    [SerializeField]
    private float maxRotation;

    // Start is called before the first frame update
    void Start()
    {
        InitalizeProperties();
        
    }

    private void InitalizeProperties()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerDirection = 1;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get key pressed and return a move
        ArrayList stringCommands = GetInput();

        //perform action on rigidbody according to move
        HandleMoves(stringCommands);
        //Debug.Log("current velocity: " + rb.velocity);
        ClampRotation();
        CheckDirection();
    }

    private void ClampRotation()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        var clampZ = Mathf.Clamp(transform.rotation.z, minRotation, maxRotation);
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, clampZ);
    }

    public ArrayList GetInput()
    {
        // right arrow - accelerate/move forwards
        // up arrow - grab board
        // down arrow - crouch
        // space bar - jump
        ArrayList actions = new ArrayList();
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("moving forwards");
            actions.Add("forwards");
            //return "forwards";
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            actions.Add("backwards");
            //return "backwards";
        }
        if (Input.GetKey(KeyCode.Space))
        {
            actions.Add("jump");
            //return "jump";
        }
        if (rb.velocity == Vector2.zero)
        {
            actions.Add("idle");
            //return "idle";
        }
        return actions;
        
    }

    public void HandleMoves(ArrayList actions)
    {
        // will jump forwards if on downwards inclide or horizontal platform
        // will jump flip backwards if on upwards incline
        foreach (string move in actions)
        {
            if (move == "forwards")
            {
                if (onGround)
                {
                    Vector2 currentVelocity = rb.velocity;
                    if (currentVelocity.magnitude < maxSpeed)
                    {
                        Debug.Log("adding force: "+ right * acceleration);
                        rb.AddForce(right * acceleration, ForceMode2D.Force);
                        Debug.Log("current velocity: " + rb.velocity);
                    }
                    else
                    {
                        rb.velocity = maxSpeed * right;
                    }

                }

            }
            if (move == "backwards")
            {
                if (onGround)
                {
                    Vector2 currentVelocity = rb.velocity;
                    if (currentVelocity.magnitude < maxSpeed)
                    {
                        rb.AddForce(left * acceleration, ForceMode2D.Force);
                    }
                    else
                    {
                        rb.velocity = maxSpeed * left;
                    }

                }

            }
            if (move == "jump")
            {
                if (onGround)
                {
                    Vector2 jumpVector = (rb.velocity*2) + up * jumpForce;
                    Vector3 rotation = transform.rotation.eulerAngles;
                   // rb.AddForce(jumpVector, ForceMode2D.Force);

                    if (playerDirection == 1)
                    {
                        rb.AddForceAtPosition(jumpVector, pivot2.position, ForceMode2D.Impulse);
                        //rb.AddTorque(maxRotation * Mathf.Deg2Rad, ForceMode2D.Impulse);
                        //transform.RotateAround(pivot1.position, new Vector3(0, 0, 1), maxRotation);
                        // transform.rotation = Quaternion.Euler(rotation.x, rotation.y, maxRotation);
                    } else
                    {
                        rb.AddForceAtPosition(jumpVector, pivot1.position, ForceMode2D.Impulse);
                        //rb.AddTorque(minRotation * Mathf.Deg2Rad, ForceMode2D.Impulse);
                        //transform.RotateAround(pivot2.position, new Vector3(0, 0, -1), minRotation);
                        //transform.rotation = Quaternion.Euler(rotation.x, rotation.y, minRotation);

                    }
                    BalanceBoard();
                    Debug.Log("adding force: " + jumpVector);
                    onGround = false;
                }

            }
        }
        
        
        

    }

    private void BalanceBoard()
    {
        
    }

    private void CheckDirection()
    {
        if (rb.velocity.x < 0)
        {
            playerDirection = 0;
        }
        else if (rb.velocity.x > 0)
        {
            playerDirection = 1;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log("tag: "+tag);
        if (tag == "Block" && collision.gameObject.GetComponent<BuildingBlock>().isJumpable())
        {
            onGround = true;
        }
        
    }
    /*
    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log("trigger collided");
        if (tag == "Collectible")
        {
            Destroy(collision.gameObject);
        }
        else if (tag == "Goal")
        {
            collision.gameObject.GetComponent<GoalHandler>().ActivateGoal();
        }
    }*/

}

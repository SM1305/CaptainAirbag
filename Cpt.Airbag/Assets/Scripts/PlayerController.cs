using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody player;
    [Header("Acceleration")]
    public float velocity;
    public float startVelocity;
    public float acceleration;
    public float maxVelocity;
    public float minVelocity = 1f;
    public float brake;
    private float startDrag;
    [Header("Horizontal Movement")]
    public float sideSpeed;
    //public float horizontalSpeed;
    //public float horizontalAcceleration;
    [Header("Vertical Movement")]
    public float jumpHeight;
    public float crouchHeight;
    public bool isGrounded;

    private Vector3 originalScale;
    private Vector3 tempScale;
    private Vector3 tempVelocity;


	void Start ()
    {
        player = GetComponent<Rigidbody>();
        startDrag = player.drag;
        velocity = startVelocity;
        Vector3 originalScale = transform.localScale;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, -Vector3.up, Color.green);
        IsGrounded();

        if ((Input.GetKey(KeyCode.S)))
        {
            player.drag = brake;
        }
        else
        {
            player.drag = startDrag;
            velocity = Mathf.Lerp(velocity, maxVelocity, 0.1f * Time.deltaTime);
        }
        //keep velocity within bounds
        if (velocity >= maxVelocity)
        {
            velocity = maxVelocity;
        }

        //jump
        if ((Input.GetKey(KeyCode.UpArrow) && isGrounded))
        {
            player.AddForce(transform.up * jumpHeight);
        }
        //duck
        if ((Input.GetKey(KeyCode.DownArrow)))
        {
            tempScale = transform.localScale;
            tempScale.y = tempScale.y * crouchHeight;
            transform.localScale = tempScale;
        }
        else
        {
            //transform.localScale = originalScale;
        }

        //left
        if (Input.GetKey(KeyCode.A))
        {
            //player.AddForce((transform.right * -sideSpeed) * Time.deltaTime, ForceMode.Impulse);
            player.AddForce((transform.right * (-velocity /6)) * Time.deltaTime, ForceMode.Impulse);

            //transform.Translate(Vector3.right * -sideSpeed);

            //horizontalSpeed -= horizontalAcceleration * Time.deltaTime;

            //tempVelocity = player.velocity;
            //tempVelocity.x -= horizontalAcceleration * Time.deltaTime;
            //player.velocity = tempVelocity;
        }
        //right
        else if (Input.GetKey(KeyCode.D))
        {
            //player.AddForce((transform.right * sideSpeed) * Time.deltaTime, ForceMode.Impulse);
            player.AddForce((transform.right * (velocity /6)) * Time.deltaTime, ForceMode.Impulse);

            //transform.Translate(Vector3.right * sideSpeed);

            //horizontalSpeed += horizontalAcceleration * Time.deltaTime;

            //tempVelocity = player.velocity;
            //tempVelocity.x += horizontalAcceleration * Time.deltaTime;
            //player.velocity = tempVelocity;
        }



        //player acceleration
        player.AddForce(transform.forward * velocity * Time.deltaTime);
    }

    void FixedUpdate ()
    {
        ////player acceleration
        //player.AddForce(transform.forward * velocity * Time.deltaTime);

        ////horizontal movement
        //if (horizontalSpeed < 0)
        //{
        //    player.AddForce(new Vector3(-1, 0, 0) * (-horizontalSpeed * Time.deltaTime));
        //}
        //if (horizontalSpeed > 0)
        //{
        //    player.AddForce(transform.right * (horizontalSpeed * Time.deltaTime));
        //}
    }


    bool IsGrounded()
    {
        if (!isGrounded)
        {
            return true;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.5f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

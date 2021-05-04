using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // movement
    public float speedV;
    public float speedH;

    // jump
    public float accJump;
    public float gravity;

    // flags
    public bool isGrounded;
    public bool isJumping;
    private bool isLocked;

    // Update is called once per frame
    void Update()
    {
        // if not locked, process moving
        if (!isLocked)
        {
            // keyboard input
            float moveV;
            float moveH;

            // set move V
            if (Input.GetKey(KeyCode.W))
            {
                moveV = 1f * Time.deltaTime * speedV;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveV = -1f * Time.deltaTime * speedV;
            }
            else
            {
                moveV = 0f;
            }

            // set move H
            if (Input.GetKey(KeyCode.A))
            {
                moveH = -1f * Time.deltaTime * speedH;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveH = 1f * Time.deltaTime * speedH;
            }
            else
            {
                moveH = 0f;
            }

            // jump
            if (Input.GetKey(KeyCode.Space))
            {
                if (isGrounded && !isJumping)
                {
                    isJumping = true;
                    AccelerateJump();
                }
            }
            else
            {
                isJumping = false;
            }

            // move player
            Vector3 movement = new Vector3(moveH, 0f, moveV);
            transform.Translate(movement);
        }

        // receive artificial gravity
        if (!isGrounded)
        {
            FreeFall();
        }
    }

    // calculate jump ccceleration
    private void AccelerateJump()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0f, accJump, 0f), ForceMode.Impulse);
    }

    // free fall
    private void FreeFall()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0f, -gravity, 0f));
    }

    // lock movement
    public void Lock()
    {
        isLocked = true;
    }

    // unlock movement
    public void Unlock()
    {
        isLocked = false;
    }

    // ground the character
    public void Ground()
    {
        isGrounded = true;
    }

    // hover the character
    public void Hover()
    {
        isGrounded = false;
    }
}

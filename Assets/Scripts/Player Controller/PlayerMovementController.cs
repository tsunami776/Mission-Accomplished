using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // movement
    public float speedV;
    public float speedH;
    public float accMove;
    public float maxSpeedForward;
    public float maxSpeedBackward;

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
                moveV = 1f;
                AccelerateV(moveV * accMove * Time.deltaTime);  
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveV = -1f;
                AccelerateV(moveV * accMove * Time.deltaTime);
            }
            else
            {
                DecelerateV();
            }

            // set move H
            if (Input.GetKey(KeyCode.A))
            {
                moveH = -1f;
                AccelerateH(moveH * accMove * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveH = 1f;
                AccelerateH(moveH * accMove * Time.deltaTime);
            }
            else
            {
                DecelerateH();
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
            Vector3 movement = new Vector3(speedH, 0f, speedV);
            transform.Translate(movement);
        }

        // receive artificial gravity
        if (!isGrounded)
        {
            FreeFall();
        }
    }

    // calculate Vertical Acceleration
    private void AccelerateV(float acceleration)
    {
        speedV += acceleration;
        if (speedV >= maxSpeedForward)
        {
            speedV = maxSpeedForward;
        }
        if (speedV <= maxSpeedBackward)
        {
            speedV = maxSpeedBackward;
        }
    }

    // calculate Horizontal Acceleration
    private void AccelerateH(float acceleration)
    {
        speedH += acceleration;
        if (speedH >= maxSpeedForward)
        {
            speedH = maxSpeedForward;
        }
        if (speedH <= maxSpeedBackward)
        {
            speedH = maxSpeedBackward;
        }
    }

    // reset acceleration V
    private void DecelerateV()
    {
        speedV = 0f;
    }

    // reset acceleration H
    private void DecelerateH()
    {
        speedH = 0f;
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

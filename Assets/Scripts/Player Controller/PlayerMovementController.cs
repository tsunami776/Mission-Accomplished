using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementController : MonoBehaviour
{
    public float speedV;
    public float speedH;
    public float speedjump;
    public float acceleration;
    public float maxSpeedForward;
    public float maxSpeedBackward;

    private bool isLocked;

    //private void Start()
    //{
    //SceneManager.LoadScene("Ben'sScene", LoadSceneMode.Additive);
    //}

    // Update is called once per frame
    void Update()
    {
        // if not locked, process moving
        if (!isLocked)
        {
            // keyboard input
            float moveV;
            float moveH;
            float jump;

            // set move V
            if (Input.GetKey(KeyCode.W))
            {
                moveV = 1f;
                AccelerateV(moveV * acceleration);  
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveV = -1f;
                AccelerateV(moveV * acceleration);
            }
            else
            {
                DecelerateV();
            }

            // jump
            if (Input.GetKey(KeyCode.Space))
            {
                jump = 5f;
                AccelerateJump(jump * acceleration);
            }
            else {
                DecelerateJump();
            }

            // set move H
            if (Input.GetKey(KeyCode.A))
            {
                moveH = -1f;
                AccelerateH(moveH * acceleration);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveH = 1f;
                AccelerateH(moveH * acceleration);
            }
            else
            {
                DecelerateH();
            }

            // move player               
            Vector3 movement = new Vector3(speedH, speedjump, speedV);
            transform.Translate(movement);
        } 
    }

    // calculate Vertical Acceleration
    private void AccelerateV(float acceleration)
    {
        speedV += acceleration * Time.deltaTime;
        if (speedV >= maxSpeedForward)
        {
            speedV = maxSpeedForward;
        }
        if (speedV <= maxSpeedBackward)
        {
            speedV = maxSpeedBackward;
        }
    }

    // calculate jump
    private void AccelerateJump(float acceleration)
    {
        speedjump += acceleration * Time.deltaTime;
        if (speedjump >= maxSpeedForward)
        {
            speedjump = maxSpeedForward;
        }
        if (speedjump <= maxSpeedBackward)
        {
            speedjump = maxSpeedBackward;
        }
    }

    // calculate Horizontal Acceleration
    private void AccelerateH(float acceleration)
    {
        speedH += acceleration * Time.deltaTime;
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

    // reset acceleration jump
    private void DecelerateJump()
    {
        speedjump = 0f;
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
}

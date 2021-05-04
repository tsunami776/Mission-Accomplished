using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // animator
    private Animator animator;

    // movement
    public float speedV;
    public float speedH;

    // jump
    public float accJump;
    public float gravity;

    // flags
    public bool isMoving;
    public bool isGrounded;
    public bool isJumping;
    private bool isLocked;

    // moving audio
    [SerializeField] private AudioSource footstepSound;

    // avatar
    [SerializeField] private GameObject avatar;
    [SerializeField] private Transform lookTarget;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

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

            // rotate avatar
            float rotationDeg = 0f;
            if (moveH * moveH + moveV * moveV != 0)
            {
                rotationDeg = Mathf.Asin(moveH / Mathf.Sqrt(moveH * moveH + moveV * moveV)) * Mathf.Rad2Deg;
                if (moveV < 0 && rotationDeg > 0)
                {
                    rotationDeg += 90f;
                }
                if (moveV < 0 && rotationDeg < 0)
                {
                    rotationDeg -= 90f;
                }
                if (moveV < 0 && moveH == 0)
                {
                    rotationDeg = 180f;
                }
                lookTarget.parent.localEulerAngles = new Vector3(0f, rotationDeg, 0f);
            }
            Vector3 lTargetDir = lookTarget.position - avatar.transform.position;
            lTargetDir.y = 0.0f;
            avatar.transform.rotation = Quaternion.RotateTowards(avatar.transform.rotation, Quaternion.LookRotation(lTargetDir), Config.MODIFIER_ROTATION_SPEED_SMOOTH);

            // set animator and audio
            if (Mathf.Abs(moveH) > 0.01f || Mathf.Abs(moveV) > 0.01f)
            {
                isMoving = true;
                animator.SetBool("isMoving", true);
                if (!footstepSound.isPlaying)
                {
                    footstepSound.Play();
                }
            }
            else
            {
                isMoving = true;
                animator.SetBool("isMoving", false);
                footstepSound.Stop();
            }
        }

        // receive artificial gravity
        if (!isGrounded)
        {
            FreeFall();
        }
    }

    // rotate avatar coroutine
    private void smoothRotateAvatar(float rotationDeg)
    {
        float smoothRotation = avatar.transform.localEulerAngles.y;
        while (!(rotationDeg - 1f < avatar.transform.localEulerAngles.y && avatar.transform.localEulerAngles.y < rotationDeg + 1f))
        {
            avatar.transform.localEulerAngles = new Vector3(0f, smoothRotation, 0f);
            if (rotationDeg > 0)
            {
                smoothRotation += 1 / rotationDeg;
                if (smoothRotation > 180f)
                {
                    smoothRotation -= 180f;
                }
            }
            else if (rotationDeg < 0)
            {
                smoothRotation -= 1 / rotationDeg;
                if (smoothRotation < -180f)
                {
                    smoothRotation += 180f;
                }
            }
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

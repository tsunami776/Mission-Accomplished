using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    // references
    [SerializeField] private GameObject avatar;
    [SerializeField] private Transform lookTarget;
    [SerializeField] private GunController gun;
    [SerializeField] private AudioSource footstepSound;
    [SerializeField] private Animator camAnimator;
    private Animator characterAnimator;

    // values
    public float speedV;
    public float speedH;
    public bool isMoving;
    public bool isSpriting;
    public bool isGrounded;
    public bool isJumping;
    public float accJump;
    public float gravity;
    private bool isLocked;


    private void Start()
    {
        characterAnimator = GetComponent<Animator>();
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
                if (!gun.isShooting && !GetComponent<PlayerLookController>().isAiming)
                {
                    // if player not grounded, play cam move animation
                    if (isGrounded)
                    {
                        camAnimator.SetBool("isMoving", true);

                        // sprite cam
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            isSpriting = true;
                            moveV *= Config.MODIFIER_SPRITE;
                            camAnimator.SetBool("isSpriting", true);
                        }
                        else
                        {
                            isSpriting = false;
                            camAnimator.SetBool("isSpriting", false);
                        }
                    }
                    else
                    {
                        camAnimator.SetBool("isMoving", false);
                        camAnimator.SetBool("isSpriting", false);
                    }
                }
                else
                {
                    camAnimator.SetBool("isMoving", false);
                    isSpriting = false;
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveV = -1f * Time.deltaTime * speedV;
            }
            else
            {
                moveV = 0f;
                camAnimator.SetBool("isMoving", false);
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

            // aim/shoot slow down moving
            if (GetComponent<PlayerLookController>().isAiming)
            {
                moveV *= Config.MODIFIER_GUN_AIM_SLOWDOWN;
                moveH *= Config.MODIFIER_GUN_AIM_SLOWDOWN;
            }
            if (gun.isShooting)
            {
                moveV *= Config.MODIFIER_GUN_SHOOT_SLOWDOWN;
                moveH *= Config.MODIFIER_GUN_SHOOT_SLOWDOWN;
            }

            // move player
            Vector3 movement = new Vector3(moveH, 0f, moveV);
            transform.Translate(movement);

            // play gun move animation
            if (movement.magnitude > 0)
            {
                isMoving = true;
                if (!gun.isShooting)
                {
                    gun.GunMove(isSpriting);
                }
            }
            else
            {
                isMoving = false;
                if (!gun.isShooting)
                {
                    gun.GunStatic();
                }
            }

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
                characterAnimator.SetBool("isMoving", true);
                if (!footstepSound.isPlaying)
                {
                    footstepSound.Play();
                }
            }
            else
            {
                characterAnimator.SetBool("isMoving", false);
                footstepSound.Stop();
            }
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

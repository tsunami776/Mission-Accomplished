using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    //footstep sound
    public AudioSource footstepSound;
    public Rigidbody Player;
    private bool ismoving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.velocity.x != 0 || Player.velocity.y != 0 || Player.velocity.z != 0)
            {
                ismoving = true;
            }else{
                ismoving = false;
            }
        
        if (ismoving){
            if(!footstepSound.isPlaying){
                footstepSound.Play();
            }
        }else{
                footstepSound.Stop();
        }
    }
}

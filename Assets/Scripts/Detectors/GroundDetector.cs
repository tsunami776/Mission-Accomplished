﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private PlayerMovementController PMC;
    private Vector3 down;

    private void Start()
    {
        PMC = GetComponentInParent<PlayerMovementController>();
        down = new Vector3(0f, -1f, 0f);
    }

    /*void FixedUpdate()
    {
        // Does the ray intersect any objects in layer 8
        RaycastHit hit;
        if (Physics.Raycast(transform.position, down, out hit, Config.INTERACTION_RANGE))
        {
            Debug.DrawRay(transform.position, down * hit.distance, Color.yellow);

            print(hit.distance);
        }
        else
        {

        }
    }*/

    // detect if character is grounded
    private void OnTriggerEnter(Collider other)
    {
        PMC.Ground();
    }

    private void OnTriggerExit(Collider other)
    {
        PMC.Hover();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private PlayerMovementController PMC;

    private void Start()
    {
        PMC = GetComponentInParent<PlayerMovementController>();  
    }

    // detect if character is grounded
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            PMC.Ground();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            PMC.Hover();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    public Transform playerCam;
    public GameObject interactableObj;

    // Update is called once per fixed delta time
    void FixedUpdate()
    {
        // Only detect interactable objects in layer 8:"Interactable"
        int layerMask = 1 << 8;
        
        // Does the ray intersect any objects in layer 8
        RaycastHit hit;
        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, 8f, layerMask))
        {
            // Debug.DrawRay(playerCam.position, playerCam.forward * hit.distance, Color.yellow);

            // select hit object
            interactableObj = hit.collider.gameObject;

            // Highlight the outline & show notification text
            if (interactableObj != null)
            {
                interactableObj.GetComponent<Outline>().OutlineColor = new Color(0f,200f,0f);
                interactableObj.GetComponent<ShowNotification>().ShowText();
            }
        }
        else
        {
            // Set outline back to default color & hide notification text
            if (interactableObj != null)
            {
                interactableObj.GetComponent<Outline>().OutlineColor = new Color(255f,255f,255f);
                interactableObj.GetComponent<ShowNotification>().HideText();
                interactableObj = null;
            }
        }
    }
}
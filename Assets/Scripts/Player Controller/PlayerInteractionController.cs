using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{
    public Transform playerCam;
    public RawImage crossHair;
    public GameObject interactableObj;

    // Update is called once per fixed delta time
    void FixedUpdate()
    {
        // Only detect interactable objects in layer 8:"Interactable"
        int layerMask = 1 << Config.LAYER_INDEX_INTERACTABLE;
        
        // Does the ray intersect any objects in layer 8
        RaycastHit hit;
        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, Config.INTERACTION_RANGE, layerMask))
        {
            // Debug.DrawRay(playerCam.position, playerCam.forward * hit.distance, Color.yellow);

            // Highlight Crosshair color
            crossHair.color = Config.INTERACTION_COLOR_SELECTED;

            // select hit object
            interactableObj = hit.collider.gameObject;

            // Highlight the outline & show notification text
            if (interactableObj != null)
            {
                interactableObj.GetComponent<Outline>().OutlineColor = Config.INTERACTION_COLOR_SELECTED;
                interactableObj.GetComponent<ShowNotification>().ShowText();
            }
        }
        else
        {
            // Reset Crosshair color
            crossHair.color = Config.INTERACTION_COLOR_DEFAULT;

            // Reset outline color & hide notification text
            if (interactableObj != null)
            {
                interactableObj.GetComponent<Outline>().OutlineColor = Config.INTERACTION_COLOR_DEFAULT;
                interactableObj.GetComponent<ShowNotification>().HideText();
                interactableObj = null;
            }
        }
    }
}
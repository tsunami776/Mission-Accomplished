using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{
    public Transform playerCam;
    public RawImage crossHair;
    public GameObject interactableObj;
    public GameObject missionView;

    // Only detect interactable objects in layer 8:"Interactable"
    private int layerMask = 1 << Config.LAYER_INDEX_INTERACTABLE;

    // Update is called once per fixed delta time
    void FixedUpdate()
    {
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

                // if player hit the interaction key, default='E'
                if (Input.GetKey(KeyCode.E))
                {
                    // if it is Mission Obj: open the mission view
                    if (interactableObj.CompareTag("MissionObj"))
                    {
                        GameController.GC.UpdateMissionState_Player();
                        missionView.SetActive(true);
                        GetComponent<PlayerMovementController>().Lock();
                        GetComponent<PlayerLookController>().Lock();
                    }
                }
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
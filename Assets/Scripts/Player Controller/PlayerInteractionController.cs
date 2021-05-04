using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{
    [HideInInspector] public float interactionRange;
    public Transform playerCam;
    public RawImage crossHair;
    public GameObject interactableObj;
    public GameObject missionView;

    // Only detect interactable objects in layer 8:"Interactable"
    private int layerMask = 1 << Config.LAYER_INDEX_INTERACTABLE;

    private void Start()
    {
        interactionRange = Config.INTERACTION_RANGE_FPS;
    }

    // Update is called once per fixed delta time
    void FixedUpdate()
    {
        // Does the ray intersect any objects in layer 8
        RaycastHit hit;
        //Debug.DrawRay(playerCam.position, playerCam.forward * interactionRange, Color.yellow);


        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, interactionRange, layerMask))
        {
            // Highlight Crosshair color
            crossHair.color = Config.INTERACTION_COLOR_SELECTED;

            // select hit object
            interactableObj = hit.collider.gameObject;

            // Highlight the outline & show notification text
            if (interactableObj != null)
            {
                interactableObj.GetComponent<Outline>().OutlineColor = Config.INTERACTION_COLOR_SELECTED;
                if (interactableObj.GetComponent<ShowNotification>() != null)
                {
                    interactableObj.GetComponent<ShowNotification>().ShowText();
                }

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
                if (interactableObj.GetComponent<ShowNotification>() != null)
                {
                    interactableObj.GetComponent<ShowNotification>().HideText();
                }
                interactableObj = null;
            }
        }
    }
}
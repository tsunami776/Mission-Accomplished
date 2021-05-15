using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private GunController gun;
    [SerializeField] private GameObject gunParts;
    [SerializeField] private GameObject[] toolSlots;

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

        // detect and interact with objs
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

        // shoot
        if (Input.GetMouseButton(0))
        {
            gun.GunShoot();
        }

        // mid aim
        if (Input.GetMouseButton(1))
        {
            crossHair.gameObject.SetActive(false);
            gun.GunAim_FPS();
        }
        else 
        {
            crossHair.gameObject.SetActive(true);
            gun.GunDisAim_FPS();
        }

        // reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.GunReload();
        }


        // switch gun in
        if (Input.GetKey(KeyCode.Alpha2))
        {
            toolSlots[1].SetActive(true);
            for (int i = 0; i < toolSlots.Length; i++)
            {
                if (i == 1)
                {
                    continue;
                }
                toolSlots[i].SetActive(false);
            }
            gun.GunSwitchIn();
        }

        // switch gun out
        if (Input.GetKey(KeyCode.Alpha1))
        {
            toolSlots[0].SetActive(true);
            for (int i = 0; i < toolSlots.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                toolSlots[i].SetActive(false);
            }
            gun.GunSwitchOut();
        }
    }
}
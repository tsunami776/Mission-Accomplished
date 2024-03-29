﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionController : MonoBehaviour
{
    public GunController gun;
    public GameObject gunParts;
    public GameObject[] toolSlots;

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
                        missionView.SetActive(true);
                        GetComponent<PlayerMovementController>().Lock();
                        GetComponent<PlayerLookController>().Lock();
                    }

                    // if it is an NPC
                    if (interactableObj.CompareTag("NPC"))
                    {
                        GameController.GC.UpdateMissionState_Player();
                    }

                    // if it is a portal
                    if (interactableObj.CompareTag("Portal"))
                    {
                        interactableObj.GetComponent<HelicopterPortal>().Teleport();
                    }

                    // if it is a portal
                    if (interactableObj.CompareTag("AmmoSupply") && GetComponent<PlayerWeaponController>().totalAmmoRemains[0] < Config.DEFAULT_MAX_TOTAL_AMMO[0])
                    {
                        var loadAmout = Config.DEFAULT_MAX_ONE_CLIP[0];
                        if (Config.DEFAULT_MAX_TOTAL_AMMO[0] - GetComponent<PlayerWeaponController>().totalAmmoRemains[0] < Config.DEFAULT_MAX_ONE_CLIP[0])
                        {
                            loadAmout = Config.DEFAULT_MAX_TOTAL_AMMO[0] - GetComponent<PlayerWeaponController>().totalAmmoRemains[0];

                        }
                        GetComponent<PlayerWeaponController>().AddTotalAmmoRemain(0, loadAmout);
                        interactableObj.GetComponent<AudioSource>().Play();
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

        // check if during the day
        if (!GameController.GC.timer.clockLock)
        {
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

            /*// switch gun in
            if (Input.GetKey(KeyCode.Alpha2) && !GetComponent<PlayerLookController>().isTPS)
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
            if (Input.GetKey(KeyCode.Alpha1) && !GetComponent<PlayerLookController>().isTPS)
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
            }*/
        }
    }
}
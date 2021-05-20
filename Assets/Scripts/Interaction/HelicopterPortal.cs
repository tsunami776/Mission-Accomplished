using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterPortal : MonoBehaviour
{
    enum Portal { Entry, Exit }
    [SerializeField] private Portal portal;
    [SerializeField] private Transform entryPos;
    [SerializeField] private Transform exitPos;

    private PlayerInteractionController PIC;
    private PlayerLookController PLC;

    private void Start()
    {
        PIC = GameController.GC.player.GetComponent<PlayerInteractionController>();
        PLC = GameController.GC.player.GetComponent<PlayerLookController>();
    }

    public void Teleport()
    {
        // to the shooting range
        if (portal == Portal.Entry)
        {
            // teleport
            GameController.GC.player.transform.position = exitPos.position;

            // switch gun in
            if (!PLC.GetComponent<PlayerLookController>().isTPS)
            {
                PIC.toolSlots[1].SetActive(true);
                for (int i = 0; i < PIC.toolSlots.Length; i++)
                {
                    if (i == 1)
                    {
                        continue;
                    }
                    PIC.toolSlots[i].SetActive(false);
                }
                PIC.gun.GunSwitchIn();
            }
        }

        // to the city
        if (portal == Portal.Exit)
        {
            // teleport
            GameController.GC.player.transform.position = entryPos.position;

            // switch gun out
            if (!PLC.GetComponent<PlayerLookController>().isTPS)
            {
                PIC.toolSlots[0].SetActive(true);
                for (int i = 0; i < PIC.toolSlots.Length; i++)
                {
                    if (i == 0)
                    {
                        continue;
                    }
                    PIC.toolSlots[i].SetActive(false);
                }
                PIC.gun.GunSwitchOut();
            }
        }
    }
}

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
    private IEnumerator GunSwitchInCo;
    private IEnumerator GunSwitchOutCo;

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
            if (PLC.GetComponent<PlayerLookController>().isTPS)
            {
                PLC.GetComponent<PlayerLookController>().isManuallyViewSwitching = true;
            }

            PIC.toolSlots[1].SetActive(true);
            for (int i = 0; i < PIC.toolSlots.Length; i++)
            {
                if (i == 1)
                {
                    continue;
                }
                PIC.toolSlots[i].SetActive(false);
            }

            if (GunSwitchInCo == null)
            {
                GunSwitchInCo = SwitchGunIn();
                GunSwitchOutCo = null;
            }
            StartCoroutine(GunSwitchInCo);
        }

        // to the city
        if (portal == Portal.Exit)
        {
            // teleport
            GameController.GC.player.transform.position = entryPos.position;

            // switch gun out
            if (PLC.GetComponent<PlayerLookController>().isTPS)
            {
                PLC.GetComponent<PlayerLookController>().isManuallyViewSwitching = true;
            }

            PIC.toolSlots[0].SetActive(true);
            for (int i = 0; i < PIC.toolSlots.Length; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                PIC.toolSlots[i].SetActive(false);
            }

            if (GunSwitchOutCo == null)
            {
                GunSwitchOutCo = SwitchGunOut();
                GunSwitchInCo = null;
            }
            StartCoroutine(GunSwitchOutCo);
        }
    }

    IEnumerator SwitchGunIn()
    {
        while (PLC.GetComponent<PlayerLookController>().isTPS)
        {
            yield return null;
        }
        PIC.gun.GunSwitchIn();
        GunSwitchInCo = null;
    }

    IEnumerator SwitchGunOut()
    {
        while (PLC.GetComponent<PlayerLookController>().isTPS)
        {
            yield return null;
        }
        PIC.gun.GunSwitchOut();
        GunSwitchOutCo = null;
    }
}

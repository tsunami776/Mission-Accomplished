using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPartsController : MonoBehaviour
{
    [SerializeField] private GunController gun;

    private void OnEnable()
    {
        gun.GunSwitchIn();
    }

    private void OnDisable()
    {
        gun.GunSwitchOut();
    }
}

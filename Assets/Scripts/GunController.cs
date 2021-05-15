using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    // References
    [SerializeField] private PlayerLookController PLC;
    [SerializeField] private PlayerWeaponController PWC;
    [SerializeField] private GameObject gunFire1;
    [SerializeField] private GameObject gunFire2;
    [SerializeField] private Animator gunPartsAnimator;
    [SerializeField] private AudioSource fireAudio;
    [SerializeField] private AudioSource loadAudio;
    [SerializeField] private AudioSource emptyAudio;
    [SerializeField] private AudioSource reloadAudio;
    [SerializeField] private GameObject fireLight;
    [SerializeField] private GameObject bulletShell;

    // Values
    public bool isLocked = true;
    public bool isShooting;

    public void GunShoot()
    {
        if (!isLocked && !PLC.isTPS)
        {
            if (!isShooting)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        // flag
        isShooting = true;

        // check if the gun is reloading
        if (!PWC.isReloading)
        {
            // shoot ammo from clip, if empty, do nothing but play the empty audio
            if (!PWC.SubtractClipRemain(0, 1))
            {
                // if clip empty
                // try reload the ammo
                if (!PWC.Reload(0))
                {
                    // if empty, do nothing but play the empty audio
                    emptyAudio.Play();
                    yield return new WaitForSecondsRealtime(Config.TIME_GUN_EMPTY_INTERVAL);
                }
                else
                {
                    // if not empty, play the reload animation and audio
                    gunPartsAnimator.SetTrigger("isReloading");
                    reloadAudio.Play();
                }
            }
            else
            {
                // if not empty, shoot 
                // gun fire
                float alpha = Random.Range(Config.MODIFIER_GUN_FIRE_ALPHA, 1f);
                float rotate = 90f;
                float spread1 = Random.Range(1f, 1.7f);
                float spread2 = Random.Range(1f, 1.7f);
                /*1*/
                gunFire1.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
                gunFire1.transform.Rotate(new Vector3(0f, 0f, rotate));
                gunFire1.transform.localScale = new Vector3(spread1, spread1, spread1);
                gunFire1.SetActive(true);
                /*2*/
                gunFire2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - alpha);
                gunFire2.transform.Rotate(new Vector3(0f, 0f, rotate));
                gunFire2.transform.localScale = new Vector3(spread2, spread2, spread2);
                gunFire2.SetActive(true);

                // gun recoil
                StartCoroutine(Recoil());

                // fire light
                fireLight.SetActive(true);

                // fire audio
                fireAudio.Play();

                // throw shell
                var newShell = Instantiate(bulletShell, bulletShell.transform.parent);
                newShell.gameObject.SetActive(true);
                newShell.transform.parent = transform.root.parent;


                yield return new WaitForSecondsRealtime(Config.TIME_GUN_SHOOT_INTERVAL);


                // reset
                gunFire1.SetActive(false);
                gunFire2.SetActive(false);
                fireLight.SetActive(false);
            }
        }

        // reset flag
        isShooting = false;
    }

    IEnumerator Recoil()
    {
        float move = 0f;
        gunPartsAnimator.Play("GunRecoilAnimation");

        // recoil camera
        while (move < Config.PHYSICS_CAM_SHOOT_RECOIL)
        {
            move += Config.PHYSICS_CAM_SHOOT_RECOIL / Config.TIME_CAM_SHOOT_RECOIL_TIMESTEP;
            PLC.RecoilVertical(move);
            yield return new WaitForFixedUpdate();
        }
        move = 0;

        // restore camera
        while (move < Config.PHYSICS_CAM_SHOOT_RECOIL)
        {
            move += Config.PHYSICS_CAM_SHOOT_RECOIL / Config.TIME_CAM_SHOOT_RECOIL_TIMESTEP * Config.PHYSICS_GUN_SHOOT_RECOIL_RESTORE;
            PLC.RecoilVertical(-move);
            yield return new WaitForFixedUpdate();
        }
    }

    // gun moving animation 
    public void GunMove(bool isSpriting)
    {
        if (!isShooting && !PLC.isAiming)
        {
            gunPartsAnimator.SetBool("isMoving", true);

            // sprite
            if (isSpriting)
            {
                gunPartsAnimator.SetBool("isSpriting", true);
            }
            else
            {
                gunPartsAnimator.SetBool("isSpriting", false);
            }
        }
        else
        {
            gunPartsAnimator.SetBool("isMoving", false);
            gunPartsAnimator.SetBool("isSpriting", false);
        }
    }

    // gun stay static animation
    public void GunStatic()
    {
        gunPartsAnimator.SetBool("isMoving", false);
    }

    // gun aim
    public void GunAim_FPS()
    {
        if (!isLocked && !PLC.isTPS)
        {
            PLC.Aiming(true);
            GetComponent<Animator>().SetBool("isAiming", true);
        }
    }

    // gun dis-aim
    public void GunDisAim_FPS()
    {
        if (!isLocked && !PLC.isTPS)
        {
            PLC.Aiming(false);
            GetComponent<Animator>().SetBool("isAiming", false);
        }
    }

    // gun reload
    public void GunReload()
    {
        // check if already full
        if (PWC.ClipRemains[0] <= Config.DEFAULT_MAX_ONE_CLIP[0])
        {
            // try reload the ammo
            if (!PWC.Reload(0))
            {
                // if empty, do nothing but play the empty audio
                emptyAudio.Play();
                StartCoroutine(WaitGunEmptyInterval());
            }
            else
            {
                // if not empty, play the reload animation and audio
                gunPartsAnimator.SetTrigger("isReloading");
                reloadAudio.Play();
            }
        }
    }

    IEnumerator WaitGunEmptyInterval()
    {
        yield return new WaitForSecondsRealtime(Config.TIME_GUN_EMPTY_INTERVAL);
    }

    // switch gun in animation
    public void GunSwitchIn()
    {
        if (isLocked && !PLC.isTPS)
        {
            isLocked = false;
            gunPartsAnimator.SetBool("isSwitchOut", false);
            gunPartsAnimator.SetBool("isSwitchIn", true);
            loadAudio.Play();
        }
    }

    // switch gun out animation
    public void GunSwitchOut()
    {
        if (!isLocked && !PLC.isTPS)
        {
            isLocked = true;
            gunPartsAnimator.Play("GunIn");
            gunPartsAnimator.SetBool("isMoving", false);
            gunPartsAnimator.SetBool("isSwitchOut", true);
            gunPartsAnimator.SetBool("isSwitchIn", false);
        }
    }
}

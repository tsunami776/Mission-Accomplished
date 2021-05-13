using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponController : MonoBehaviour
{
    // references
    [SerializeField] private Text[] clipTexts;
    [SerializeField] private Text[] totalAmmoTexts;
    [SerializeField] private Image[] weaponIcons;

    // values
    public bool isReloading;
    private int[] ClipRemains;
    private int[] totalAmmoRemains;
    private IEnumerator coroutineBuffer_clip;
    private IEnumerator coroutineBuffer_totalAmmo;

    private void Start()
    {
        ClipRemains = new int[] { 30 };
        totalAmmoRemains = new int[] { 90 };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < clipTexts.Length; i++)
        {
            clipTexts[i].text = ClipRemains[i].ToString();
            totalAmmoTexts[i].text = totalAmmoRemains[i].ToString();
        }
    }

    // add ammo to the current clip
    public bool AddClipRemain(int whichWeapon, int changeAmount)
    {
        // return false if the clip exceeds the max
        if (ClipRemains[whichWeapon] + changeAmount > Config.DEFAULT_MAX_ONE_CLIP[whichWeapon])
        {
            return false;
        }
        else
        {
            ClipRemains[whichWeapon] += changeAmount;

            // only 1 color change coroutine is running at 1 time
            if (coroutineBuffer_clip != null)
            {
                StopCoroutine(coroutineBuffer_clip);
            }
            coroutineBuffer_clip = ColorDecrease(clipTexts[whichWeapon], Config.MISSION_COLOR_UNLOCKED);
            StartCoroutine(coroutineBuffer_clip);
            return true;
        }
    }

    // take ammo out from the current clip
    public bool SubtractClipRemain(int whichWeapon, int changeAmount)
    {
        // return false if the clip has been empty
        if (ClipRemains[whichWeapon] - changeAmount < 0)
        {
            return false;
        }
        else
        {
            ClipRemains[whichWeapon] -= changeAmount;

            // only 1 color change coroutine is running at 1 time
            if (coroutineBuffer_clip != null)
            {
                StopCoroutine(coroutineBuffer_clip);
            }
            coroutineBuffer_clip = ColorDecrease(clipTexts[whichWeapon], Config.MISSION_COLOR_FAILED);
            StartCoroutine(coroutineBuffer_clip);
            return true;
        }
    }

    // add ammo to the total ammo remain
    public bool AddTotalAmmoRemain(int whichWeapon, int changeAmount)
    {
        // return false if the clip exceeds the max
        if (totalAmmoRemains[whichWeapon] + changeAmount > Config.DEFAULT_MAX_TOTAL_AMMO[whichWeapon])
        {
            return false;
        }
        else
        {
            totalAmmoRemains[whichWeapon] += changeAmount;

            // only 1 color change coroutine is running at 1 time
            if (coroutineBuffer_totalAmmo != null)
            {
                StopCoroutine(coroutineBuffer_totalAmmo);
            }
            coroutineBuffer_totalAmmo = ColorDecrease(totalAmmoTexts[whichWeapon], Config.MISSION_COLOR_UNLOCKED);
            StartCoroutine(coroutineBuffer_totalAmmo);
            return true;
        }
    }

    // take ammo out from the total ammo remain
    public bool SubtractTotalAmmoRemain(int whichWeapon, int changeAmount)
    {
        // return false if the clip has been empty
        if (totalAmmoRemains[whichWeapon] - changeAmount < 0)
        {
            return false;
        }
        else
        {
            totalAmmoRemains[whichWeapon] -= changeAmount;

            // only 1 color change coroutine is running at 1 time
            if (coroutineBuffer_totalAmmo != null)
            {
                StopCoroutine(coroutineBuffer_totalAmmo);
            }
            coroutineBuffer_totalAmmo = ColorDecrease(totalAmmoTexts[whichWeapon], Config.MISSION_COLOR_FAILED);
            StartCoroutine(coroutineBuffer_totalAmmo);
            return true;
        }
    }

    IEnumerator ColorDecrease(Text whichText, Color color)
    {
        // intialization
        float countTime = Config.TIME_COLOR_DECREASE / Time.fixedDeltaTime;
        float colorDelta_r = (Color.white.r - color.r) / countTime;
        float colorDelta_g = (Color.white.g - color.g) / countTime;
        float colorDelta_b = (Color.white.b - color.b) / countTime;

        // change color
        whichText.color = color;

        // decreasing
        float counter = 0;
        while (counter < Config.TIME_COLOR_DECREASE)
        {
            counter += Time.fixedDeltaTime;
            whichText.color = new Color(whichText.color.r + colorDelta_r, whichText.color.g + colorDelta_g, whichText.color.b + colorDelta_b);
            yield return new WaitForFixedUpdate();
        }

        // return to initial color
        whichText.color = Color.white;
    }

    // load ammo from total to the current clip
    public bool Reload(int whichWeapon)
    {
        isReloading = true;

        // identify the load amount
        int loadAmount = Config.DEFAULT_MAX_ONE_CLIP[whichWeapon];
        if (ClipRemains[whichWeapon] > 0 && ClipRemains[whichWeapon] <= Config.DEFAULT_MAX_ONE_CLIP[whichWeapon])
        {
            loadAmount -= ClipRemains[whichWeapon];
        }
        if (loadAmount > totalAmmoRemains[whichWeapon])
        {
            loadAmount = totalAmmoRemains[whichWeapon];
        }

        // reload ammo from pool to clip
        if (loadAmount > 0)
        {
            StartCoroutine(ReloadProcess(whichWeapon, loadAmount));
            return true;
        }
        else
        {
            isReloading = false;
            return false;
        }
    }

    IEnumerator ReloadProcess(int whichWeapon, int loadAmount)
    {
        yield return new WaitForSecondsRealtime(Config.TIME_GUN_RELOAD);
        SubtractTotalAmmoRemain(whichWeapon, loadAmount);
        AddClipRemain(whichWeapon, loadAmount);
        isReloading = false;
    }
}

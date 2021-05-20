using System.Collections;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    // References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    [SerializeField] private GameObject canvas_Interation;
    [SerializeField] private GameObject canvas_Management;
    [SerializeField] private GameObject canvas_Transition;
    [SerializeField] private GameObject playerCam_Main;
    [SerializeField] private GameObject playerCam_MiniMap;
    [SerializeField] private GameObject[] transitionCams;

    // Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    void Start()
    {
        // day time offset
        TimeOfDay = -Config.TIME_ONEDAY * 0.2f;
    }

    public void UpdateTime(float change)
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            //(Replace with a reference to the game time)
            TimeOfDay += change;
            TimeOfDay %= Config.TIME_ONEDAY; // mod 1 day time
            UpdateLighting(-TimeOfDay / Config.TIME_ONEDAY);
        }
        else
        {
            UpdateLighting(-TimeOfDay / Config.TIME_ONEDAY);
        }
    }

    public void DayTransition()
    {
        StartCoroutine(transCharacter());
    }

    IEnumerator transCharacter()
    {
        canvas_Transition.SetActive(true);
        canvas_Transition.GetComponent<TransitionPlayer>().TransAnime_Forward();
        yield return new WaitForSecondsRealtime(Config.TIME_ANIMATION_TRANSITION_DELAY);
        UpdateTime(Config.TIME_DAY_TRANSITION_OFFSET);
        StartCoroutine(transCams());
    }

    IEnumerator transCams()
    {
        // freeze the character
        GameController.GC.player.GetComponent<PlayerMovementController>().Lock();
        GameController.GC.player.GetComponent<PlayerLookController>().Lock();

        // turn off the player character UI
        canvas_Interation.SetActive(false);
        canvas_Management.SetActive(false);
        playerCam_Main.SetActive(false);
        playerCam_MiniMap.SetActive(false);

        // turn on one of the random transition cam
        int index = Random.Range(0, transitionCams.Length);
        transitionCams[index].SetActive(true);


        /* 
         * wait for some time
         */
        yield return new WaitForSecondsRealtime(Config.TIME_ANIMATION_TRANSCAM);
        /* 
         * wait for some time
         */


        // reset all player UI
        transitionCams[index].GetComponentInChildren<TransitionCam>().TransAnime_Forward(canvas_Transition);
        yield return new WaitForSecondsRealtime(Config.TIME_ANIMATION_TRANSITION_DELAY);
        TimeOfDay = -Config.TIME_ONEDAY * 0.2f;
        canvas_Interation.SetActive(true);
        canvas_Management.SetActive(true);
        playerCam_Main.SetActive(true);
        playerCam_MiniMap.SetActive(true);

        // respawn the player
        GameController.GC.player.transform.position = GameController.GC.spawn.position;
        GameController.GC.player.transform.rotation= GameController.GC.spawn.rotation;

        // restore player control
        GameController.GC.player.GetComponent<PlayerMovementController>().Unlock();
        GameController.GC.player.GetComponent<PlayerLookController>().Unlock();
    }

    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f + Config.OFFSET_SUNRISE_ANGLE, 170f, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
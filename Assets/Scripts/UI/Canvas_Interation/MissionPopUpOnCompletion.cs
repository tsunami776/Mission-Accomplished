using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MissionPopUpOnCompletion : MonoBehaviour
{
    // references
    [SerializeField] private Text missionName;
    [SerializeField] private Text missionReward;
    private Image img;

    // value
    private Color missionNameCol;
    private Color missionRewardCol;
    private Color imgCol;
    private float countDown;

    private void Start()
    {
        img = GetComponent<Image>();
        missionNameCol = missionName.color;
        missionRewardCol = missionReward.color;
        imgCol = img.color;
    }

    public void MissionCompletePopUp(string name, string reward)
    {
        var clone = Instantiate(gameObject, transform.parent);
        clone.GetComponent<MissionPopUpOnCompletion>().InitializeCoroutines(name, reward);
    }

    private void InitializeCoroutines(string name, string reward)
    {
        missionName.text = name + " Complete!";
        missionReward.text = reward;
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        // fade in
        while (countDown < Config.TIME_ANIMATION_MISSION_COMPLETE_POPUP)
        {
            yield return new WaitForFixedUpdate();
            countDown += Time.fixedDeltaTime;
            var alpha = countDown / Config.TIME_ANIMATION_MISSION_COMPLETE_POPUP;
            missionName.color = new Color(missionNameCol.r, missionNameCol.g, missionNameCol.b, alpha);
            missionReward.color = new Color(missionRewardCol.r, missionRewardCol.g, missionRewardCol.b, alpha);
            img.color = new Color(imgCol.r, imgCol.g, imgCol.b, alpha);
        }
        countDown = 0;
        StartCoroutine(SustainCoroutine());
    }

    IEnumerator SustainCoroutine()
    {
        // sustain
        while (countDown < Config.TIME_SUSTAIN_MISSION_COMPLETE_POPUP)
        {
            yield return new WaitForFixedUpdate();
            countDown += Time.fixedDeltaTime;
        }
        countDown = 0;
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        // pop up
        while (countDown < Config.TIME_ANIMATION_MISSION_COMPLETE_POPUP)
        {
            yield return new WaitForFixedUpdate();
            countDown += Time.fixedDeltaTime;
            var alpha = Config.TIME_ANIMATION_MISSION_COMPLETE_POPUP - countDown / Config.TIME_ANIMATION_MISSION_COMPLETE_POPUP;
            missionName.color = new Color(missionNameCol.r, missionNameCol.g, missionNameCol.b, alpha);
            missionReward.color = new Color(missionRewardCol.r, missionRewardCol.g, missionRewardCol.b, alpha);
            img.color = new Color(imgCol.r, imgCol.g, imgCol.b, alpha);
        }
        countDown = 0;
        Destroy(gameObject);
    }
}

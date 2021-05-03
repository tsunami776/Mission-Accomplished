using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionPlayer : MonoBehaviour
{
    [SerializeField] private Image transImg;

    private Color imgColor;

    private void Awake()
    {
        imgColor = transImg.color;
    }

    public void TransAnime_Forward()
    {
        StartCoroutine(Animation_Forward());
    }

    IEnumerator Animation_Forward()
    {
        // transition alpha (0 -> full)
        while (transImg.color.a <= 1f)
        {
            transImg.color = new Color(imgColor.r, imgColor.g, imgColor.b, transImg.color.a + Config.TIME_ANIMATION_TRANSITION_FRAME);
            yield return new WaitForSecondsRealtime(Config.TIME_ANIMATION_TRANSITION_FRAME);
        }
        gameObject.SetActive(false);
    }

    public void TransAnime_Backward()
    {
        StartCoroutine(Animation_Backward());
    }

    IEnumerator Animation_Backward()
    {
        // transition alpha (full -> 0)
        while (transImg.color.a > 0f)
        {
            transImg.color = new Color(imgColor.r, imgColor.g, imgColor.b, transImg.color.a - Config.TIME_ANIMATION_TRANSITION_FRAME);
            yield return new WaitForSecondsRealtime(Config.TIME_ANIMATION_TRANSITION_FRAME);
        }

        // unlock the timer for the new day
        GameController.GC.timer.Unlock();
        GameController.GC.UpdateTopBar();
        gameObject.SetActive(false);
    }
}

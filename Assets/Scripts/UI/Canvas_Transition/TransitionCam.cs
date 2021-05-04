using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionCam : MonoBehaviour
{
    // reference
    [SerializeField] private GameObject cam;
    [SerializeField] private Image transImg;
    [SerializeField] private Text dateText;

    // value
    private Color imgColor;

    private void Awake()
    {
        imgColor = transImg.color;
    }

    private void OnEnable()
    {
        transImg.gameObject.SetActive(true);
        dateText.text = GameController.GC.currentDate.ToShortDateString();
        TransAnime_Backward();
    }

    private void OnDisable()
    {
        transImg.color = new Color(imgColor.r, imgColor.g, imgColor.b, 1f);
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
    }

    public void TransAnime_Forward(GameObject transPlayer)
    {
        StartCoroutine(Animation_Forward(transPlayer));
    }

    IEnumerator Animation_Forward(GameObject transPlayer)
    {
        // transition alpha (0 -> full)
        while (transImg.color.a <= 1f)
        {
            transImg.color = new Color(imgColor.r, imgColor.g, imgColor.b, transImg.color.a + Config.TIME_ANIMATION_TRANSITION_FRAME);
            yield return new WaitForSecondsRealtime(Config.TIME_ANIMATION_TRANSITION_FRAME);
        }

        // play the player transition animation, when complete, turn off the current trans cam
        transPlayer.SetActive(true);
        transPlayer.GetComponent<TransitionPlayer>().TransAnime_Backward();
        cam.gameObject.SetActive(false);
    }
}

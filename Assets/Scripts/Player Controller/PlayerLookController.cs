using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    // references
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject miniMapDirIndicator;
    [SerializeField] private GameObject globalMiniMap;
    [SerializeField] private GameObject crossHair;

    // values
    private float mouseSensitive;
    private float CamVRotation = 0f;
    private bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        mouseSensitive = Config.MOUSE_SENSITIVE;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked)
        {
            // mouse input
            float lookV = Input.GetAxis("Mouse Y") * mouseSensitive * Time.deltaTime * 100f;
            float lookH = Input.GetAxis("Mouse X") * mouseSensitive * Time.deltaTime * 100f;

            // rotate player
            transform.Rotate(new Vector3(0f, lookH, 0f));

            // rotate camera
            CamVRotation -= lookV;
            CamVRotation = Mathf.Clamp(CamVRotation, -90f, 90f);
            playerCam.transform.localRotation = Quaternion.Euler(CamVRotation, 0f, 0f);

            // rotate mini map N,S,W,E
            miniMapDirIndicator.transform.Rotate(new Vector3(0f, 0f, lookH));

            // Press 'Tab' to enlarge the minimap to see all
            if (Input.GetKey(KeyCode.Tab))
            {
                //Debug.Log("Enlarge Minimap");
                globalMiniMap.SetActive(true);
                crossHair.SetActive(false);
            }
            else 
            {
                //Debug.Log("Minimap Closed");
                globalMiniMap.SetActive(false);
                crossHair.SetActive(true);
            }
        }
    }

    // lock movement
    public void Lock()
    {
        isLocked = true;
    }

    // unlock movement
    public void Unlock()
    {
        isLocked = false;
    }
}

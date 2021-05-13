using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookController : MonoBehaviour
{
    // references
    [SerializeField] private GunController gunPartsController;
    [SerializeField] private GameObject gunParts;
    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject miniMapDirIndicator;
    [SerializeField] private GameObject toolSlot;
    [SerializeField] private GameObject globalMiniMap;
    [SerializeField] private GameObject gun;
    public GameObject crossHair;

    // values
    private Vector3 initialPosition;
    private float mouseSensitive;
    private float CamVRotation;
    private bool isLocked;
    public bool isAiming;
    public bool isTPS;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = playerCam.transform.localPosition;
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
                if (!isAiming)
                {
                    crossHair.SetActive(true);
                }
            }

            // Press T to switch between FPS / TPS
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (!isTPS)
                {
                    playerCam.transform.localPosition = new Vector3(initialPosition.x + Config.OFFSET_CAM_FPS_TO_TPS_X, initialPosition.y + Config.OFFSET_CAM_FPS_TO_TPS_Y, initialPosition.z + Config.OFFSET_CAM_FPS_TO_TPS_Z);
                    GetComponent<PlayerInteractionController>().interactionRange = Config.INTERACTION_RANGE_TPS;
                    isTPS = true;

                    // disable gun
                    toolSlot.SetActive(false);
                    gunParts.SetActive(false);
                }
                else
                {
                    playerCam.transform.localPosition = initialPosition;
                    GetComponent<PlayerInteractionController>().interactionRange = Config.INTERACTION_RANGE_FPS;
                    isTPS = false;

                    // enable gun
                    toolSlot.SetActive(true);
                    gunParts.SetActive(true);
                }
            }
        }

        // adjust view of field along Gun's X coordinate
        playerCam.GetComponent<Camera>().fieldOfView = Config.DEFAULT_INTI_MAIN_CAM_VIEW - (Config.OFFSET_FPS_MID_AIM_ZOOM * ((-Config.OFFSET_FPS_MID_AIM_X - gun.transform.localPosition.x) / -Config.OFFSET_FPS_MID_AIM_X));
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

    // gun shoot recoil rotation
    public void RecoilVertical(float amount)
    {
        CamVRotation -= amount;
    }

    // unlock movement
    public void Aiming(bool flag)
    {
        isAiming = flag;
    }
}

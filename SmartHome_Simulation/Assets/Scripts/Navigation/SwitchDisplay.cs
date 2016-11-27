using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class SwitchDisplay : MonoBehaviour
{
    private static GameObject menu;
    private static GameObject camTop;
    private GameObject character;
    private static GameObject lastCamera;
    private MouseLook mouse;

    // Use this for initialization
    void Start()
    {
        menu = GameObject.Find(Config.STRING_GAMEOBJECT_MODES)
                .transform.FindChild(Config.STRING_GAMEOBJECT_MENU)
                .gameObject;
        camTop = GameObject.Find(Config.STRING_GAMEOBJECT_MODES)
                .transform.FindChild(Config.STRING_GAMEOBJECT_CAMERATOP)
                .gameObject;
        character = GameObject.Find(Config.STRING_GAMEOBJECT_MODES)
                .transform.FindChild(Config.STRING_GAMEOBJECT_CHARAKTER)
                .gameObject;
        mouse = character.GetComponent<RigidbodyFirstPersonController>().mouseLook;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !PlayModeNavigation.dialog.activeSelf && !GameobjectLoader.isLoading())
        {
            if (!Mode.isPauseMode())
            {
                if (camTop.activeSelf)
                {
                    lastCamera = camTop;
                }
                else
                {
                    mouse.curserLockState(false);
                    lastCamera = character;
                }
                menu.SetActive(true);
                lastCamera.SetActive(false);
                Mode.changeToPauseMode();
            }
            else
            {
                lastCamera.SetActive(true);
                menu.SetActive(false);
                if (lastCamera == camTop)
                {
                    Mode.changeToPlaceMode();
                }
                else
                {
                    Mode.changeToPlayMode();
                    mouse.curserLockState(true);
                }
            }
        }
    }

	/// <summary>
	/// Sets the last camera active.
	/// </summary>
    public static void setLastCameraActive()
    {
        KeyListener.resetCurrentDeviceType();
        if (lastCamera == camTop)
        {
            Mode.changeToPlaceMode();
        }
        else
        {
            Mode.changeToPlayMode();
        }
        lastCamera.SetActive(true);
        menu.SetActive(false);
    }
}
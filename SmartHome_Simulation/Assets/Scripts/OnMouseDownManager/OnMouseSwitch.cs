using System;
using System.Collections;
using UnityEngine;

public class OnMouseSwitch : OnMouseManager
{
	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
        deviceTransform = transform;
        deviceTag = deviceTransform.tag;
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
    void Update()
    {
        if (Mode.isPlaceMode() && !GameobjectLoader.isLoading() && (grids == null))
        {
            grids = new ArrayList();
            grids.AddRange(GameObject.Find(Config.STRING_GAMEOBJECT_MODES).GetComponentsInChildren<Grid>());
        }
        if (Mode.isPlaceMode() && !GameobjectLoader.isLoading() && message == null)
        {
            message = GameObject.Find(Config.OBJ_NAME_CANVAS).GetComponent<MessageManager>();
        }
        currentDeviceType = KeyListener.getCurrentDeviceType();
    }

	/// <summary>
	/// Raises the mouse down event.
	/// </summary>
    void OnMouseDown()
    {
        if (!GameobjectLoader.isLoading())
        {
            if (Mode.isPlaceMode())
            {
                if (currentDeviceType.Equals(Config.STRING_BUTTON_DELETE))
                {
                    deleteObject(deviceTransform);
                }
                else
                {
                    message.addMessageToQueue(MessageManager.MSG_DEFAULT);
                }
            }
            else if (Mode.isPlaceSwitchMode())
            {
                message.addMessageToQueue(Config.MSG_SWITCH_ONLY_ON_POLE);
            }
        }
    }
}
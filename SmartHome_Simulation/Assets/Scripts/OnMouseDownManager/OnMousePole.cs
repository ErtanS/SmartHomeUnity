using UnityEngine;
using System.Collections;

public class OnMousePole : OnMouseManager {

    // Use this for initialization
    void Start()
    {
        deviceTransform = transform.parent;
        deviceTag = deviceTransform.tag;
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
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
                    message.addMessageToQueue(Config.MSG_CANNOT_DELETE_POLE);
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

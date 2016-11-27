using System;
using System.Collections;
using UnityEngine;

public class OnMouseStove : OnMouseManager
{
	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
        deviceTransform = transform.parent;
        if (deviceTransform.childCount == 4)
        {
            deviceTransform = deviceTransform.parent.parent;
        }
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
                GameObject newObject;
                if (currentDeviceType.Equals(Config.STRING_BUTTON_DELETE))
                {
                    deleteObject(deviceTransform);
                }
                else if (GameobjectLoader.ceilingObjects.Contains(currentDeviceType))
                {
                    if (!hasObjects(GameobjectLoader.ceilingObjects))
                    {
                        newObject = Instantiate(GameobjectLoader.getPrefab(currentDeviceType));
                        createGameObject(deviceTransform, newObject);
                        if (currentDeviceType.Equals(Config.STRING_TYPE_EN_LIGHT) ||
                            currentDeviceType.Equals(Config.STRING_TYPE_EN_SPEAKER))
                        {
                            Mode.changeToPlaceSwitchMode();
                            SwitchPosition.currentObject = newObject;
                            message.addMessageToQueue(Config.MSG_SWITCH_ON_POLE);
                        }
                    }
                    else
                    {
                        message.addMessageToQueue(MessageManager.MSG_DEFAULT_USED);
                    }
                }
                else
                {
                    rotateGameObject(90, deviceTransform);
                    rotateOven();
                }
            }
            else if (Mode.isPlaceSwitchMode())
            {
                message.addMessageToQueue(Config.MSG_SWITCH_ONLY_ON_POLE);
            }
        }
    }

	/// <summary>
	/// Rotates the oven.
	/// </summary>
    private void rotateOven()
    {
        foreach (Transform child in deviceTransform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.tag.Equals(Config.STRING_TYPE_EN_OVEN))
            {
                rotateGameObject(90, child);
                break;
            }
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

public class OnMouseWindow : OnMouseManager
{
	/// <summary>
	/// Start this instance.
	/// </summary>
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
                if (currentDeviceType.Equals(Config.STRING_TYPE_EN_SHUTTERS))
                {
                    if (deviceTransform.parent.childCount == 2)
                    {
                        newObject = Instantiate(GameobjectLoader.getPrefab(currentDeviceType));
                        createGameObject(transform.parent, newObject);
                        Mode.changeToPlaceSwitchMode();
                        SwitchPosition.currentObject = newObject;
                        message.addMessageToQueue(Config.MSG_SWITCH_ON_POLE);
                    }
                    else
                    {
                        message.addMessageToQueue(Config.MSG_WINDOW_CONTAINS_SHUTTERS);
                    }
                }
                else if (currentDeviceType.Equals(Config.STRING_BUTTON_DELETE))
                {
                    removeShutters();
                    enableWall();
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

	/// <summary>
	/// Removes the shutters.
	/// </summary>
    private void removeShutters()
    {
        foreach (Transform child in deviceTransform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.tag.Equals(Config.STRING_TYPE_EN_SHUTTERS))
            {
                deleteObject(child);
                break;
            }
        }
    }

	/// <summary>
	/// Enables the wall.
	/// </summary>
    private void enableWall()
    {
        Transform wall = getWall();

        if (wall != null)
        {
            wall.gameObject.SetActive(true);
        }
    }

	/// <summary>
	/// Gets the wall.
	/// </summary>
	/// <returns>The wall.</returns>
    private Transform getWall()
    {
        foreach (Transform trans in deviceTransform.parent.GetComponentsInChildren<Transform>(true))
        {
            if (trans.tag.Equals(Config.STRING_PREFAB_WALL))
            {
                return trans;
            }
        }
        return null;
    }
}
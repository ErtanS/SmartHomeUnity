using System;
using System.Collections;
using UnityEngine;

public class OnMouseOven : OnMouseManager
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
                Transform stove = getStove();
                GameObject newObject;
                if (currentDeviceType.Equals(Config.STRING_BUTTON_DELETE))
                {
                    removeStove(stove);
                    deleteFloorObject();
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
                else if (currentDeviceType.Equals(Config.STRING_TYPE_EN_STOVE))
                {
                    if (!hasStove(stove))
                    {
                        newObject = Instantiate(GameobjectLoader.getPrefab(currentDeviceType));
                        createGameObject(deviceTransform, newObject);
                    }
                    else
                    {
                        message.addMessageToQueue(MessageManager.MSG_DEFAULT_USED);
                    }
                }
                else
                {
                    rotateGameObject(90, deviceTransform);
                    rotateStove(stove);
                }
            }
            else if (Mode.isPlaceSwitchMode())
            {
                message.addMessageToQueue(Config.MSG_SWITCH_ONLY_ON_POLE);
            }
        }
    }

	/// <summary>
	/// Removes the stove.
	/// </summary>
	/// <param name="stove">Stove.</param>
    private void removeStove(Transform stove)
    {
        if (stove != null)
        {
            deleteObject(stove);
        }
    }

	/// <summary>
	/// Gets the stove.
	/// </summary>
	/// <returns>The stove.</returns>
    private Transform getStove()
    {
        foreach (Transform child in deviceTransform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.tag.Equals(Config.STRING_TYPE_EN_STOVE) && child.childCount == 3)
            {
                return child;
            }
        }
        return null;
    }

	/// <summary>
	/// Oven has Stove.
	/// </summary>
	/// <returns><c>true</c>, if has stove, <c>false</c> otherwise.</returns>
	/// <param name="stove">Stove.</param>
    private bool hasStove(Transform stove)
    {
        return (stove != null);
    }

	/// <summary>
	/// Rotates the stove.
	/// </summary>
	/// <param name="stove">Stove.</param>
    private void rotateStove(Transform stove)
    {
        if (stove != null)
        {
            rotateGameObject(90, stove);
        }
    }
}
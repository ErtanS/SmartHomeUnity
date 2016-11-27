using System;
using System.Collections;
using UnityEngine;

public class OnMouseFloor : OnMouseManager
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
                if (GameobjectLoader.ceilingObjects.Contains(currentDeviceType))
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
                else if (GameobjectLoader.floorObjects.Contains(currentDeviceType) &&
                         !currentDeviceType.Equals(Config.STRING_TYPE_EN_STOVE))
                {
                    if (!hasObjects(GameobjectLoader.floorObjects))
                    {
                        int angle = 0;
                        bool canRotate = true;
                        Serializer serial = deviceTransform.GetComponent<Serializer>();
                        Vector2 size = GameobjectLoader.getSize(currentDeviceType);
                        while (!tryToPlace(size, serial.getGridPos()))
                        {
                            float tempSize = size.x;
                            size.x = size.y;
                            size.y = -tempSize;
                            angle += 90;
                            if (angle == 360)
                            {
                                canRotate = false;
                                break;
                            }
                        }
                        if (canRotate)
                        {
                            newObject = Instantiate(GameobjectLoader.getPrefab(currentDeviceType));
                            createGameObject(deviceTransform, newObject);
                            rotateGameObject(angle, newObject.transform);
                            updateAllGrids();
                        }
                        else
                        {
                            message.addMessageToQueue(Config.MSG_NOT_ENOUGHT_SPACE);
                        }
                    }
                    else
                    {
                        message.addMessageToQueue(MessageManager.MSG_DEFAULT_USED);
                    }
                }
                else if (currentDeviceType.Equals(Config.STRING_BUTTON_DELETE))
                {
                    message.addMessageToQueue(Config.MSG_CANNOT_DELETE_FLOOR_PLACEMODE);
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
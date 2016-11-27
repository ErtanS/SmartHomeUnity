using UnityEngine;
using System.Collections;
using System;

public class OnMouseBigSizedObjects : OnMouseManager
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
                deviceTransform = transform.parent;
                GameObject newObject;
                if (currentDeviceType.Equals(Config.STRING_BUTTON_DELETE))
                {
                    deleteFloorObject();
                }
                else if (GameobjectLoader.ceilingObjects.Contains(currentDeviceType))
                {
                    deviceTransform = getFloorOnClickedPosition();
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
                    rotateFloorObject();
                }
            }
            else if (Mode.isPlaceSwitchMode())
            {
                message.addMessageToQueue(Config.MSG_SWITCH_ONLY_ON_POLE);
            }
        }
    }

	/// <summary>
	/// Rotates the floor object.
	/// </summary>
    private void rotateFloorObject()
    {
        Serializer serial = deviceTransform.GetComponent<Serializer>();
        Vector2 size = serial.getSize();
        setFloorState(serial.getSize(), serial.getGridPos(), false);

        float tempSize = size.x;
        size.x = size.y;
        size.y = -tempSize;
        int angle = 90;
        bool canRotate = true;
        while (!tryToPlace(size, serial.getGridPos()))
        {
            tempSize = size.x;
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
            rotateGameObject(angle, deviceTransform);
            updateAllGrids();
        }
        else
        {
            message.addMessageToQueue(Config.MSG_CANNOT_ROTATE);
            setFloorState(serial.getSize(), serial.getGridPos(), true);
        }
    }

	/// <summary>
	/// Checks the mouse position.
	/// </summary>
	/// <returns><c>true</c>, if mouse position was checked, <c>false</c> otherwise.</returns>
	/// <param name="position">Position.</param>
	/// <param name="hit">Hit.</param>
    private bool checkMousePosition(Vector2 position, RaycastHit hit)
    {
        Rect rect = new Rect(position.x*1.2f, position.y*1.2f, 1.2f, 1.2f);
        if (rect.Contains(new Vector2(hit.point.x, hit.point.z)))
        {
            return true;
        }
        return false;
    }

	/// <summary>
	/// Gets the floor on clicked position.
	/// </summary>
	/// <returns>The floor on clicked position.</returns>
    private Transform getFloorOnClickedPosition()
    {
        Serializer serial = deviceTransform.GetComponent<Serializer>();
        Vector2 size = serial.getSize();
        Vector2 pos = serial.getGridPos();
        RaycastHit hit = getMousePosition();

        for (int i = 0; i < Math.Abs(size.x); i++)
        {
            for (int j = 0; j < Math.Abs(size.y); j++)
            {
                Vector2 currentGridPos = new Vector2((int) pos.x + i*(int) (size.x/Math.Abs(size.x)),
                    (int) pos.y + j*(int) (size.y/Math.Abs(size.y)));
                if (checkMousePosition(currentGridPos, hit))
                {
                    Transform trans =
                        deviceTransform.parent.parent.FindChild(Config.STRING_PREFIX_POS_TRANSFORM +
                                                                (pos.x + i*size.x/Math.Abs(size.x)) + ":" +
                                                                (pos.y + j*size.y/Math.Abs(size.y)));
                    for (int k = 0; k < trans.childCount; k++)
                    {
                        if (trans.GetChild(k).tag.Equals(Config.STRING_PREFAB_FLOOR))
                        {
                            return trans.GetChild(k);
                        }
                    }
                }
            }
        }
        return null;
    }

	/// <summary>
	/// Gets the mouse position.
	/// </summary>
	/// <returns>The mouse position.</returns>
    private RaycastHit getMousePosition()
    {
        Camera camera = GameObject.Find(Config.STRING_GAMEOBJECT_MODES)
                .transform.FindChild(Config.STRING_GAMEOBJECT_CAMERATOP)
                .GetComponent<Camera>();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit
        Physics.Raycast(ray, out hit);
        return hit;
    }
}
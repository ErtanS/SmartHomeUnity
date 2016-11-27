using UnityEngine;
using System.Collections;
using System;

public class OnMouseManager : MonoBehaviour
{
    protected string currentDeviceType;
    protected string deviceTag;
    protected Transform deviceTransform;
    protected MessageManager message;
    protected ArrayList grids;

	/// <summary>
	/// Updates all grids.
	/// </summary>
    protected void updateAllGrids()
    {
        if (grids != null && grids.Count > 0)
        {
            foreach (Grid grid in grids)
            {
                grid.registerPath();
            }
        }
    }

	/// <summary>
	/// Creates the game object.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="newObject">New object.</param>
    protected void createGameObject(Transform transform, GameObject newObject)
    {
        string currentDeviceType = KeyListener.getCurrentDeviceType();
        newObject.name = currentDeviceType + transform.parent.name;
        string currentType = newObject.tag;
        if (currentType.Equals(Config.STRING_PREFAB_SINK) || currentType.Equals(Config.STRING_PREFAB_KITCHEN_SINK) ||
            currentType.Equals(Config.STRING_PREFAB_TUB))
        {
            currentType = Config.STRING_TYPE_EN_WATER;
        }
        DataManager.addRenamedDevice(new DeviceName(newObject.name, currentType));
        newObject.transform.SetParent(transform.parent);
        newObject.transform.position = transform.position;
        if (transform.localScale.z < 0.15f && transform.tag.Equals(Config.STRING_PREFAB_WALL))
        {
            Components.moveObjectOnRightPosition(newObject.transform,
                transform.name.Split('_')[transform.name.Split('_').Length - 1], -0.05f);
        }
        newObject.transform.rotation = transform.rotation;

        if (currentDeviceType.Equals(Config.STRING_TYPE_EN_CAMERA))
        {
            newObject.transform.FindChild(Config.STRING_GAMEOBJECT_VIEW).GetComponent<Camera>().enabled = false;
        }
        if (currentDeviceType.Equals(Config.STRING_TYPE_EN_STOVE))
        {
            for (int i = 0; i < 4; i++)
            {
                newObject.transform.GetChild(0).GetChild(i).name = newObject.name + "_" +
                                                                   newObject.transform.GetChild(0).GetChild(i).name;
            }
        }
        Serializer serializer = newObject.GetComponent<Serializer>();
        serializer.serialize(false);
        setNameInput(newObject);
    }

	/// <summary>
	/// Sets the name input.
	/// </summary>
	/// <param name="newObject">New object.</param>
    private void setNameInput(GameObject newObject)
    {
        PlayModeNavigation navigation =
            GameObject.Find(Config.STRING_GAMEOBJECT_MANAGER).GetComponent<PlayModeNavigation>();
        navigation.isActive = true;
        //navigation.activateInputName();
        navigation.setLastCreatedGameObject(newObject);
    }

	/// <summary>
	/// Raises the mouse over event.
	/// </summary>
    void OnMouseOver()
    {
        if (!GameobjectLoader.isLoading())
        {
            if (Mode.isPlaceMode() && Input.GetMouseButtonDown(1) && !deviceTag.Equals(Config.STRING_PREFAB_WALL) &&
            !deviceTag.Equals(Config.STRING_PREFAB_FLOOR))
            {
                setNameInput(transform.parent.gameObject);
            }
        }
    }

	/// <summary>
	/// Deletes the floor object.
	/// </summary>
    protected void deleteFloorObject()
    {
        setPlaceFree();
        deleteObject(deviceTransform);
    }

	/// <summary>
	/// Deletes the object.
	/// </summary>
	/// <param name="trans">Trans.</param>
    protected void deleteObject(Transform trans)
    {
        DataManager.addDeletedObject(new DeviceDataSet(trans.name, trans.tag));
        DataManager.removeDeviceName(trans.name);
        Destroy(trans.gameObject);
    }

	/// <summary>
	/// Sets the state of the floor.
	/// </summary>
	/// <param name="size">Size.</param>
	/// <param name="pos">Position.</param>
	/// <param name="state">If set to <c>true</c> state.</param>
    protected void setFloorState(Vector2 size, Vector2 pos, bool state)
    {
        for (int i = 0; i < Math.Abs(size.x); i++)
        {
            for (int j = 0; j < Math.Abs(size.y); j++)
            {
                Serializer serial =
                    deviceTransform.parent.parent.FindChild(Config.STRING_PREFIX_POS_TRANSFORM +
                                                            (pos.x + i*size.x/Math.Abs(size.x)) + ":" +
                                                            (pos.y + j*size.y/Math.Abs(size.y))).GetComponent<Serializer>();
                serial.serialize(state);
            }
        }
    }

	/// <summary>
	/// Sets the place free.
	/// </summary>
    private void setPlaceFree()
    {
        Serializer serial = deviceTransform.GetComponent<Serializer>();
        setFloorState(serial.getSize(), serial.getGridPos(), false);
        updateAllGrids();
    }

	/// <summary>
	/// Rotates the game object.
	/// </summary>
	/// <param name="angle">Angle.</param>
	/// <param name="trans">Trans.</param>
    protected void rotateGameObject(int angle, Transform trans)
    {
        trans.Rotate(new Vector3(0, angle, 0));
    }

	/// <summary>
	/// Hases the objects.
	/// </summary>
	/// <returns><c>true</c>, if objects was hased, <c>false</c> otherwise.</returns>
	/// <param name="list">List.</param>
    protected bool hasObjects(ArrayList list)
    {
        foreach (Transform sibling in deviceTransform.parent.GetComponentsInChildren<Transform>(true))
        {
            if (list.Contains(sibling.tag))
            {
                return true;
            }
        }
        return false;
    }

	/// <summary>
	/// Tries to place.
	/// </summary>
	/// <returns><c>true</c>, if to place was tryed, <c>false</c> otherwise.</returns>
	/// <param name="size">Size.</param>
	/// <param name="pos">Position.</param>
    protected bool tryToPlace(Vector2 size, Vector2 pos)
    {
        for (int i = 0; i < Math.Abs(size.x); i++)
        {
            for (int j = 0; j < Math.Abs(size.y); j++)
            {
                if (
                    !canBeUsed((int) pos.x + i*(int) (size.x/Math.Abs(size.x)),
                        (int) pos.y + j*(int) (size.y/Math.Abs(size.y))))
                {
                    return false;
                }
            }
        }
        setFloorState(size, pos, true);
        return true;
    }

	/// <summary>
	/// position can be used.
	/// </summary>
	/// <returns><c>true</c>, if position can be used, <c>false</c> otherwise.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
    private bool canBeUsed(int x, int y)
    {
        Transform room = deviceTransform.parent.parent;
        if (room != null)
        {
            Transform pos = room.FindChild(Config.STRING_PREFIX_POS_TRANSFORM + x + ":" + y);
            if (pos != null)
            {
                Serializer serial = pos.GetComponent<Serializer>();
                if (!serial.isUsed())
                {
                    return true;
                }
            }
        }
        return false;
    }
}
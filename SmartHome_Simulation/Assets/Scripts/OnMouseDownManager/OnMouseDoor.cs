using System;
using System.Collections;
using UnityEngine;

public class OnMouseDoor : OnMouseManager
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
                if (currentDeviceType.Equals(Config.STRING_BUTTON_DELETE))
                {
                    enableWalls();
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
	/// Enables the walls.
	/// </summary>
    private void enableWalls()
    {
        Transform wall1 = getWall();
        Transform wall2 = getDuplicateWall(wall1);
        if (wall1 != null)
        {
            wall1.gameObject.SetActive(true);
        }
        if (wall2 != null)
        {
            wall2.gameObject.SetActive(true);
        }
    }

	/// <summary>
	/// Gets the duplicate wall.
	/// </summary>
	/// <returns>The duplicate wall.</returns>
	/// <param name="wall">Wall.</param>
    private Transform getDuplicateWall(Transform wall)
    {
        ArrayList walls = Components.findGameObjectsWithTag(Config.STRING_PREFAB_WALL);
        foreach (Transform dupWall in walls)
        {
            if (!dupWall.name.Equals(wall.name) && Walls.getName(wall).Equals(Walls.getName(dupWall)))
            {
                return dupWall;
            }
        }
        return null;
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
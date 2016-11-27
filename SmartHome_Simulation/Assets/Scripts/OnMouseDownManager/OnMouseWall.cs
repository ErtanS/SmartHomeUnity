using System;
using System.Collections;
using UnityEngine;

public class OnMouseWall : OnMouseManager
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
                Transform dupWall = getDuplicateWall();
                if (currentDeviceType.Equals(Config.STRING_TYPE_EN_DOOR))
                {
                    if (deviceTransform.parent.childCount == 1)
                    {
                        newObject = Instantiate(GameobjectLoader.getPrefab(currentDeviceType));
                        createGameObject(deviceTransform, newObject);
                        disableDuplicateWall(dupWall);
                        deviceTransform.gameObject.SetActive(false);
                        updateAllGrids();
                    }
                    else
                    {
                        message.addMessageToQueue(Config.MSG_WALL_CONTAINS_OBJECT);
                    }
                }
                else if (currentDeviceType.Equals(Config.STRING_TYPE_EN_WINDOW))
                {
                    if (deviceTransform.parent.childCount == 1 && !hasDuplicateWall(dupWall))
                    {
                        newObject = Instantiate(GameobjectLoader.getPrefab(currentDeviceType));
                        createGameObject(deviceTransform, newObject);
                        deviceTransform.gameObject.SetActive(false);
                    }
                    else
                    {
                        if (deviceTransform.parent.childCount > 1)
                        {
                            message.addMessageToQueue(Config.MSG_WALL_CONTAINS_OBJECT);
                        }
                        else
                        {
                            message.addMessageToQueue(Config.MSG_WINDOW_ON_WALL);
                        }
                    }
                }
                else if (currentDeviceType.Equals(Config.STRING_PREFAB_SINK))
                {
                    if (deviceTransform.parent.childCount == 1)
                    {
                        Serializer serialPos = deviceTransform.parent.parent.GetComponent<Serializer>();

                        if (!serialPos.isUsed())
                        {
                            serialPos.serialize(true);
                            newObject = Instantiate(GameobjectLoader.getPrefab(currentDeviceType));
                            createGameObject(deviceTransform, newObject);
                            updateAllGrids();
                        }
                        else
                        {
                            message.addMessageToQueue(MessageManager.MSG_DEFAULT_USED);
                        }
                    }
                    else
                    {
                        message.addMessageToQueue(Config.MSG_WALL_CONTAINS_OBJECT);
                    }
                }
                else if (currentDeviceType.Equals(Config.STRING_TYPE_EN_WALL))
                {
                    if (deviceTransform.parent.childCount == 1)
                    {
                        newObject = Instantiate(GameobjectLoader.getPrefab(currentDeviceType));
                        createGameObject(deviceTransform, newObject);
                        findMagicWallOnSameWall(newObject);
                    }
                    else
                    {
                        message.addMessageToQueue(Config.MSG_WALL_CONTAINS_OBJECT);
                    }
                }
                else if (currentDeviceType.Equals(Config.STRING_BUTTON_DELETE))
                {
                    message.addMessageToQueue(Config.MSG_CANNOT_DELETE_WALL);
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
	/// Finds the magic wall on same wall.
	/// </summary>
	/// <param name="newWall">New wall.</param>
    private void findMagicWallOnSameWall(GameObject newWall)
    {
        GameObject[] magicwalls = GameObject.FindGameObjectsWithTag(Config.STRING_TYPE_EN_WALL);
        string room = newWall.transform.parent.parent.parent.name;
        string side = newWall.transform.parent.name.Split('_')[0];
        float posXLast;
        float posXFirst;

        if (side.Equals(Config.STRING_LEFT) || side.Equals(Config.STRING_RIGHT))
        {
            posXLast = newWall.transform.position.z + 0.5f;
            posXFirst = newWall.transform.position.z - 0.5f;
        }
        else
        {
            posXLast = newWall.transform.position.x + 0.5f;
            posXFirst = newWall.transform.position.x - 0.5f;
        }
        ArrayList wallsSameWall = new ArrayList();
        for (int i = 0; i < magicwalls.Length; i++)
        {
            string tempRoom = magicwalls[i].transform.parent.parent.parent.name;
            string tempSide = magicwalls[i].transform.parent.name.Split('_')[0];

            float tempPosXLast;
            float tempPosXFirst;
            if (side.Equals(Config.STRING_LEFT) || side.Equals(Config.STRING_RIGHT))
            {
                tempPosXLast = magicwalls[i].transform.position.z + magicwalls[i].transform.localScale.x/2;
                tempPosXFirst = magicwalls[i].transform.position.z - magicwalls[i].transform.localScale.x/2;
            }
            else
            {
                tempPosXLast = magicwalls[i].transform.position.x + magicwalls[i].transform.localScale.x/2;
                tempPosXFirst = magicwalls[i].transform.position.x - magicwalls[i].transform.localScale.x/2;
            }


            if (room.Equals(tempRoom) && side.Equals(tempSide) && !(magicwalls[i].name.Equals(newWall.name)) &&
                (Math.Abs(posXLast - tempPosXFirst) < 0.21f || Math.Abs(posXFirst - tempPosXLast) < 0.21f))
            {
                wallsSameWall.Add(magicwalls[i]);
            }
        }
        wallsSameWall.Add(newWall);

        if (wallsSameWall.Count > 1)
        {
            scaleMagicWall(wallsSameWall, newWall);
        }
    }

	/// <summary>
	/// Scales the magic wall.
	/// </summary>
	/// <param name="walls">Walls.</param>
	/// <param name="lastInserted">Last inserted.</param>
    private void scaleMagicWall(ArrayList walls, GameObject lastInserted)
    {
        float firstPosX = 1000;
        float lastPosX = -1000;

        foreach (GameObject wall in walls)
        {
            string side = wall.transform.parent.name.Split('_')[0];
            if (side.Equals(Config.STRING_LEFT) || side.Equals(Config.STRING_RIGHT))
            {
                if (firstPosX > wall.transform.position.z)
                {
                    firstPosX = wall.transform.position.z - wall.transform.localScale.x/2;
                }
                if (lastPosX < wall.transform.position.z)
                {
                    lastPosX = wall.transform.position.z + wall.transform.localScale.x/2;
                }
            }
            else
            {
                if (firstPosX > wall.transform.position.x)
                {
                    firstPosX = wall.transform.position.x - wall.transform.localScale.x/2;
                }
                if (lastPosX < wall.transform.position.x)
                {
                    lastPosX = wall.transform.position.x + wall.transform.localScale.x/2;
                }
            }
            if (!lastInserted.name.Equals(wall.name))
            {
                deleteObject(wall.transform);
            }
        }
        string tempSide = lastInserted.transform.parent.name.Split('_')[0];

        lastInserted.transform.localScale = new Vector3(lastPosX - firstPosX, lastInserted.transform.localScale.y,
            lastInserted.transform.localScale.z);

        if (tempSide.Equals(Config.STRING_LEFT) || tempSide.Equals(Config.STRING_RIGHT))
        {
            lastInserted.transform.position = new Vector3(lastInserted.transform.position.x,
                lastInserted.transform.position.y, firstPosX + (lastPosX - firstPosX)/2);
        }
        else
        {
            lastInserted.transform.position = new Vector3(firstPosX + (lastPosX - firstPosX)/2,
                lastInserted.transform.position.y, lastInserted.transform.position.z);
        }

        message.addMessageToQueue(Config.MSG_JOIN_WALLS);
    }

	/// <summary>
	/// Disables the duplicate wall.
	/// </summary>
	/// <param name="wall">Wall.</param>
    private void disableDuplicateWall(Transform wall)
    {
        if (wall != null)
        {
            wall.gameObject.SetActive(false);
        }
    }

	/// <summary>
	/// Gets the duplicate wall.
	/// </summary>
	/// <returns>The duplicate wall.</returns>
    private Transform getDuplicateWall()
    {
        ArrayList walls = Components.findGameObjectsWithTag(Config.STRING_PREFAB_WALL);
        foreach (Transform wall in walls)
        {
            if (!wall.name.Equals(deviceTransform.name) && Walls.getName(deviceTransform).Equals(Walls.getName(wall)))
            {
                return wall;
            }
        }
        return null;
    }

	/// <summary>
	/// checks if Wall has duplicate Wall.
	/// </summary>
	/// <returns><c>true</c>, if has duplicate wall, <c>false</c> otherwise.</returns>
	/// <param name="wall">Wall.</param>
    private bool hasDuplicateWall(Transform wall)
    {
        return (wall != null);
    }
}
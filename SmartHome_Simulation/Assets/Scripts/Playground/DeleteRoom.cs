using UnityEngine;
using System.Collections;
using System;

public class DeleteRoom : MonoBehaviour
{
    private Grundriss grundriss;
    private MessageManager message;

	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
        if (Mode.isBuildMode())
        {
            message = GameObject.Find(Config.OBJ_NAME_MESSAGE).GetComponent<MessageManager>();
            grundriss = GameObject.Find(Config.STRING_GAMEOBJECT_MANAGER).GetComponent<Grundriss>();
        }
    }

	/// <summary>
	/// Raises the mouse down event.
	/// </summary>
    void OnMouseDown()
    {
        if (!GameobjectLoader.isLoading())
        {
            if (Mode.isBuildMode())
            {
                Transform parent;
                parent = transform.parent.parent.parent;

                if (parent != null && !parent.name.Contains(Config.STRING_PREFAB_EMPTY) && Grundriss.currentRoom.Count == 0)
                {
                    DataManager.addDeletedRoom(parent.name);
                    Grundriss.roomNames.Remove(parent.name);
                    Destroy(parent.gameObject);
                }
                else if (Grundriss.currentRoom.Contains(getPositionOnGrid()) && isValidToDelete(getPositionOnGrid()))
                {
                    Grundriss.currentRoom.Remove(getPositionOnGrid());
                    Destroy(transform.parent.parent.gameObject);
                    if (Grundriss.currentRoom.Count == 0)
                    {
                        grundriss.resetRoomTemplate();
                    }
                }
                else if (!isValidToDelete(getPositionOnGrid()))
                {
                    message.addMessageToQueue(Config.MSG_CANNOT_DELETE_FLOOR);
                }
                else if (!Grundriss.currentRoom.Contains(getPositionOnGrid()))
                {
                    message.addMessageToQueue(Config.MSG_ERROR_CANNOT_DELETE_ROOM);
                }
            }
        }
    }

	/// <summary>
	/// Raises the mouse over event.
	/// </summary>
    void OnMouseOver()
    {
        if (!GameobjectLoader.isLoading())
        {
            if ((Mode.isBuildMode() && Input.GetMouseButtonDown(1) && Grundriss.currentRoom.Count == 0))
            {
                string room = transform.parent.parent.parent.name;
                BuildModeNavigation.setLastEditedRoomName(room);
                BuildModeNavigation.enableInputField(room);
            }
        }
    }

	/// <summary>
	/// Gets the position on grid.
	/// </summary>
	/// <returns>The position on grid.</returns>
    private Vector2 getPositionOnGrid()
    {
        string[] cubeName = transform.parent.parent.name.Split(':');
        int posX = Convert.ToInt32(cubeName[1]);
        int posZ = Convert.ToInt32(cubeName[2]);
        return new Vector2(posX, posZ);
    }

	/// <summary>
	/// Ises the valid to delete.
	/// </summary>
	/// <returns><c>true</c>, if valid to delete was ised, <c>false</c> otherwise.</returns>
	/// <param name="currentGridPoint">Current grid point.</param>
    private bool isValidToDelete(Vector2 currentGridPoint)
    {
        int counter = 0;
        foreach (Vector2 temp in Grundriss.currentRoom)
        {
            if (temp != currentGridPoint)
            {
                Vector2 left = new Vector2(temp.x - 1, temp.y);
                Vector2 right = new Vector2(temp.x + 1, temp.y);
                Vector2 below = new Vector2(temp.x, temp.y - 1);
                Vector2 above = new Vector2(temp.x, temp.y + 1);

                bool drawLeft = true;
                bool drawRight = true;
                bool drawBelow = true;
                bool drawAbove = true;

                foreach (Vector2 point in Grundriss.currentRoom)
                {
                    if (point != currentGridPoint)
                    {
                        if (point == left)
                        {
                            drawLeft = false;
                        }
                        if (point == right)
                        {
                            drawRight = false;
                        }
                        if (point == below)
                        {
                            drawBelow = false;
                        }
                        if (point == above)
                        {
                            drawAbove = false;
                        }
                    }
                }
                if (drawLeft)
                {
                    counter++;
                }
                if (drawRight)
                {
                    counter++;
                }
                if (drawAbove)
                {
                    counter++;
                }
                if (drawBelow)
                {
                    counter++;
                }
            }
        }
        if (counter <= Grundriss.currentRoom.Count*2)
        {
            return true;
        }
        return false;
    }
}
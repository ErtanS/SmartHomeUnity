using UnityEngine;
using System.Collections;
using System;

public class cubeScript : MonoBehaviour
{
    private wandGenerator generator = new wandGenerator();
    private GameObject floorTemplate;
    private int posX;
    int posZ;
    private MessageManager message;

    // Use this for initialization
    void Start()
    {
        message = GameObject.Find(Config.OBJ_NAME_MESSAGE).GetComponent<MessageManager>();
        getPositionOnGrid();
        floorTemplate = GameobjectLoader.getPrefab(Config.STRING_PREFAB_FLOOR);
    }

	/// <summary>
	/// Raises the mouse down event.
	/// </summary>
    void OnMouseDown()
    {
        if (!GameobjectLoader.isLoading())
        {
            if (Grundriss.isClickable)
            {
                bool drawable = Grundriss.currentRoom.Count == 0;

                foreach (Vector2 pos in Grundriss.currentRoom)
                {
                    if (Math.Abs(pos.y - posZ) == 1 && Math.Abs(pos.x - posX) == 0 ||
                        Math.Abs(pos.y - posZ) == 0 && Math.Abs(pos.x - posX) == 1)
                    {
                        drawable = true;
                        break;
                    }
                }

                if (drawable)
                {
                    GameObject positionParent = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_EMPTY));
                    positionParent.name = Config.STRING_PREFIX_POS_TRANSFORM + posX + ":" + posZ;
                    GameObject floor = generator.createFloor(posX * 1.2f, posZ * 1.2f, 1.2f, 1.2f, Instantiate(floorTemplate));
                    floor.transform.SetParent(positionParent.transform);
                    positionParent.transform.SetParent(Grundriss.roomTemplate.transform);
                    Grundriss.currentRoom.Add(new Vector2(posX, posZ));
                }
                else
                {
                    message.addMessageToQueue(Config.MSG_ERROR_CANNOT_PLACE_FLOOR);
                }
            }
            else
            {
                message.addMessageToQueue(Config.MSG_ERROR_FIRST_NAME_ROOM);
            }
        }
    }

	/// <summary>
	/// Gets the position on grid.
	/// </summary>
    private void getPositionOnGrid()
    {
        string[] cubeName = name.Split('_');
        posX = Convert.ToInt32(cubeName[1]);
        posZ = Convert.ToInt32(cubeName[2]);
    }
}
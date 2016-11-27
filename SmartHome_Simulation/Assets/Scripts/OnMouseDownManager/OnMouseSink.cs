using System.Collections;
using UnityEngine;

public class OnMouseSink : OnMouseManager
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
                deviceTransform = transform.parent;
                if (currentDeviceType.Equals(Config.STRING_BUTTON_DELETE))
                {
                    deviceTransform.parent.parent.GetComponent<Serializer>().serialize(false);
                    updateAllGrids();
                    deleteObject(deviceTransform);
                }
                else if (GameobjectLoader.ceilingObjects.Contains(currentDeviceType))
                {
                    deviceTransform = getFloorOnPosition();
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
	/// Gets the floor on position.
	/// </summary>
	/// <returns>The floor on position.</returns>
    private Transform getFloorOnPosition()
    {
        foreach (Transform pos in deviceTransform.parent.parent.GetComponentsInChildren<Transform>())
        {
            if (pos.tag.Equals(Config.STRING_PREFAB_FLOOR))
            {
                return pos;
            }
        }
        return null;
    }
}
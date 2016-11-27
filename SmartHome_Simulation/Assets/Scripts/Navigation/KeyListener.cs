using UnityEngine;
using System.Collections;

public class KeyListener : MonoBehaviour
{
    private static string currentDeviceType;
    private static string oldDeviceType;
    private static ArrayList types = new ArrayList();
    private static MessageManager message;

    // Use this for initialization
    void Start()
    {
        message = GameObject.Find(Config.STRING_GAMEOBJECT_CANVAS).GetComponent<MessageManager>();
        types.Add(Config.STRING_TYPE_EN_DOOR);
        types.Add(Config.STRING_TYPE_EN_WINDOW);
        types.Add(Config.STRING_TYPE_EN_SHUTTERS);
        types.Add(Config.STRING_PREFAB_SINK);
        types.Add(Config.STRING_TYPE_EN_WALL);
        types.Add(Config.STRING_TYPE_EN_CAMERA);
        types.Add(Config.STRING_TYPE_EN_LIGHT);
        types.Add(Config.STRING_TYPE_EN_SPEAKER);
        types.Add(Config.STRING_TYPE_EN_DRYER);
        types.Add(Config.STRING_TYPE_EN_WASHER);
        types.Add(Config.STRING_PREFAB_KITCHEN_SINK);
        types.Add(Config.STRING_TYPE_EN_OVEN);
        types.Add(Config.STRING_TYPE_EN_PC);
        types.Add(Config.STRING_TYPE_EN_STOVE);
        types.Add(Config.STRING_PREFAB_TUB);
        types.Add(Config.STRING_TYPE_EN_TV);
        types.Add(Config.STRING_BUTTON_DELETE);
    }

    // Update is called once per frame
    void Update()
    {
        if (oldDeviceType != null && !currentDeviceType.Equals(oldDeviceType))
        {
            Mode.changeToPlaceMode();
        }
        oldDeviceType = currentDeviceType;
    }

    /// <summary>
    /// Anzeigen der entsprechenden Nachricht/Infomeldung im Fenster
    /// </summary>
    private static void showInfoMessage()
    {
        switch (currentDeviceType)
        {
            case Config.STRING_TYPE_EN_DOOR:
                message.setImportantMessage(Config.MSG_DOOR);
                break;
            case Config.STRING_TYPE_EN_WINDOW:
                message.setImportantMessage(Config.MSG_WINDOW);
                break;
            case Config.STRING_TYPE_EN_SHUTTERS:
                message.setImportantMessage(Config.MSG_SHUTTERS);
                break;
            case Config.STRING_PREFAB_SINK:
                message.setImportantMessage(Config.MSG_SINK);
                break;
            case Config.STRING_TYPE_EN_WALL:
                message.setImportantMessage(Config.MSG_MAGICWALL);
                break;
            case Config.STRING_TYPE_EN_CAMERA:
                message.setImportantMessage(Config.MSG_CAMERA);
                break;
            case Config.STRING_TYPE_EN_LIGHT:
                message.setImportantMessage(Config.MSG_LIGHT);
                break;
            case Config.STRING_TYPE_EN_SPEAKER:
                message.setImportantMessage(Config.MSG_SPEAKER);
                break;
            case Config.STRING_TYPE_EN_DRYER:
                message.setImportantMessage(Config.MSG_DRYER);
                break;
            case Config.STRING_TYPE_EN_WASHER:
                message.setImportantMessage(Config.MSG_WASHER);
                break;
            case Config.STRING_PREFAB_KITCHEN_SINK:
                message.setImportantMessage(Config.MSG_KITCHENSINK);
                break;
            case Config.STRING_TYPE_EN_OVEN:
                message.setImportantMessage(Config.MSG_OVEN);
                break;
            case Config.STRING_TYPE_EN_PC:
                message.setImportantMessage(Config.MSG_PC);
                break;
            case Config.STRING_TYPE_EN_STOVE:
                message.setImportantMessage(Config.MSG_STOVE);
                break;
            case Config.STRING_PREFAB_TUB:
                message.setImportantMessage(Config.MSG_TUB);
                break;
            case Config.STRING_TYPE_EN_TV:
                message.setImportantMessage(Config.MSG_TV);
                break;
            case Config.STRING_BUTTON_DELETE:
                message.setImportantMessage(Config.MSG_DELETE);
                break;

        }
    }

    /// <summary>
    /// aktuell ausgewähltes Gerät setzen 
    /// z.B. window, door, ... 
    /// </summary>
    /// <param name="type">Gerätetyp</param>
    public static void setCurrentDeviceType(int index)
    {
        currentDeviceType = types[index] as string;
        showInfoMessage();
    }

    public static void resetCurrentDeviceType()
    {
        currentDeviceType = Config.STRING_EMPTY;
    }

    /// <summary>
    /// Getter für den aktuellen Gerätetypen
    /// </summary>
    /// <returns></returns>
    public static string getCurrentDeviceType()
    {
        if (currentDeviceType != null)
        {
            return currentDeviceType;
        }
        return Config.STRING_EMPTY;
    }
}
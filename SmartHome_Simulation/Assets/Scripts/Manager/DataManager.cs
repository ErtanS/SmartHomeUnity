using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class DataManager : MonoBehaviour
{
    public static bool insertReady = false;
    private static DataSet devices;
    private static DataSet timestamps;
    private static GetDataJob myJob;
    private static RequestHandler rh = new RequestHandler();
    private static ArrayList deletedObjects = new ArrayList();
    private static ArrayList deletedRooms = new ArrayList();
    private static ArrayList renamedDevices = new ArrayList();
    public static bool data;
    public static Dictionary<string, string> postData = new Dictionary<string, string>();
    public static string url;

	/// <summary>
	/// Resets the database.
	/// </summary>
    private IEnumerator resetDatabase()
    {
        insertReady = false;
        devices = null;
        timestamps = null;
        if (myJob != null)
        {
            myJob.Abort();
        }
        myJob = null;
        yield return StartCoroutine(deleteDevicesFromDatabase());
    }

	/// <summary>
	/// Initializes the database.
	/// </summary>
	/// <param name="isBackup">If set to <c>true</c> is backup.</param>
	public void initializeDatabase(bool isBackup)
    {
        GameobjectLoader.startLoading();
        StartCoroutine(initialize(isBackup));
    }

	/// <summary>
	/// Initialize the specified isBackup.
	/// </summary>
	/// <param name="isBackup">If set to <c>true</c> is backup.</param>
    private IEnumerator initialize(bool isBackup)
    {
        yield return StartCoroutine(resetDatabase());
        yield return StartCoroutine(updateNamesInDatabase());
        yield return StartCoroutine(setUpDataBase());
        createTimestamps();
        createDevices();
        if (isBackup)
        {
            yield return StartCoroutine(insertClock());
            yield return StartCoroutine(rh.makeRequest(rh.backupDatabase(LevelManager.getFileName())));
        }
        else
        {
            setTime();
        }
        GameobjectLoader.stopLoading();
        insertReady = true;
        myJob = new GetDataJob();
        myJob.Start();
    }

    /// <summary>
    /// Fügt Geräte hinzu, löscht überflüssige Zeitstempel und aktualisiert die Musikliste
    /// </summary>
    private IEnumerator setUpDataBase()
    {
        yield return StartCoroutine(insertDevices(Config.TAG_TABLE_LIGHT));
        yield return StartCoroutine(insertDevices(Config.TAG_TABLE_SPEAKER));
        yield return StartCoroutine(insertDevices(Config.TAG_TABLE_SHUTTERS));
        yield return StartCoroutine(insertDevices(Config.TAG_TABLE_DOOR));
        yield return StartCoroutine(insertDevices(Config.TAG_TABLE_WINDOW));
        yield return StartCoroutine(insertDevices(Config.TAG_TABLE_HEATER));
        yield return StartCoroutine(insertDevices(Config.STRING_TYPE_EN_CAMERA));
        yield return StartCoroutine(insertDevices(Config.STRING_TYPE_EN_WATER));
        yield return StartCoroutine(insertDevices(Config.STRING_TYPE_EN_TV));
        yield return StartCoroutine(insertDevices(Config.STRING_TYPE_EN_WASHER));
        yield return StartCoroutine(insertDevices(Config.STRING_TYPE_EN_DRYER));
        yield return StartCoroutine(insertDevices(Config.STRING_TYPE_EN_OVEN));
        yield return StartCoroutine(insertDevices(Config.STRING_TYPE_EN_STOVE));
        yield return StartCoroutine(insertDevices(Config.STRING_TYPE_EN_PC));
        yield return StartCoroutine(insertDevices(Config.STRING_TYPE_EN_WALL));

        rh.sendGetRequest(Config.URL_RESET);
        rh.sendGetRequest(Config.URL_DELETE_TIMESTAMPS);
        yield return StartCoroutine(insertMusicPlaylist());
        yield return StartCoroutine(insertChannels());
    }

    /// <summary>
    /// Fügt Geräte des gegebenen Typen in die Datenbank ein
    /// </summary>
    /// <param name="tag">Tag zur Identifikation des Typs</param>
    private IEnumerator insertDevices(string tag)
    {
        GameObject[] devicesList = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject device in devicesList)
        {
            if (!(tag.Equals(Config.STRING_TYPE_EN_STOVE) && (device.transform.childCount < 4)))
            {
                yield return StartCoroutine(rh.makeRequest(rh.insert(Config.URL_INSERT, getName(device), getRoom(device), tag)));
            }
        }
    }

    /// <summary>
    /// Lädt die Musikliste in die Datenbank 
    /// </summary>
    private IEnumerator insertMusicPlaylist()
    {
        UnityEngine.Object[] musikListe = Resources.LoadAll(Config.STRING_MUSIC_FOLDER, typeof(AudioClip));
        for (int i = 0; i < musikListe.Length; i++)
        {
            yield return StartCoroutine(rh.makeRequest(rh.insertPlaylists(i.ToString(), musikListe[i].name, Config.TAG_MUSIC)));
        }
    }

    /// <summary>
    /// Lädt die Channels in die Datenbank 
    /// </summary>
    private IEnumerator insertChannels()
    {
        UnityEngine.Object[] musikListe = Resources.LoadAll(Config.STRING_VIDEO_FOLDER, typeof(MovieTexture));

        for (int i = 0; i < musikListe.Length; i++)
        {
            yield return StartCoroutine(rh.makeRequest(rh.insertPlaylists(i.ToString(), musikListe[i].name, Config.TAG_CHANNEL)));
        }
    }

    /// <summary>
    /// Lädt die Channels in die Datenbank 
    /// </summary>
    private IEnumerator insertClock()
    {
        print("Hour__________________"+Clock.hour);
        print("Minute________________"+Clock.minute);
        yield return StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TABLE_CLOCK, Config.TAG_HOUR, Clock.hour.ToString(), 1)));
        yield return StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TABLE_CLOCK, Config.TAG_MINUTE, Clock.minute.ToString(), 1)));
    }

    /// <summary>
    /// Aktualisierung der Daten
    /// </summary>
    public static void loadData()
    {
        fillDevices();
        fillTimestamps();
    }

    /// <summary>
    /// Erstellt das DataSet zur Verwaltung der Geräte
    /// </summary>
    private static void createDevices()
    {
        String result = rh.selectAll(Config.URL_GET_ALL, Config.STRING_CATEGORY_DEVICE);
        String[] resultSet = getResult(result);

        devices = new DataSet(resultSet[3], resultSet[2], resultSet[1], resultSet[4], resultSet[5], resultSet[0],
            resultSet[7], resultSet[8], resultSet[9], resultSet[10], resultSet[11], resultSet[12], resultSet[13],
            resultSet[14], resultSet[15]);
    }

    /// <summary>
    /// Aktualisert das DataSet zur Verwaltung der Geräte
    /// </summary>
    private static void fillDevices()
    {
        String result = rh.selectAll(Config.URL_GET_ALL, Config.STRING_CATEGORY_DEVICE);
        //print(result);
        String[] resultSet = getResult(result);

        devices.fillDevices(resultSet[3], resultSet[2], resultSet[1], resultSet[4], resultSet[5], resultSet[0],
            resultSet[7], resultSet[8], resultSet[9], resultSet[10], resultSet[11], resultSet[12], resultSet[13],
            resultSet[14], resultSet[15]);
    }


    /// <summary>
    /// Erstellt das DataSet zur Verwaltung der Zeitstempel
    /// </summary>
    private static void createTimestamps()
    {
        string result = rh.selectAll(Config.URL_GET_ALL, Config.STRING_CATEGORY_TIMESTAMP);
        string[] resultSet = DataManager.getResult(result);

        timestamps = new DataSet(resultSet[3], resultSet[2], resultSet[1], resultSet[4], resultSet[5], resultSet[0],
            resultSet[6], resultSet[7], resultSet[8], resultSet[9], resultSet[10], resultSet[11], resultSet[12],
            resultSet[13], resultSet[14], resultSet[15]);
    }

    /// <summary>
    /// Aktualisert das DataSet zur Verwaltung der Zeitstempel
    /// </summary>
    private static void fillTimestamps()
    {
        string result = rh.selectAll(Config.URL_GET_ALL, Config.STRING_CATEGORY_TIMESTAMP);
        string[] resultSet = DataManager.getResult(result);

        timestamps.fillTimestamps(resultSet[3], resultSet[2], resultSet[1], resultSet[4], resultSet[5], resultSet[0],
            resultSet[6], resultSet[7], resultSet[8], resultSet[9], resultSet[10], resultSet[11], resultSet[12],
            resultSet[13], resultSet[14], resultSet[15]);
    }


    /// <summary>
    /// Verarbeitet die PHP-Rückgabe zum korrekten Anlegen in das zugehörige DataSet 
    /// </summary>
    /// <returns>Array mit Daten des entsprechenden Gerätetyps</returns>
    /// <param name="json">Gesamte PHP-Rückgabe</param>
    private static string[] getResult(string json)
    {
        string[] result = new string[16];
        string[] temp = json.Split('[');
        int count = 0;
        for (int i = 2; i < temp.Length; i++)
        {
            string split = temp[i];
            result[count] = "{\"result\":[" + split.Split(']')[0] + "]}";
            count++;
        }
        return result;
    }


    /// <summary>
    /// aktualisiert den Datensatz des Gerätes mit übergebenem Namen  
    /// </summary>
    /// <returns> neuen Datensatz, wenn neue Daten vorhanden, ansonsten die alten Daten</returns>
    /// <param name="name">Name des Geräts</param>
    /// <param name="oldDevice">Vorhandene Daten des Gerätes</param>
    public static DeviceDataSet getDevice(string name, DeviceDataSet oldDevice)
    {
        DeviceDataSet result = devices.getDevice(name);
        if (result != null)
        {
            return result;
        }
        return oldDevice;
    }

    /// <summary>
    /// Überprüft, ob zu gegebener Uhrzeit ein Timestamp vorhanden ist.
    /// </summary>
    /// <returns><c>true</c>, wenn, Timestamp vorhanden <c>false</c> wenn nicht </returns>
    /// <param name="hour"> Stunde </param>
    /// <param name="minute">Minute</param>
    public static bool isTimestamp(int hour, int minute)
    {
        return timestamps.isTimestamp(hour, minute);
    }

    /// <summary>
    /// Beendet alle Threads bei Schließen der Anwendung
    /// </summary>
    void OnApplicationQuit()
    {
        if (myJob != null)
        {
            myJob.Abort();
        }
    }

	/// <summary>
	/// Adds the deleted object.
	/// </summary>
	/// <param name="device">Device.</param>
    public static void addDeletedObject(DeviceDataSet device)
    {
        deletedObjects.Add(device);
    }

	/// <summary>
	/// Adds the deleted room.
	/// </summary>
	/// <param name="roomName">Room name.</param>
    public static void addDeletedRoom(string roomName)
    {
        deletedRooms.Add(roomName);
    }

	/// <summary>
	/// Deletes the devices from database.
	/// </summary>
    private IEnumerator deleteDevicesFromDatabase()
    {
        foreach (DeviceDataSet temp in deletedObjects)
        {
            string type = temp.getType();
            if (type.Equals(Config.STRING_TYPE_EN_STOVE))
            {
                yield return StartCoroutine(rh.makeRequest(rh.delete(temp.getName() + "_" + Config.STOVE_OR, type)));
                yield return StartCoroutine(rh.makeRequest(rh.delete(temp.getName() + "_" + Config.STOVE_UR, type)));
                yield return StartCoroutine(rh.makeRequest(rh.delete(temp.getName() + "_" + Config.STOVE_OL, type)));
                yield return StartCoroutine(rh.makeRequest(rh.delete(temp.getName() + "_" + Config.STOVE_UL, type)));
            }
            else
            {
                if (type.Equals(Config.STRING_PREFAB_KITCHEN_SINK) || type.Equals(Config.STRING_PREFAB_TUB) ||
                    type.Equals(Config.STRING_PREFAB_SINK))
                {
                    type = Config.STRING_TYPE_EN_WATER;
                }
                yield return StartCoroutine(rh.makeRequest(rh.delete(temp.getName(), type)));
            }
        }
        clearDeletedObjects();

        foreach (string temp in deletedRooms)
        {
            yield return StartCoroutine(rh.makeRequest(rh.deleteRoom(temp)));
        }
        clearDeletedRooms();
    }

	/// <summary>
	/// Clears the deleted objects.
	/// </summary>
    public static void clearDeletedObjects()
    {
        deletedObjects.Clear();
    }

	/// <summary>
	/// Clears the deleted rooms.
	/// </summary>
    public static void clearDeletedRooms()
    {
        deletedRooms.Clear();
    }

	/// <summary>
	/// Gets the name.
	/// </summary>
	/// <returns>The name.</returns>
	/// <param name="device">Device.</param>
    private static string getName(GameObject device)
    {
        if (device.tag.Equals(Config.STRING_TYPE_EN_WATER))
        {
            return device.transform.parent.name;
        }
        return device.name;
    }

	/// <summary>
	/// Gets the room.
	/// </summary>
	/// <returns>The room.</returns>
	/// <param name="device">Device.</param>
    private static string getRoom(GameObject device)
    {
        if (device.tag.Equals(Config.STRING_TYPE_EN_WATER))
        {
            if (device.transform.parent.parent.parent.parent != null)
            {
                return device.transform.parent.parent.parent.parent.name;
            }
            return device.transform.parent.parent.parent.name;
        }
        if (device.tag.Equals(Config.STRING_TYPE_EN_STOVE))
        {
            return device.transform.parent.parent.parent.parent.name;
        }
        if (device.tag.Equals(Config.STRING_TYPE_EN_HEATER))
        {
            return device.transform.parent.name;
        }
        if (device.transform.parent.parent.parent != null)
        {
            return device.transform.parent.parent.parent.name;
        }
        return device.transform.parent.parent.name;
    }

	/// <summary>
	/// Adds the renamed device.
	/// </summary>
	/// <param name="device">Device.</param>
    public static void addRenamedDevice(DeviceName device)
    {
        renamedDevices.Add(device);
    }

	/// <summary>
	/// Checks if name can be used
	/// </summary>
	/// <returns><c>true</c>, if name vaild was ised, <c>false</c> otherwise.</returns>
	/// <param name="name">Name.</param>
    public static bool isNameVaild(string name)
    {
        foreach (DeviceName tempName in renamedDevices)
        {
            if (tempName.getNewName() != null)
            {
                if (tempName.getNewName().Equals(name))
                {
                    return false;
                }
            }
            else if (tempName.getFirstName().Equals(name))
            {
                return false;
            }
        }
        return true;
    }

	/// <summary>
	/// Adds the name of the device.
	/// </summary>
	/// <param name="oldName">Old name.</param>
	/// <param name="newName">New name.</param>
    public static void addDeviceName(string oldName, string newName)
    {
        foreach (DeviceName tempName in renamedDevices)
        {
            if (tempName.getNewName() != null)
            {
                if (tempName.getNewName().Equals(oldName))
                {
                    tempName.rename(newName);
                }
            }
            else if (tempName.getFirstName().Equals(oldName))
            {
                tempName.rename(newName);
            }
        }
    }

	/// <summary>
	/// Removes the name of the device.
	/// </summary>
	/// <param name="name">Name.</param>
    public static void removeDeviceName(string name)
    {
        DeviceName removedName = null;
        foreach (DeviceName tempName in renamedDevices)
        {
            if (tempName.getNewName() != null)
            {
                if (tempName.getNewName().Equals(name))
                {
                    removedName = tempName;
                }
            }
            else if (tempName.getFirstName().Equals(name))
            {
                removedName = tempName;
            }
        }
        if (removedName != null)
        {
            renamedDevices.Remove(removedName);
        }
    }

	/// <summary>
	/// Updates the names in database.
	/// </summary>
    private IEnumerator updateNamesInDatabase()
    {
        foreach (DeviceName renamedDevice in renamedDevices)
        {
            if (renamedDevice.getNewName() != null)
            {
                yield return StartCoroutine(rh.makeRequest(rh.updateDatabase(renamedDevice.getDeviceType(), Config.TAG_NAME, renamedDevice.getNewName(),
                    Config.TAG_NAME, renamedDevice.getFirstName())));
                renamedDevice.reset();
            }
        }
    }

	/// <summary>
	/// Resets the renamed devices list.
	/// </summary>
    public static void resetRenamedDevicesList()
    {
        renamedDevices = new ArrayList();
    }

	/// <summary>
	/// Sets the time.
	/// </summary>
    private static void setTime()
    {
        string result = rh.sendGetRequest(Config.URL_GET_TIME);
        JsonData jArray = JsonMapper.ToObject(result);

        Clock.hour = Convert.ToInt32(jArray[0][Config.TAG_HOUR].ToString().Trim());
        Clock.minute = Convert.ToInt32(jArray[0][Config.TAG_MINUTE].ToString().Trim());
    }
}
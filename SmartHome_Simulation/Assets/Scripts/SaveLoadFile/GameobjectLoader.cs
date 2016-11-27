using UnityEngine;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameobjectLoader : MonoBehaviour
{
    public static Object[] prefabs;
    public static ArrayList floorObjects = new ArrayList();
    public static ArrayList wallObjects = new ArrayList();
    public static ArrayList ceilingObjects = new ArrayList();
    private static ArrayList jsonStrings = new ArrayList();
    public static bool prefabsReady;
    private static bool loading;
    public static Dictionary<string, Vector2> sizeList = new Dictionary<string, Vector2>();
    private MessageManager message;
    private DataManager dm;

	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
        dm = GameObject.Find("Manager").GetComponent<DataManager>();
        fillFloorObjectsArray();
        fillCeilingObjectsArray();
        fillWallObjectsArray();
        fillSizeList();
        prefabs = Resources.LoadAll(Config.STRING_PREFAB_FOLDER);
        prefabsReady = true;
    }

	/// <summary>
	/// Fills the size list.
	/// </summary>
    private static void fillSizeList()
    {
        if (sizeList.Count == 0)
        {
            sizeList.Add(Config.STRING_PREFAB_KITCHEN_SINK, new Vector2(2, -2));
            sizeList.Add(Config.STRING_TYPE_EN_PC, new Vector2(2, -1));
            sizeList.Add(Config.STRING_PREFAB_TUB, new Vector2(2, -2));
            sizeList.Add(Config.STRING_TYPE_EN_TV, new Vector2(2, -1));
        }
    }

	/// <summary>
	/// Fills the floor objects array.
	/// </summary>
    private static void fillFloorObjectsArray()
    {
        if (floorObjects.Count == 0)
        {
            floorObjects.Add(Config.STRING_TYPE_EN_DRYER);
            floorObjects.Add(Config.STRING_TYPE_EN_WASHER);
            floorObjects.Add(Config.STRING_PREFAB_KITCHEN_SINK);
            floorObjects.Add(Config.STRING_TYPE_EN_OVEN);
            floorObjects.Add(Config.STRING_TYPE_EN_PC);
            floorObjects.Add(Config.STRING_TYPE_EN_STOVE);
            floorObjects.Add(Config.STRING_PREFAB_TUB);
            floorObjects.Add(Config.STRING_TYPE_EN_TV);
        }
    }

	/// <summary>
	/// Fills the wall objects array.
	/// </summary>
    private static void fillWallObjectsArray()
    {
        if (wallObjects.Count == 0)
        {
            wallObjects.Add(Config.STRING_TYPE_EN_DOOR);
            wallObjects.Add(Config.STRING_TYPE_EN_SHUTTERS);
            wallObjects.Add(Config.STRING_PREFAB_SINK);
            wallObjects.Add(Config.STRING_TYPE_EN_WALL);
            wallObjects.Add(Config.STRING_TYPE_EN_WINDOW);
        }
    }

	/// <summary>
	/// Fills the ceiling objects array.
	/// </summary>
    private static void fillCeilingObjectsArray()
    {
        if (ceilingObjects.Count == 0)
        {
            ceilingObjects.Add(Config.STRING_TYPE_EN_CAMERA);
            ceilingObjects.Add(Config.STRING_TYPE_EN_LIGHT);
            ceilingObjects.Add(Config.STRING_TYPE_EN_SPEAKER);
        }
    }

	/// <summary>
	/// Gets the size.
	/// </summary>
	/// <returns>The size.</returns>
	/// <param name="name">Name.</param>
    public static Vector2 getSize(string name)
    {
        if (name != null && sizeList.ContainsKey(name))
        {
            return sizeList[name];
        }
        return new Vector2(1, 1);
    }

	/// <summary>
	/// Gets the prefab.
	/// </summary>
	/// <returns>The prefab.</returns>
	/// <param name="name">Name.</param>
    public static GameObject getPrefab(string name)
    {
        foreach (Object temp in prefabs)
        {
            if (name.Equals(temp.name))
            {
                return (GameObject) temp;
            }
        }
        return null;
    }

	/// <summary>
	/// Sets the json string.
	/// </summary>
	/// <param name="json">Json.</param>
    public static void setJsonString(string json)
    {
        jsonStrings.Add(json);
    }

	/// <summary>
	/// Deletes the json from list.
	/// </summary>
	/// <param name="json">Json.</param>
    public static void deleteJsonFromList(string json)
    {
        jsonStrings.Remove(json);
    }

	/// <summary>
	/// Gets the save data.
	/// </summary>
	/// <returns>The save data.</returns>
    public static string[] getSaveData()
    {
        string[] returnList = new string[jsonStrings.Count];
        int count = 0;
        foreach (string temp in jsonStrings)
        {
            returnList[count++] = temp;
        }
        return returnList;
    }

	/// <summary>
	/// Methode to check if current scnene is loading
	/// </summary>
	/// <returns><c>true</c>, if loading was ised, <c>false</c> otherwise.</returns>
    public static bool isLoading()
    {
        return loading;
    }

	/// <summary>
	/// Starts the loading.
	/// </summary>
    public static void startLoading()
    {
        loading = true;
    }

    /// <summary>
	/// Stops the loading.
	/// </summary>
    public static void stopLoading()
    {
        loading = false;
    }

    /// <summary>
    /// Loads the scene.
    /// </summary>
    /// <returns>The scene.</returns>
    /// <param name="fileName">File name.</param>
    /// <param name="level">Level.</param>
    public IEnumerator loadScene(string fileName, int level)
    {
        loading = true;
        destroyAllGameobjectsInScene();
        yield return new WaitForEndOfFrame();
        ArrayList uniqueData = new ArrayList();
        ArrayList gameObjects = new ArrayList();
        string[] json = File.ReadAllLines(fileName);
        foreach (string temp in json)
        {
            SerializeData data = JsonUtility.FromJson<SerializeData>(temp);
            if (!data.name.Contains(Config.STRING_PREFAB_EMPTY))
            {
                gameObjects.Add(createLoadedGameObject(data, level));
                uniqueData.Add(data);
            }
        }
        yield return new WaitForEndOfFrame();
        foreach (SerializeData data in uniqueData)
        {
            if (data.parent != null && !data.parent.Equals(Config.STRING_EMPTY) &&
                !data.name.Contains(Config.STRING_PREFAB_EMPTY))
            {
                setParent(data, gameObjects);
            }
        }
        yield return new WaitForEndOfFrame();
        setUpObjects();
        yield return new WaitForEndOfFrame();
        if (level == LevelManager.SCENE_PLAYMODE)
        {
            activateCameraObjects(Mode.isPlayMode());
            yield return new WaitForEndOfFrame();
            foreach (Grid grid in GameObject.Find(Config.STRING_GAMEOBJECT_MODES).GetComponentsInChildren<Grid>())
            {
                grid.registerPath();
            }
        }
        else
        {
            activateCameraObjects(false);
            fillRoomNameList();
        }
        yield return new WaitForEndOfFrame();
        RequestHandler rh = new RequestHandler();
        yield return StartCoroutine(rh.makeRequest(rh.restoreDatabase(LevelManager.getFileName())));
        dm.initializeDatabase(false);
    }

	/// <summary>
	/// Fills the room name list.
	/// </summary>
    private void fillRoomNameList()
    {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            if (o.tag.Equals(Config.STRING_PREFAB_EMPTY) && o.transform.parent == null)
            {
                Grundriss.roomNames.Add(o.name);
            }
        }
    }

	/// <summary>
	/// Creates the loaded game object.
	/// </summary>
	/// <returns>The loaded game object.</returns>
	/// <param name="data">Data.</param>
	/// <param name="level">Level.</param>
    private GameObject createLoadedGameObject(SerializeData data, int level)
    {
        GameObject gObject = getPrefab(data.prefab);
        if (gObject != null)
        {
            GameObject instance = Instantiate(gObject);
            instance.transform.position = data.position;
            instance.transform.localScale = data.scale;
            instance.transform.localRotation = data.rotation;
            instance.transform.name = data.name;
            string currentType = data.prefab;
            if (data.prefab.Equals(Config.STRING_PREFAB_SINK) || data.prefab.Equals(Config.STRING_PREFAB_KITCHEN_SINK) ||
                data.prefab.Equals(Config.STRING_PREFAB_TUB))
            {
                currentType = Config.STRING_TYPE_EN_WATER;
            }
            DataManager.addRenamedDevice(new DeviceName(data.name, currentType));
            if (data.prefab.Equals(Config.STRING_TYPE_EN_STOVE))
            {
                for (int i = 0; i < 4; i++)
                {
                    instance.transform.FindChild(Config.STRING_BODY).GetChild(i).name = data.name + "_" +
                                                                                        instance.transform.FindChild(
                                                                                                Config.STRING_BODY)
                                                                                            .GetChild(i)
                                                                                            .name;
                }
            }
            return instance;
        }
        return null;
    }

	/// <summary>
	/// Sets the parents.
	/// </summary>
	/// <param name="data">Data.</param>
	/// <param name="gameObjects">Game objects.</param>
    private void setParent(SerializeData data, ArrayList gameObjects)
    {
        Transform child = null;
        Transform par = null;
        Serializer serializer = null;
        foreach (GameObject temp in gameObjects)
        {
            if (temp.name.Equals(data.name))
            {
                child = temp.transform;
                serializer = temp.GetComponent<Serializer>();
            }
            if (temp.name.Equals(data.parent))
            {
                par = temp.transform;
            }
        }
        if (child != null && null != data.parent)
        {
            child.SetParent(par);
            serializer.serializeDataFromFile(data);
        }
    }

	/// <summary>
	/// Destroys all gameobjects in scene.
	/// </summary>
    private void destroyAllGameobjectsInScene()
    {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            if (o.tag.Equals(Config.STRING_PREFAB_EMPTY) && o.transform.parent == null)
            {
                Destroy(o);
            }
        }
        if (Mode.isPlayMode())
        {
            GameObject.Find(Config.STRING_GAMEOBJECT_MODES)
                .transform.FindChild(Config.STRING_THIEF_BEHAVIOUR)
                .FindChild(Config.STRING_THIEF)
                .position = new Vector3(-5, 0, -5);
            GameObject.Find(Config.STRING_GAMEOBJECT_MODES)
                .transform.FindChild(Config.STRING_GAMEOBJECT_CHARAKTER)
                .position = new Vector3(-1, 0.1f, -1);
        }
        Clock.hour = 0;
        Clock.minute = 0;
        Grundriss.currentRoom = new ArrayList();
        jsonStrings = new ArrayList();
        Grundriss.roomNames = new ArrayList();
        DataManager.resetRenamedDevicesList();
        DataManager.clearDeletedObjects();
        DataManager.clearDeletedRooms();
        foreach (Grid grid in GameObject.Find(Config.STRING_GAMEOBJECT_MODES).GetComponentsInChildren<Grid>())
        {
            grid.resetPath();
        }
    }

	/// <summary>
	/// Activates the camera objects.
	/// </summary>
	/// <param name="active">If set to <c>true</c> active.</param>
    public static void activateCameraObjects(bool active)
    {
        GameObject[] cameraList = GameObject.FindGameObjectsWithTag(Config.STRING_TYPE_EN_CAMERA);
        foreach (GameObject camera in cameraList)
        {
            camera.transform.FindChild(Config.STRING_GAMEOBJECT_VIEW).gameObject.SetActive(active);
        }
    }

	/// <summary>
	/// Sets up objects.
	/// </summary>
    public static void setUpObjects()
    {
        Walls.manageWalls();
        Ceiling.manageCeiling();
        CeilingObjects.manageCeilingObjects();
        Doors.manageDoors();
        FloorObjects.manageFloorObjects();
        Floors.manageFloors();
        Poles.managePoles();
        WallObjects.manageWallObjects();
        Windows.manageWindows();
    }
}
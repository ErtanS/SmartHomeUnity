using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public const int SCENE_SETTINGS = 3;
    public const int SCENE_PLAYMODE = 2;
    public const int SCENE_BUILDMODE = 1;
    public const int SCENE_MENU = 0;
    private static bool loadObjects;
    private static string currentFile;
    private static int currentLevel;
    private GameobjectLoader loader;

	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
        if (currentFile != null)
        {
            loadObjects = false;
            loadObjectsinScene();
        }
        else if(GameobjectLoader.isLoading())
        {
            GameobjectLoader.stopLoading();
        }
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
    void Update()
    {
        if (loadObjects && currentFile != null)
        {
            loadObjects = false;
            loadObjectsinScene();
        }
    }

	/// <summary>
	/// Loads the objects from file.
	/// </summary>
    public static void loadObjectsFromFile()
    {
        loadObjects = true;
    }

	/// <summary>
	/// Changes the level.
	/// </summary>
	/// <param name="newLevel">New level.</param>
    public static void changeLevel(int newLevel)
    {
        currentLevel = newLevel;
        SceneManager.LoadScene(currentLevel);
    }

	/// <summary>
	/// Changes the file.
	/// </summary>
	/// <param name="newFile">New file.</param>
    public static void changeFile(string newFile)
    {
        currentFile = newFile;
    }

	/// <summary>
	/// Checks if play mode can be started.
	/// </summary>
	/// <returns><c>true</c>, if start play mode was caned, <c>false</c> otherwise.</returns>
    public static bool canStartPlayMode()
    {
        return !string.IsNullOrEmpty(currentFile);
    }

    private void loadObjectsinScene()
    {
        loader = GameObject.Find(Config.STRING_GAMEOBJECT_MANAGER).GetComponent<GameobjectLoader>();
        StartCoroutine(loader.loadScene(currentFile, currentLevel));
    }

	/// <summary>
	/// Gets the name of the file.
	/// </summary>
	/// <returns>The file name.</returns>
    public static string getFileName()
    {
        if (currentFile != null)
        {
            string[] part = currentFile.Split('\\');
            return part[part.Length - 1].Split('.')[0];
        }
        return null;
    }

	/// <summary>
	/// Resets the level manager.
	/// </summary>
    public static void resetLevelManager()
    {
        currentFile = null;
        loadObjects = false;
        DataManager.clearDeletedObjects();
        DataManager.clearDeletedRooms();
        Grundriss.currentRoom = new ArrayList();
        Grundriss.roomNames = new ArrayList();
        DataManager.resetRenamedDevicesList();
    }
}
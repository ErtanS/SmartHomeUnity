using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveDialog : MonoBehaviour
{
    private static int mode;
    private static int scene;
    private FileExplorer fileExplorer;
    private static bool buildScene;
    private static MessageManager message;

    void Start()
    {
        fileExplorer = new FileExplorer(GameObject.Find("Manager").GetComponent<DataManager>());
    }

	/// <summary>
	/// Clicks the yes.
	/// </summary>
    public void clickYes()
    {
        transform.gameObject.SetActive(false);
        if (fileExplorer.saveFile(GameobjectLoader.getSaveData()))
        {
            loadNewScene(true);
        }    
    }

	/// <summary>
	/// Clicks the no.
	/// </summary>
    public void clickNo()
    {
        transform.gameObject.SetActive(false);
        loadNewScene(false);
    }

	/// <summary>
	/// Clicks the cancel.
	/// </summary>
    public void clickCancel()
    {
        transform.gameObject.SetActive(false);
    }

	/// <summary>
	/// Loads the new scene.
	/// </summary>
    private void loadNewScene(bool save)
    {
        if (mode == -1) {
            Application.Quit();
        }
        else if (mode == Mode.MENU_MODE)
        {
            GameobjectLoader.startLoading();
            Mode.setMode(mode);
            LevelManager.resetLevelManager();
            LevelManager.changeLevel(scene);
        }
        else if (buildScene || mode==Mode.BUILD_MODE)
        {
            if (LevelManager.canStartPlayMode())
            {
                KeyListener.resetCurrentDeviceType();
                GameobjectLoader.startLoading();
                Mode.setMode(mode);
                LevelManager.changeLevel(scene);
            }
        }
        else
        { 
            if (save)
            {
                GameobjectLoader.setUpObjects();
            }
            else
            {
                GameobjectLoader.startLoading();
                LevelManager.loadObjectsFromFile();
            }
            KeyListener.resetCurrentDeviceType();
            message.clearMessages();
            if (mode == Mode.PLACE_MODE)
            {
                GameobjectLoader.activateCameraObjects(false);
            }
            else
            {
                GameobjectLoader.activateCameraObjects(true);
            }
            Mode.setMode(mode);
        }
    }

    public static void setMode(int newMode)
    {
        mode = newMode;
    }

    public static void setScene(int newScene)
    {
        scene = newScene;
    }

    public static void setBuildScene(bool state)
    {
        buildScene = state;
    }

    public static void setMessage(MessageManager msg)
    {
        message= msg;
    }
}
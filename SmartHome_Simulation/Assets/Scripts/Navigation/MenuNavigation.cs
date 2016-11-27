using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    private Button btnBuild;
    private Button btnPlay;
    private Button btnNew;
    private Button btnLoad;
    private FileExplorer fileExplorer;

    // Use this for initialization
    void Start()
    {
        RequestHandler rh = new RequestHandler();
        rh.sendGetRequest(Config.URL_CLEAR_DATABASE);
        btnBuild = GameObject.Find(Config.OBJ_NAME_CANVAS).transform.FindChild(Config.BTN_BUILD).GetComponent<Button>();
        btnPlay = GameObject.Find(Config.OBJ_NAME_CANVAS).transform.FindChild(Config.BTN_PLAY).GetComponent<Button>();
        btnNew = GameObject.Find(Config.OBJ_NAME_CANVAS).transform.FindChild(Config.BTN_NEW).GetComponent<Button>();
        btnLoad = GameObject.Find(Config.OBJ_NAME_CANVAS).transform.FindChild(Config.BTN_LOAD).GetComponent<Button>();
        fileExplorer = new FileExplorer(GameObject.Find("Manager").GetComponent<DataManager>());
    }

	/// <summary>
	/// Triggers when the build button is clicked.
	/// </summary>
    public void clickBuildButton()
    {
        btnNew.gameObject.SetActive(true);
        btnLoad.gameObject.SetActive(true);
    }

	/// <summary>
	/// Triggers when the play button is clicked.
	/// </summary>
    public void clickPlayButton()
    {
        if (fileExplorer.loadFile())
        {
            Mode.changeToPlaceMode();
            KeyListener.resetCurrentDeviceType();
            LevelManager.changeLevel(LevelManager.SCENE_PLAYMODE);
        }
    }

	/// <summary>
	/// Triggers when the new button is clicked.
	/// </summary>
    public void clickNewButton()
    {
        GameobjectLoader.startLoading();
        Clock.hour = 0;
        Clock.minute = 0;
        RequestHandler rh = new RequestHandler();
        rh.sendGetRequest(Config.URL_CLEAR_DATABASE);
        Mode.changeToBuildMode();
        LevelManager.changeLevel(LevelManager.SCENE_BUILDMODE);
    }

	/// <summary>
	/// Triggers when the load button is clicked.
	/// </summary>
    public void clickLoadButton()
    {
        if (fileExplorer.loadFile())
        {
            Mode.changeToBuildMode();
            LevelManager.changeLevel(LevelManager.SCENE_BUILDMODE);
        }
    }

	/// <summary>
	/// Triggers when the close button is clicked.
	/// </summary>
    public void onClickCloseButton()
    {
        Application.Quit();
    }

	/// <summary>
	/// Triggers when the game button is clicked.
	/// </summary>
    public void clickGameButton()
    {
        if (fileExplorer.loadFile())
        {
            Mode.changeToPlayMode();
            LevelManager.changeLevel(LevelManager.SCENE_PLAYMODE);
        }
    }

	/// <summary>
	/// Triggers when the settings button is clicked.
	/// </summary>
    public void clickSettingsButton()
    {
        LevelManager.changeLevel(LevelManager.SCENE_SETTINGS);
    }
}
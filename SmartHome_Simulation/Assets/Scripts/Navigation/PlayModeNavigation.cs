using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class PlayModeNavigation : MonoBehaviour
{
    public GUIContent content;
    private int height;
    private int width;
    private Object[] icons;
    private Button btnPlay;
    private Button btnBuildMode;
    private FileExplorer fileExplorer;
    private InputField inputName;
    private static GameObject lastInsertedGameObject;
    public bool isActive = false;
    private CameraController cameraController;
    private GameObject cameraTop;
    private GameObject character;
    private GameObject menu;
    private MessageManager message;
    private Regex r = new Regex("^[a-zA-Z0-9 ]*$");
    public static GameObject dialog;
    private GameObject buttons;
    


    // Use this for initialization
    void Start()
    {
        fileExplorer = new FileExplorer(GameObject.Find("Manager").GetComponent<DataManager>());
        message = GameObject.Find(Config.OBJ_NAME_CANVAS).GetComponent<MessageManager>();
        GameObject parentCamCharacter = GameObject.Find(Config.STRING_GAMEOBJECT_MODES);
        cameraTop = parentCamCharacter.transform.FindChild(Config.STRING_GAMEOBJECT_CAMERATOP).gameObject;
        character = parentCamCharacter.transform.FindChild(Config.STRING_GAMEOBJECT_CHARAKTER).gameObject;
        menu = parentCamCharacter.transform.FindChild(Config.STRING_GAMEOBJECT_MENU).gameObject;
        dialog = menu.transform.FindChild("Dialog").gameObject;
        buttons = menu.transform.FindChild("Buttons").gameObject;
        cameraController = cameraTop.GetComponent<CameraController>();
        icons = Resources.LoadAll(Config.STRING_ICON_FOLDER);
        inputName = GameObject.Find(Config.OBJ_NAME_CANVAS)
                .transform.FindChild(Config.OBJ_INPUT_NAME)
                .GetComponent<InputField>();
        btnPlay = buttons
                .transform.FindChild(Config.BTN_PLAY)
                .GetComponent<Button>();
        btnBuildMode = buttons
                .transform.FindChild(Config.BTN_BUILD_MODE)
                .GetComponent<Button>();
        StartCoroutine(updateNameField());
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
    void Update()
    {
        height = Screen.height;
        width = Screen.width;

        if (!Mode.isPauseMode())
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
            if (Mode.isPlayMode() && !GameobjectLoader.isLoading())
            {
                btnBuildMode.gameObject.SetActive(true);
                btnPlay.gameObject.SetActive(false);
                if (!character.activeSelf)
                {
                    character.SetActive(true);
                }
                if (cameraTop.activeSelf)
                {
                    cameraTop.SetActive(false);
                }
            }
            else
            {
                btnBuildMode.gameObject.SetActive(false);
                btnPlay.gameObject.SetActive(true);
                if (!cameraTop.activeSelf)
                {
                    cameraTop.SetActive(true);
                }
                if (character.activeSelf)
                {
                    character.SetActive(false);
                }
            }
        }
        if ((Mode.isPlaceMode() || Mode.isPlaceSwitchMode()) && string.IsNullOrEmpty(KeyListener.getCurrentDeviceType() ))
        {
            message.setImportantMessage(Config.MSG_PLACE_MODE);
        }
        if (Mode.isPauseMode())
        {
            buttons.SetActive(!dialog.activeSelf);
            if (!menu.activeSelf)
            {
                menu.SetActive(true);
            }
        }
    }

	/// <summary>
	/// Updates the name field.
	/// </summary>
	/// <returns>The name field.</returns>
    private IEnumerator updateNameField()
    {
        while (true)
        {
            if (isActive)
            {
                activateInputName();
                isActive = false;
                yield return new WaitForEndOfFrame();
            }
            if (inputName.gameObject.activeSelf &&
                (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
            {
                string name = inputName.text;
                string oldName = lastInsertedGameObject.name;


                if (name != null && !name.Equals(Config.STRING_EMPTY) && DataManager.isNameVaild(name) &&
                    r.IsMatch(name))
                {
                    lastInsertedGameObject.name = name;
                    DataManager.addDeviceName(oldName, name);

                    if (lastInsertedGameObject.tag.Equals(Config.STRING_TYPE_EN_STOVE))
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            lastInsertedGameObject.transform.GetChild(0).GetChild(i).name = name + "_" +
                            lastInsertedGameObject.transform.GetChild(0).GetChild(i).name.Split('_')
                                [lastInsertedGameObject.transform.GetChild(0).GetChild(i).name.Split('_').Length - 1];
                        }
                    }

                    deactivateInputName();
                }
                else
                {
                    activateInputName();
                    if (name != null && !name.Equals(oldName))
                    {
                        if (name.Equals(Config.STRING_EMPTY))
                        {
                            message.addMessageToQueue(Config.MSG_ERROR_ENTER_NAME);
                        }
                        else if (!DataManager.isNameVaild(name))
                        {
                            message.addMessageToQueue(Config.MSG_ERROR_NAME_TAKEN);
                        }
                        else if (!r.IsMatch(name))
                        {
                            message.addMessageToQueue(Config.MSG_NOT_ALLOWED_CHARACTERS);
                        }
                        else
                        {
                            message.addMessageToQueue(Config.MSG_CANNOT_CHANGE_NAME);
                        }
                    }
                }
            }
            if (inputName.gameObject.activeSelf && !inputName.isFocused)
            {
                deactivateInputName();
            }
            yield return null;
        }
    }

	/// <summary>
	/// Triggers when the save button is clicked.
	/// </summary>
    public void clickSaveButton()
    {
        fileExplorer.saveFile(GameobjectLoader.getSaveData());
        //SwitchDisplay.setLastCameraActive();
    }

	/// <summary>
	/// Triggers when the load button is clicked.
	/// </summary>
    public void clickLoadButton()
    {
        if (fileExplorer.loadFile())
        {
            character.transform.position = new Vector3(-1, 0.1f, -1);
            LevelManager.loadObjectsFromFile();
            SwitchDisplay.setLastCameraActive();
        }
    }

	/// <summary>
	/// Triggers when the play button is clicked.
	/// </summary>
    public void clickPlayButton()
    {
        SaveDialog.setMode(Mode.PLAY_MODE);
        SaveDialog.setBuildScene(false);
        SaveDialog.setMessage(message);
        dialog.SetActive(true);
        //message.clearMessages();
        //if (!fileExplorer.saveFile(GameobjectLoader.getSaveData()))
        //{
        //    GameobjectLoader.startLoading();
        //    LevelManager.loadObjectsFromFile();
        //}
        //else
        //{
        //    GameobjectLoader.setUpObjects();    
        //}
        //GameobjectLoader.activateCameraObjects(true);
        //btnPlay.gameObject.SetActive(false);
        //btnBuildMode.gameObject.SetActive(true);
        //Mode.changeToPlayMode();
        //menu.SetActive(false);
    }

	/// <summary>
	/// Triggers when the build button is clicked.
	/// </summary>
    public void clickBuildModeButton()
    {
        SaveDialog.setMode(Mode.PLACE_MODE);
        SaveDialog.setBuildScene(false);
        SaveDialog.setMessage(message);
        dialog.SetActive(true);
        //message.clearMessages();
        //if (!fileExplorer.saveFile(GameobjectLoader.getSaveData()))
        //{
        //    GameobjectLoader.startLoading();
        //    LevelManager.loadObjectsFromFile();
        //}
        //else
        //{
        //    GameobjectLoader.setUpObjects();
        //}
        //GameobjectLoader.activateCameraObjects(false);
        //btnPlay.gameObject.SetActive(true);
        //btnBuildMode.gameObject.SetActive(false);
        //Mode.changeToPlaceMode();
        //menu.SetActive(false);
    }

	/// <summary>
	/// Clicks the scene scene button.
	/// </summary>
    public void clickBuildSceneButton()
    {
        SaveDialog.setScene(LevelManager.SCENE_BUILDMODE);
        SaveDialog.setMode(Mode.BUILD_MODE);
        SaveDialog.setBuildScene(false);
        dialog.SetActive(true);
        //fileExplorer.saveFile(GameobjectLoader.getSaveData());
        //GameobjectLoader.startLoading();
        //Mode.changeToBuildMode();
        //LevelManager.changeLevel(LevelManager.SCENE_BUILDMODE);
    }

	/// <summary>
	/// Triggers when the mainmenu button is clicked.
	/// </summary>
    public void onClickMainMenuButton()
    {
        SaveDialog.setScene(LevelManager.SCENE_MENU);
        SaveDialog.setMode(Mode.MENU_MODE);
        dialog.SetActive(true);
        //fileExplorer.saveFile(GameobjectLoader.getSaveData());
        //GameobjectLoader.startLoading();
        //Mode.changeToMenuMode();
        //LevelManager.resetLevelManager();
        //LevelManager.changeLevel(LevelManager.SCENE_MENU);
    }

	/// <summary>
	/// Triggers when the close button is clicked.
	/// </summary>
    public void onClickCloseButton()
    {
        SaveDialog.setMode(-1);
        dialog.SetActive(true);
        //fileExplorer.saveFile(GameobjectLoader.getSaveData());
        //Application.Quit();
    }

	/// <summary>
	/// Activates the inputfield.
	/// </summary>
    private void activateInputName()
    {
        cameraController.enabled = false;
        inputName.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(inputName.gameObject, null);
        inputName.OnPointerClick(new PointerEventData(EventSystem.current));
    }

	/// <summary>
	/// Deactivates the inputfield.
	/// </summary>
    private void deactivateInputName()
    {
        inputName.gameObject.SetActive(false);
        cameraController.enabled = true;
    }

	/// <summary>
	/// Sets the last created game object.
	/// </summary>
	/// <param name="newObject">New object.</param>
    public void setLastCreatedGameObject(GameObject newObject)
    {
        lastInsertedGameObject = newObject;
        inputName.text = newObject.name;
    }

	/// <summary>
	/// Raises the GUI event.
	/// </summary>
    void OnGUI()
    {
        if ((Mode.isPlaceMode() || Mode.isPlaceSwitchMode()) && !GameobjectLoader.isLoading())
        {
            for (int i = 0; i < 9; i++)
            {
                content.image = icons[i] as Texture2D;
                if (GUI.Button(new Rect(0, height*0.11f*i, width*0.05f, height*0.1f), content))
                {
                    KeyListener.setCurrentDeviceType(i);
                }
                if (i < 8)
                {
                    content.image = icons[i + 9] as Texture2D;
                    if (GUI.Button(new Rect(width*0.05f + 10, height*0.11f*i, width*0.05f, height*0.1f), content))
                    {
                        KeyListener.setCurrentDeviceType(i + 9);
                    }
                }
            }
        }
    }
}
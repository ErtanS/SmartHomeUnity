using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class BuildModeNavigation : MonoBehaviour
{
    private static InputField ipName;
    private static Button btnCreate;
    private static Button btnCancel;
    private Button btnSave;
    private Button btnLoad;
    private Button btnPlay;
    private static Button btnApplyName;
    private FileExplorer fileExplorer;
    private Grundriss grundriss;
    private RequestHandler rh = new RequestHandler();
    private GameObject menu;
    private GameObject mainCamera;
    private Regex r = new Regex("^[a-zA-Z0-9 ]*$");
    private static string lastEditedRoom;
    private MessageManager message;
    private GameObject dialog;
    private GameObject buttons;


    // Use this for initialization
    void Start()
    {
        message = GameObject.Find(Config.OBJ_NAME_MESSAGE).GetComponent<MessageManager>();
        grundriss = GameObject.Find(Config.STRING_GAMEOBJECT_MANAGER).GetComponent<Grundriss>();
        mainCamera = GameObject.Find(Config.STRING_GAMEOBJECT_MODES)
                .transform.FindChild(Config.STRING_GAMEOBJECT_CAMERATOP).gameObject;
        menu = GameObject.Find(Config.STRING_GAMEOBJECT_MODES)
                .transform.FindChild(Config.STRING_GAMEOBJECT_MENU).gameObject;
        ipName = GameObject.Find(Config.OBJ_NAME_CANVAS)
                .transform.FindChild(Config.STRING_INPUTFIELD_ROOMNAME).GetComponent<InputField>();
        btnCreate = GameObject.Find(Config.OBJ_NAME_CANVAS).transform.FindChild(Config.BTN_APPLY).GetComponent<Button>();
        btnCancel = GameObject.Find(Config.OBJ_NAME_CANVAS).transform.FindChild(Config.BTN_CANCEL).GetComponent<Button>();
        btnApplyName = GameObject.Find(Config.OBJ_NAME_CANVAS).transform.FindChild(Config.BTN_APPLY_NAME).GetComponent<Button>();
        dialog = menu.transform.FindChild("Dialog").gameObject;
        buttons = menu.transform.FindChild("Buttons").gameObject;
        fileExplorer = new FileExplorer(GameObject.Find("Manager").GetComponent<DataManager>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Grundriss.currentRoom.Count > 0 && !btnApplyName.gameObject.activeSelf)
        {
            if (!btnCancel.gameObject.activeSelf || !btnCreate.gameObject.activeSelf)
            {
                btnCancel.gameObject.SetActive(true);
                btnCreate.gameObject.SetActive(true);
            }
        }
        else if (btnCancel.gameObject.activeSelf || btnCreate.gameObject.activeSelf)
        {
            btnCancel.gameObject.SetActive(false);
            btnCreate.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !dialog.activeSelf && !GameobjectLoader.isLoading())
        {
            if (Mode.isPauseMode())
            {
                Mode.changeToBuildMode();
                mainCamera.SetActive(true);
                menu.SetActive(false);
            }
            else
            {
                Mode.changeToPauseMode();
                menu.SetActive(true);
                mainCamera.SetActive(false);
            }
        }
        if (Mode.isPauseMode())
        {
            buttons.SetActive(!dialog.activeSelf);
        }
    }

    /// <summary>
    /// Enables the input field.
    /// </summary>
    /// <param name="displayedText">Displayed text.</param>
    public static void enableInputField(string displayedText)
    {
        btnApplyName.gameObject.SetActive(true);
        ipName.gameObject.SetActive(true);
        ipName.text = displayedText;
        EventSystem.current.SetSelectedGameObject(ipName.gameObject, null);
        Grundriss.isClickable = false;
    }

    /// <summary>
    /// Triggers when the create button is clicked.
    /// </summary>
    public void clickCreateButton()
    {
        if (Grundriss.currentRoom.Count > 0)
        {
            enableInputField("");
        }
        else
        {
            message.addMessageToQueue(Config.MSG_ERROR_ROOMCOUNT_EQUALS_ZERO);
        }
    }

    /// <summary>
    /// Triggers when the cancle button is clicked.
    /// </summary>
    public void clickCancelButton()
    {
        grundriss.resetRoomTemplate();
    }

    public void clickSaveButton()
    {
        if (Grundriss.currentRoom.Count == 0)
        {
            fileExplorer.saveFile(GameobjectLoader.getSaveData());
        }
        else
        {
            mainCamera.SetActive(true);
            menu.SetActive(false);
            Mode.changeToBuildMode();
            message.addMessageToQueue(Config.MSG_ERROR_SAVE_ROOM_BEFORE);
        }

    }

    /// <summary>
    /// Triggers when the load button is clicked.
    /// </summary>
    public void clickLoadButton()
    {
        if (fileExplorer.loadFile())
        {
            mainCamera.SetActive(true);
            menu.SetActive(false);
            Mode.changeToBuildMode();
            LevelManager.loadObjectsFromFile();
        }
    }

    /// <summary>
    /// Triggers when the play button is clicked.
    /// </summary>
    public void clickPlayButton()
    {
        SaveDialog.setScene(LevelManager.SCENE_PLAYMODE);
        SaveDialog.setMode(Mode.PLACE_MODE);
        SaveDialog.setBuildScene(true);
        dialog.SetActive(true);
        //fileExplorer.saveFile(GameobjectLoader.getSaveData());
        //if (LevelManager.canStartPlayMode())
        //{
        //    GameobjectLoader.startLoading();
        //    Mode.changeToPlaceMode();
        //    LevelManager.changeLevel(LevelManager.SCENE_PLAYMODE);
        //}
    }

    /// <summary>
    /// Triggers when the game button is clicked.
    /// </summary>
    public void clickGameButton()
    {

        SaveDialog.setScene(LevelManager.SCENE_PLAYMODE);
        SaveDialog.setMode(Mode.PLAY_MODE);
        SaveDialog.setBuildScene(true);
        dialog.SetActive(true);
        //fileExplorer.saveFile(GameobjectLoader.getSaveData());
        //if (LevelManager.canStartPlayMode())
        //{
        //    GameobjectLoader.startLoading();
        //    Mode.changeToPlayMode();
        //    LevelManager.changeLevel(LevelManager.SCENE_PLAYMODE);
        //}
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
        //LevelManager.resetLevelManager();
        //Mode.changeToMenuMode();
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
    /// Triggers when the apply button is clicked.
    /// </summary>
    public void clickApplyButton()
    {
        string inputText = ipName.text.Trim();
        if (Grundriss.currentRoom.Count != 0)
        {
            if (!inputText.Equals("") && !Grundriss.roomNames.Contains(inputText) && r.IsMatch(inputText))
            {
                ipName.gameObject.SetActive(false);
                btnApplyName.gameObject.SetActive(false);
                ipName.text = "";
                GameObject roomParent = Instantiate(Grundriss.roomTemplate);
                roomParent.name = inputText;
                roomParent.tag = Config.STRING_PREFAB_EMPTY;
                GameObject poleParent = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_EMPTY));
                poleParent.name = Config.STRING_POLE + roomParent.name;
                poleParent.transform.SetParent(roomParent.transform);
                Grundriss.roomNames.Add(inputText);
                foreach (Vector2 temp in Grundriss.currentRoom)
                {
                    grundriss.drawRoom(temp, poleParent, roomParent);
                }
                addHeater(roomParent);
                grundriss.resetRoomTemplate();
            }
            else
            {
                nameValidation(inputText);
            }
        }
        else
        {
            if (!inputText.Equals(Config.STRING_EMPTY) && !Grundriss.roomNames.Contains(inputText) &&
                r.IsMatch(inputText))
            {
                GameObject room = GameObject.Find(lastEditedRoom);
                StartCoroutine(rh.makeRequest(rh.renameRoom(lastEditedRoom, inputText)));
                Grundriss.roomNames.Remove(lastEditedRoom);
                Grundriss.roomNames.Add(inputText);
                lastEditedRoom = inputText;
                room.name = inputText;
                Transform[] children = room.GetComponentsInChildren<Transform>();
                foreach (Transform temp in children)
                {
                    if (temp.tag.Equals(Config.STRING_TYPE_EN_HEATER))
                    {
                        temp.name = inputText + Config.STRING_TYPE_GER_HEATER;
                        DataManager.addDeviceName(temp.name, inputText + Config.STRING_TYPE_GER_HEATER);
                    }
                }
            }
            else
            {
                nameValidation(inputText);
            }
            Grundriss.isClickable = false;
            ipName.gameObject.SetActive(false);
            btnApplyName.gameObject.SetActive(false);
            ipName.text = "";
        }
    }

    /// <summary>
    /// Adds the heater.
    /// </summary>
    /// <param name="roomParent">Room parent.</param>
    private void addHeater(GameObject roomParent)
    {
        GameObject heater = Instantiate(GameobjectLoader.getPrefab(Config.STRING_TYPE_EN_HEATER));
        heater.transform.SetParent(roomParent.transform);
        heater.transform.name = roomParent.name + Config.STRING_TYPE_GER_HEATER;
    }

    /// <summary>
    /// Sets the last name of the edited room.
    /// </summary>
    /// <param name="room">Room.</param>
    public static void setLastEditedRoomName(string room)
    {
        lastEditedRoom = room;
    }

    /// <summary>
    /// Validates the Name.
    /// </summary>
    /// <param name="inputText">Input text.</param>
    private void nameValidation(string inputText)
    {
        if (ipName.IsActive() && !ipName.isFocused)
        {
            EventSystem.current.SetSelectedGameObject(ipName.gameObject, null);
        }
        if (Grundriss.roomNames.Contains(inputText))
        {
            message.addMessageToQueue(Config.MSG_ERROR_NAME_TAKEN);
        }
        else if (inputText.Equals("") && !ipName.isFocused)
        {
            message.addMessageToQueue(Config.MSG_ERROR_ENTER_NAME);
        }
        else if (!r.IsMatch(inputText))
        {
            message.addMessageToQueue(Config.MSG_NOT_ALLOWED_CHARACTERS);
        }
        else
        {
            message.addMessageToQueue(Config.MSG_CANNOT_CHANGE_NAME);
        }
    }
}
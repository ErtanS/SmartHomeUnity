using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class DoorManager : MonoBehaviour
{
    Animator animator;
    private bool openOnTriggerEnter = false;
    private bool waitLocked = false;
    private GameObject close;
    private GameObject open;
    private int oldDoorState = -1;
    private GameObject passwordCanvas;
    private InputField passwordField;
    private MessageManager message;
    private DoorDataSet dataSet;
    private int id;
    private bool firstStart = false;
    private RequestHandler rh = new RequestHandler();
    ArrayList grids = new ArrayList();

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        if (!firstStart && DataManager.insertReady && Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            message = GameObject.Find(Config.STRING_GAMEOBJECT_CANVAS).GetComponent<MessageManager>();
            grids = new ArrayList();
            grids.AddRange(GameObject.Find(Config.STRING_GAMEOBJECT_MODES).GetComponentsInChildren<Grid>());
            close = transform.FindChild(Config.OBJ_DOOR).FindChild(Config.OBJ_DOOR_CLOSE).gameObject;
            open = transform.FindChild(Config.OBJ_DOOR).FindChild(Config.OBJ_DOOR_OPEN).gameObject;
            animator = transform.FindChild(Config.OBJ_DOOR).GetComponent<Animator>();
            passwordCanvas = GameObject.Find(Config.OBJ_NAME_CANVAS);
            passwordField = passwordCanvas.transform.FindChild(Config.OBJ_PASSWORD_FIELD).GetComponent<InputField>();
            dataSet = (DoorDataSet) DataManager.getDevice(name, dataSet);
            id = dataSet.getId();
            firstStart = true;
            StartCoroutine(changeScene());
        }
    }

    /// <summary>
    /// Aktalisiert die Eigenschften der Tür
    /// Wenn die Tür abgeschlossen ist(status = 1), wird die Tür verdunkelt
    /// Wenn die Tür offen ist(status=1), wird die Verdunklung entfernt 	
    /// </summary>
    private void updateState()
    {
        dataSet = (DoorDataSet) DataManager.getDevice(name, dataSet);
        int doorState = dataSet.getState();
        if (doorState == 1)
        {
            if (oldDoorState != doorState)
            {
                close.SetActive(true);
                open.SetActive(false);
                updateAllGrids();
            }
            if (!waitLocked && animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_DOOR_OPEN_IDLE))
            {
                animator.SetTrigger(Config.ANIMATION_TRIGGER_CLOSE);
                waitLocked = true;
            }
        }
        if (doorState == 0)
        {
            if (oldDoorState != doorState)
            {
                close.SetActive(false);
                open.SetActive(true);
                updateAllGrids();
            }
            waitLocked = false;
        }
        oldDoorState = doorState;
    }

    /// <summary>
    /// Beim Betreten des Triggers vor der Tür, öffnet sich die Tür, wenn sie nicht abgeschlossen ist
    /// </summary>
    /// <param name="col">Collider</param>
    void OnTriggerEnter(Collider col)
    {
        if (DataManager.insertReady && (col.tag.Equals(Config.STRING_PLAYER) || col.tag.Equals(Config.STRING_THIEF)))
        {
            int doorState = dataSet.getState();
            DoorSwitch.isInCollider = true;

            if (doorState == 0 && animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_DOOR_IDLE))
            {
                animator.SetTrigger(Config.ANIMATION_TRIGGER_OPEN);
                openOnTriggerEnter = true;
            }
        }
    }

    /// <summary>
    /// Während man vor der Tür steht und diese abgeschlossen ist, hat man die Möglichkeit das Passwort einzugeben.
    /// Ist das Passwort richtig, öffnet sich die Tür.
    /// Ist die Tür nicht abgeschlossen und hat sich nicht bereits beim Betreten des Triggers geöffnet, so öffnet sich diese nun
    /// </summary>
    /// <param name="col">Collider</param>
    void OnTriggerStay(Collider col)
    {
        if (DataManager.insertReady && (col.tag.Equals(Config.STRING_PLAYER) || col.tag.Equals(Config.STRING_THIEF)))
        {
            string passwordString = "-1";
            string databasePassword = dataSet.getPassword();

            if (passwordField != null && !passwordField.isFocused && RigidbodyFirstPersonController.movement == false)
            {
                RigidbodyFirstPersonController.movement = true;
                passwordString = passwordField.text;
                passwordField.gameObject.SetActive(false);
                if (passwordString.Equals(databasePassword))
                {
                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.TAG_TABLE_DOOR, Config.TAG_STATE, Config.STRING_STATUS_AUS, id)));
                }
                else
                {
                    message.addMessageToQueue(Config.MSG_INVALID_PASSWORD);
                }
            }


            int doorState = dataSet.getState();

            if (doorState == 0 && !openOnTriggerEnter &&
                animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_DOOR_IDLE))
            {
                openOnTriggerEnter = true;
                animator.SetTrigger(Config.ANIMATION_TRIGGER_OPEN);
            }
            if (doorState == 1)
            {
                openOnTriggerEnter = false;
            }
        }
    }

    /// <summary>
    /// Verlässt man den Türbereich wieder, schließt sich die Tür hinter einem wieder.
    /// </summary>
    /// <param name="col">Col.</param>
    void OnTriggerExit(Collider col)
    {
        if (DataManager.insertReady && (col.tag.Equals(Config.STRING_PLAYER) || col.tag.Equals(Config.STRING_THIEF)))
        {
            DoorSwitch.isInCollider = false;
            int doorState = dataSet.getState();

            if (doorState == 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_DOOR_CLOSE_IDLE))
            {
                animator.SetTrigger(Config.ANIMATION_TRIGGER_CLOSE);
            }
            openOnTriggerEnter = false;
            passwordField.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Coroutine zum Verändern der Umgebung(Tür öffnen, schließen, Passworteingabe etc.) 
    /// </summary>
    private IEnumerator changeScene()
    {
        while (true)
        {
            if (DataManager.insertReady && Mode.isPlayMode() && !GameobjectLoader.isLoading())
            {
                updateState();
            }
            yield return new WaitForSeconds(Config.FLOAT_REFRESH_INTERVAL);
        }
    }

	/// <summary>
	/// Updates all grids.
	/// </summary>
    private void updateAllGrids()
    {
        foreach (Grid grid in grids)
        {
            grid.registerPath();
        }
    }
}
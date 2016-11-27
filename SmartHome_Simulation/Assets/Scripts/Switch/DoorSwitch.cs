using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class DoorSwitch : MonoBehaviour
{
    public static bool isInCollider = false;
    private GameObject passwordCanvas;
    private InputField passwordField;
    private int id;
    private bool firstStart = false;
    private DoorDataSet dataSet;
    private RequestHandler rh = new RequestHandler();


    // Update is called once per frame
    void Update()
    {
        if (!firstStart && DataManager.insertReady && Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            passwordCanvas = GameObject.Find(Config.OBJ_NAME_CANVAS);

            dataSet = (DoorDataSet) DataManager.getDevice(transform.parent.name, dataSet);
            id = dataSet.getId();
            firstStart = true;
        }
    }

    /// <summary>
    /// Wenn man vor einer Tür steht und diese verschlossen ist, kann man diese mit einem Mausklick öffnen, bzw wenn ein Passwort gesetzt ist, dieses eingeben
    /// </summary>
    void OnMouseDown()
    {
        if (isInCollider && DataManager.insertReady && Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            dataSet = (DoorDataSet) DataManager.getDevice(transform.parent.name, dataSet);
            int status;
            int oldStatus = dataSet.getState();
            string password = dataSet.getPassword();

            if (oldStatus == 1 && password.Equals(Config.STRING_EMPTY))
            {
                status = 0;
                StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.TAG_TABLE_DOOR, Config.TAG_STATE, status.ToString(), id)));
            }
            else if (oldStatus == 1)
            {
                passwordField = passwordCanvas.transform.FindChild(Config.OBJ_PASSWORD_FIELD).GetComponent<InputField>();
                passwordField.text = Config.STRING_EMPTY;
                passwordField.gameObject.SetActive(true);

                EventSystem.current.SetSelectedGameObject(passwordField.gameObject, null);
                passwordField.OnPointerClick(new PointerEventData(EventSystem.current));
                RigidbodyFirstPersonController.movement = false;
            }
        }
    }
}
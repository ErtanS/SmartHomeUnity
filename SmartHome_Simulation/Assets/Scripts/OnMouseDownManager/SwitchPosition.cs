using UnityEngine;
using System.Collections;

public class SwitchPosition : MonoBehaviour
{
    private new Camera camera;
    private ArrayList switchPositions = new ArrayList();
    private const float height = 0.9f;
    public static GameObject currentObject;
    private MessageManager message;
    private Color colorGray = Color.gray;
    private Color colorOriginial;
    private Material material;

    // Use this for initialization
    void Start()
    {
        material = GetComponent<Renderer>().material;
        colorOriginial = material.GetColor(Config.SET_COLOR);
        fillSwitchRects(3);
    }

    /// <summary>
    /// Füllen der List mit Vektor2 Werten
    /// X Wert = Untere Kante
    /// Y Wert = Obere Kante
    /// </summary>
    /// <param name="amount">Anzahl an Vierecken die generiert werden sollen</param>
    private void fillSwitchRects(int amount)
    {
        for (float i = 0; i < amount; i++)
        {
            switchPositions.Add(new Vector2(height + (i*0.15f), height + (i + 1)*0.15f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mode.isPlaceMode() || Mode.isPlaceSwitchMode()) && (message == null || camera == null))
        {
            if (GameObject.Find(Config.OBJ_NAME_CANVAS) != null)
            {
                message = GameObject.Find(Config.OBJ_NAME_CANVAS).GetComponent<MessageManager>();
            }
            if (GameObject.Find(Config.STRING_GAMEOBJECT_CAMERATOP) != null)
            {
                camera = GameObject.Find(Config.STRING_GAMEOBJECT_CAMERATOP).GetComponent<Camera>();
            }
        }
        if (Mode.isPlaceSwitchMode())
        {
            if (!material.GetColor(Config.SET_COLOR).Equals(colorGray))
            {
                material.SetColor(Config.SET_COLOR, colorGray);
            }
        }
        else
        {
            if (!material.GetColor(Config.SET_COLOR).Equals(colorOriginial))
            {
                material.SetColor(Config.SET_COLOR, colorOriginial);
            }
        }
    }

    /// <summary>
    /// Positionieren von Schaltern auf den zuvor definierten Bereichen
    /// </summary>
    void OnMouseDown()
    {
        if (!GameobjectLoader.isLoading())
        {
            if (Mode.isPlaceSwitchMode())
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Casts the ray and get the first game object hit
                Physics.Raycast(ray, out hit);
                bool switchPlaced = false;
                foreach (Vector2 pos in switchPositions)
                {
                    if (hit.point.y >= pos.x && hit.point.y <= pos.y)
                    {
                        string prefab;
                        if (KeyListener.getCurrentDeviceType().Equals(Config.STRING_TYPE_EN_SPEAKER))
                        {
                            prefab = Config.OBJ_NAME_PANEL;
                        }
                        else
                        {
                            prefab = Config.OBJ_NAME_SWITCH;
                        }
                        setSwitch(pos.x, prefab);
                        switchPlaced = true;
                    }
                }
                if (!switchPlaced)
                {
                    message.addMessageToQueue(Config.MSG_SWITCH_ONLY_ON_POLE);
                }
            }
            else if (Mode.isPlaceMode())
            {
                if (KeyListener.getCurrentDeviceType().Equals(Config.STRING_BUTTON_DELETE))
                {
                    message.addMessageToQueue(Config.MSG_CANNOT_DELETE_POLE);
                }
                else
                {
                    message.addMessageToQueue(MessageManager.MSG_DEFAULT);
                }
            }
        }
    }

    /// <summary>
    /// Platzieren des Schalters und Ermittlung der Rotation um es an der Wand zu platzieren
    /// </summary>
    /// <param name="pos">Position</param>
    /// <param name="prefab">Prefab</param>
    private void setSwitch(float pos, string prefab)
    {
        int angle = -1;
        float x = -1;
        float z = -1;

        if (transform.rotation.eulerAngles.y == 180)
        {
            angle = 270;
        }
        else if (transform.rotation.eulerAngles.y == 0)
        {
            angle = 90;
        }
        else if (transform.rotation.eulerAngles.y == 270)
        {
            angle = 0;
        }
        else if (transform.rotation.eulerAngles.y == 90)
        {
            angle = 180;
        }

        x = transform.position.x;
        z = transform.position.z;
        Vector3 position = new Vector3(x, pos + 0.075f, z);
        if (!hasSwitch(position))
        {
            GameObject schalter = Instantiate(GameobjectLoader.getPrefab(prefab));
            schalter.transform.position = position;
            schalter.transform.Rotate(new Vector3(0, angle, 0));
            schalter.name = Config.OBJ_NAME_SWITCH + "_" + transform.parent.name + "_" + transform.name + "_" + pos;
            schalter.transform.SetParent(currentObject.transform);
            Mode.changeToPlaceMode();
        }
        else
        {
            message.addMessageToQueue(Config.MSG_ERROR_ALREADY_HAS_SWITCH);
        }
    }

    /// <summary>
    /// Prüfen ob die Position bereits ein Schalter hat
    /// </summary>
    /// <param name="position">Position</param>
    /// <returns>
    /// true = besitzt Schalter
    /// false = besitzt keinen Schalter
    /// </returns>
    private bool hasSwitch(Vector3 position)
    {
        ArrayList switchs = new ArrayList();
        switchs.AddRange(GameObject.FindGameObjectsWithTag(Config.OBJ_NAME_SWITCH));
        switchs.AddRange(GameObject.FindGameObjectsWithTag(Config.OBJ_NAME_PANEL));

        foreach (GameObject temp in switchs)
        {
            if (temp.transform.position == position)
            {
                return true;
            }
        }
        return false;
    }
}
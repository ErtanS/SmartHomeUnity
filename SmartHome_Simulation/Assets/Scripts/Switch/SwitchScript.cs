using System.Collections;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    private int id;
    private Transform parent;
    private bool firstStart = false;
    private DeviceDataSet dataSet;
    private new string name;
    private RequestHandler rh = new RequestHandler();

    public void Update()
    {
        if (!firstStart && DataManager.insertReady && Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            if (transform.name == Config.STRING_DOORHANDLE)
            {
                parent = transform.parent.parent;
            }
            else
            {
                parent = transform.parent;
            }
            name = parent.name;
            dataSet = DataManager.getDevice(name, dataSet);
            id = dataSet.getId();
            firstStart = true;
        }
    }

    /// <summary>
    /// Beim Klick auf einen Schalter(Licht, Rolladen, Lautsprecher, Fenster) wird der Status der jeweiligen Gerätes geändert
    /// Der neue Status wird in die Datenbank eingefügt.
    /// </summary>
    void OnMouseDown()
    {
        if (Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            dataSet = DataManager.getDevice(name, dataSet);
            int status = dataSet.getState();

            status++;

            if (status > 1)
            {
                status = 0;
            }
            if (parent.tag.Equals(Config.STRING_PREFAB_KITCHEN_SINK) || parent.tag.Equals(Config.STRING_PREFAB_SINK) ||
                parent.tag.Equals(Config.STRING_PREFAB_TUB))
            {
                StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_WATER, Config.TAG_STATE, status.ToString(), id)));
            }
            else
            {
                StartCoroutine(rh.makeRequest(rh.updateDatabase(parent.tag, Config.TAG_STATE, status.ToString(), id)));
            }
        }
    }
}
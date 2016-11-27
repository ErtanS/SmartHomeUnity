using UnityEngine;
using System.Collections;

public class StoveManager : MonoBehaviour
{
    private int oldStatus = -1;
    private int oldDuration = -1;
    private int oldTemperature = -1;
    private Material material;
    private Texture2D texture;
    private Color color;
    private int hour = -1;
    private int minute = -1;
    private int id;
    private StoveDataSet dataSet;
    private bool firstStart = false;

    /// <summary>
    /// Update this instance.
    /// </summary>
    public void Update()
    {
        if (DataManager.insertReady && !firstStart && Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            firstStart = true;
            dataSet = (StoveDataSet)DataManager.getDevice(name, dataSet);
            id = dataSet.getId();
            material = transform.FindChild(Config.OBJ_STOVE_PLATTE).GetComponent<Renderer>().material;
            color = material.color;

            StartCoroutine(changeScene());
        }
    }

    /// <summary>
    /// Aktualisiert die Eigenschaften der Rollladen
    /// Fährt die Rollladen hoch(status = 0)
    /// Föhrt die Rolladen runter (status = 1)
    /// </summary>
    private void updateState()
    {
        dataSet = (StoveDataSet)DataManager.getDevice(name, dataSet);
        int status = dataSet.getState();
        int temperature = dataSet.getTemperature();
        int duration = dataSet.getDuration();

        if (status == 1 && status != oldStatus)
        {
            material.SetColor(Config.SET_COLOR, Color.red);
        }

        if (status == 0 && status != oldStatus)
        {
            material.SetColor(Config.SET_COLOR, color);
            hour = -1;
            minute = -1;
        }
        if (status == 1 && duration != 0 && (status != oldStatus || duration != oldDuration))
        {
            hour = Clock.hour;
            minute = Clock.minute;
            print("start zeit" + hour + " : " + minute);
            int temphour = (minute + duration) / 60;
            hour = (hour + temphour) % 24;
            minute = (minute + duration) % 60;
            print("berechnete zeit" + hour + " : " + minute);
        }
        if (Clock.hour == hour && Clock.minute - minute > 0 && Clock.minute - minute <= 10 && status == 1 && duration != 0)
        {
            hour = -1;
            minute = -1;
            RequestHandler rh = new RequestHandler();
            StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_STOVE, Config.TAG_STATE, Config.STRING_STATUS_AUS, id)));
            StartCoroutine(rh.makeRequest(rh.pushFirebase(Config.STRING_MESSAGE_STOVE + name)));
            print("push notification");
        }
        oldDuration = duration;
        oldStatus = status;
        oldTemperature = temperature;
    }

    /// <summary>
    /// Coroutine zum Verändern der Umgebung(Rolladen hoch/runter) 
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
}
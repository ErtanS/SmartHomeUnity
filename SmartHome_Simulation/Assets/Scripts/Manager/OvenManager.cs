using UnityEngine;
using System.Collections;

public class OvenManager : MonoBehaviour
{
    private int oldStatus = -1;
    private int oldDuration = -1;
    private int oldTemperature = -1;
    private GameObject led;
    private int hour = -1;
    private int minute = -1;
    private int id;
    private OvenDataSet dataSet;
    private bool firstStart = false;

    // Initialisation
    public void Update()
    {
        if (DataManager.insertReady && !firstStart && Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            firstStart = true;
            dataSet = (OvenDataSet)DataManager.getDevice(name, dataSet);
            id = dataSet.getId();
            led = transform.FindChild(Config.STRING_LED).gameObject;
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
        dataSet = (OvenDataSet)DataManager.getDevice(name, dataSet);
        int status = dataSet.getState();
        int temperature = dataSet.getTemperature();
        int duration = dataSet.getDuration();

        if (status == 1 && status != oldStatus)
        {
            led.SetActive(true);
        }


        if (status == 0 && status != oldStatus)
        {
            led.SetActive(false);
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
        if (Clock.hour == hour && 0 < Clock.minute - minute && Clock.minute - minute <= 10 && status == 1 && duration != 0)
        {
            hour = -1;
            minute = -1;
            RequestHandler rh = new RequestHandler();
            StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_OVEN, Config.TAG_STATE, Config.STRING_STATUS_AUS, id)));
            StartCoroutine(rh.makeRequest(rh.pushFirebase(Config.STRING_MESSAGE_OVEN + name)));
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
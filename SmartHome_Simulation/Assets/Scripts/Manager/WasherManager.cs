using UnityEngine;
using System.Collections;

public class WasherManager : MonoBehaviour
{
    private int oldStatus = -1;
    private int oldDuration = -1;
    private Animator animator;
    private bool waitUP = false;
    private bool waitDOWN = false;
    private GameObject led;
    private int hour = -1;
    private int minute = -1;
    private int id;
    private WasherDataSet dataSet;
    private bool firstStart = false;

    // Initialisation
    public void Update()
    {
        if (DataManager.insertReady && !firstStart && Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            firstStart = true;
            dataSet = (WasherDataSet)DataManager.getDevice(name, dataSet);
            id = dataSet.getId();
            animator =
                transform.FindChild(Config.STRING_BODY).FindChild(Config.OBJ_WASHER_TROMMEL).GetComponent<Animator>();
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
        dataSet = (WasherDataSet)DataManager.getDevice(name, dataSet);
        int status = dataSet.getState();
        int temperature = dataSet.getTemperature();
        int duration = dataSet.getDuration();
        int rpm = dataSet.getRpm();
        int amount = dataSet.getAmount();
        int clothes = dataSet.getClothes();

        if (status == 0 && !waitUP && (animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_WASHER_ON)))
        {
            led.SetActive(false);
            animator.SetTrigger(Config.ANIMATION_TRIGGER_OFF);
            waitUP = true;
            waitDOWN = false;
            hour = -1;
            minute = -1;
        }
        if (status == 1 && !waitDOWN && (animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_WASHER_OFF)))
        {
            led.SetActive(true);
            animator.SetTrigger(Config.ANIMATION_TRIGGER_ON);
            waitUP = false;
            waitDOWN = true;
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
            RequestHandler rh = new RequestHandler();
            StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_WASHER, Config.TAG_STATE, Config.STRING_STATUS_AUS, id)));
            StartCoroutine(rh.makeRequest(rh.pushFirebase(Config.STRING_MESSAGE_WASHER + name)));
            hour = -1;
            minute = -1;
            print("push notification");
        }
        oldDuration = duration;
        oldStatus = status;
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
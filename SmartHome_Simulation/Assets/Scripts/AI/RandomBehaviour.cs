using UnityEngine;
using System;
using System.Collections;

public class RandomBehaviour : MonoBehaviour
{
    private GameObject targetObject;
    private bool isFollowing = false;
    private Pathfinding pathFinding;
    private GameObject player;
    private int counter = 1;
    private ArrayList targets = new ArrayList();
    private int waitSeconds = 3;
    private float deltaTime;
    private Vector3 oldPosition;
    private IEnumerator coroutine;
    private Grid grid;
    private bool stopped = false;
    private int hour = -1;
    private int minute = -1;
    private RequestHandler rh = new RequestHandler();
    private ArrayList devices = new ArrayList();
    private ArrayList hourList = new ArrayList();
    private ArrayList minuteList = new ArrayList();

    void Start()
    {
        if (Convert.ToInt32(transform.parent.name) > SettingsNavigation.numberOfPeople)
        {
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            pathFinding = transform.parent.GetComponent<Pathfinding>();
            player = transform.parent.FindChild(Config.OBJ_NAME_RANDOM_PLAYER).gameObject;
            grid = transform.parent.GetComponent<Grid>();
            coroutine = checkIfTargetIsMoving();
        }
    }

    void Update()
    {
        if (devices.Count > 0 && hourList.Count > 0 && minuteList.Count > 0)
        {
            StartCoroutine(turnDeviceOffIE(devices[0] as DeviceDataSet, (int) hourList[0], (int) minuteList[0]));
            devices.RemoveAt(0);
            hourList.RemoveAt(0);
            minuteList.RemoveAt(0);
        }

        if (Mode.isPlayMode() && !isFollowing && !GameobjectLoader.isLoading())
        {
            if ((hour == -1 || hour == Clock.hour) && (minute == -1 || minute == Clock.minute))
            {
                isFollowing = true;
                stopped = false;
                hour = -1;
                minute = -1;
                StartCoroutine(findTarget());
            }
        }
        if (!Mode.isPlayMode() && !stopped && !GameobjectLoader.isLoading())
        {
            reset();
            stopped = true;
        }
    }

    /// <summary>
    /// Überprüfung ob sich das Zielobjekt bewegt
    /// </summary>
    /// <returns></returns>
    private IEnumerator checkIfTargetIsMoving()
    {
        deltaTime = 0;
        oldPosition = transform.position;
        while (true)
        {
            if (targets != null)
            {
                deltaTime += Time.deltaTime;
                if (oldPosition != transform.position)
                {
                    deltaTime = 0;
                    oldPosition = transform.position;
                }
                if (deltaTime > 3)
                {
                    reset();
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// Ermittlung des Ziels
    /// </summary>
    /// <returns></returns>
    private IEnumerator findTarget()
    {
        yield return new WaitForSeconds(5);

        int seed = unchecked(DateTime.Now.Ticks.GetHashCode());
        waitSeconds = new System.Random(seed).Next(3, 11);
        ArrayList floorObjects = new ArrayList();
        foreach (string type in GameobjectLoader.floorObjects)
        {
            if (type.Equals(Config.STRING_TYPE_EN_STOVE))
            {
                foreach (GameObject temp in GameObject.FindGameObjectsWithTag(type))
                {
                    if (temp.transform.childCount == 3 && (targetObject == null || !temp.name.Equals(targetObject.name)))
                    {
                        floorObjects.Add(temp);
                    }
                }
            }
            else
            {
                foreach (GameObject temp in GameObject.FindGameObjectsWithTag(type))
                {
                    if (targetObject == null || !temp.name.Equals(targetObject.name))
                    {
                        floorObjects.Add(temp);
                        floorObjects.Add(temp);
                    }
                }
            }
        }

        foreach (GameObject temp in GameObject.FindGameObjectsWithTag(Config.STRING_TYPE_EN_LIGHT))
        {
            if (targetObject == null || !temp.name.Equals(targetObject.name))
            {
                floorObjects.Add(temp);
            }
        }
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag(Config.STRING_PREFAB_SINK))
        {
            if (targetObject == null || !temp.name.Equals(targetObject.name))
            {
                floorObjects.Add(temp);
            }
        }
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag(Config.STRING_TYPE_EN_SPEAKER))
        {
            if (targetObject == null || !temp.name.Equals(targetObject.name))
            {
                floorObjects.Add(temp);
            }
        }
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag(Config.STRING_TYPE_EN_WINDOW))
        {
            if (targetObject == null || !temp.name.Equals(targetObject.name))
            {
                floorObjects.Add(temp);
            }
        }
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag(Config.STRING_TYPE_EN_SHUTTERS))
        {
            if (targetObject == null || !temp.name.Equals(targetObject.name))
            {
                floorObjects.Add(temp);
            }
        }

        if (floorObjects != null && floorObjects.Count > 0)
        {
            seed = unchecked(DateTime.Now.Ticks.GetHashCode());
            int targetId = new System.Random(seed).Next(floorObjects.Count);
            targetObject = floorObjects[targetId] as GameObject;
            GameObject tempTarget = targetObject;
            if (targetObject.tag.Equals(Config.STRING_TYPE_EN_SHUTTERS) ||
                targetObject.tag.Equals(Config.STRING_TYPE_EN_WINDOW))
            {
                foreach (Transform trans in targetObject.transform.parent.parent.GetComponentInChildren<Transform>())
                {
                    if (trans.tag.Equals(Config.STRING_PREFAB_FLOOR))
                    {
                        tempTarget = trans.gameObject;
                        break;
                    }
                }
            }
            print(targetObject.name);
            grid.setTarget(tempTarget);
            pathFinding.setStart(player.transform);
            pathFinding.setTarget(tempTarget.transform);
            yield return null;
            startFollowing();
        }
        else
        {
            isFollowing = false;
        }
        yield return null;
    }

    /// <summary>
    /// Setter isFollowing
    /// </summary>
    /// <param name="following"></param>
    public void setFollowing(bool following)
    {
        isFollowing = following;
    }

    /// <summary>
    /// Starten der Verfolgung des Targets
    /// </summary>
    public void startFollowing()
    {
        StartCoroutine(follow());
    }

    /// <summary>
    /// IENumerator zum verfolgen des Zielobjekts
    /// </summary>
    /// <returns></returns>
    public IEnumerator follow()
    {
        float waitTime = 0;
        targets = pathFinding.getTargets();
        yield return new WaitForSeconds(1);
        while (targets == null && waitTime < 10)
        {
            targets = pathFinding.getTargets();
            waitTime += Time.deltaTime;
            yield return null;
        }

        if (waitTime < 10)
        {
            player.SetActive(true);
            StartCoroutine(coroutine);
        }
        else
        {
            reset();
        }
        yield return null;
    }

    /// <summary>
    /// Zurücksetzen
    /// </summary>
    private void reset()
    {
        print("reset");
        StopCoroutine(coroutine);
        transform.position = player.transform.position;
        targets = null;
        pathFinding.resetPath();
        pathFinding.stopSearching();
        counter = 1;
        setFollowing(false);
        player.SetActive(false);
    }

    
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject == player && targets != null)
        {
            if (counter < targets.Count)
            {
                transform.position = (Vector3) targets[counter++];
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player && targets != null && counter == targets.Count)
        {
            string name = "";
            if (targetObject.tag.Equals(Config.STRING_TYPE_EN_STOVE))
            {
                int seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                int child = new System.Random(seed).Next(4);
                name = targetObject.transform.FindChild(Config.STRING_BODY).GetChild(child).name;
            }
            else
            {
                name = targetObject.name;
            }
            switchDevice(name, targetObject.tag);
            reset();
        }
    }

    public void switchDevice(string name, string type)
    {
        int seed;
        switch (type)
        {
            case Config.STRING_TYPE_EN_DRYER:
                DryerDataSet dataSet = DataManager.getDevice(name, null) as DryerDataSet;
                if (dataSet != null && dataSet.getState() == 0)
                {
                    int duration = switchDryerWasher(Config.STRING_TYPE_EN_DRYER, dataSet);
                    int hour = duration/60;
                    int minute = duration%60;
                    this.hour = Clock.hour + hour;
                    this.minute = Clock.minute + minute;
                    calcCurrentTime();
                }
                break;

            case Config.STRING_TYPE_EN_LIGHT:
                seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                string color = String.Format("#{0:X6}", new System.Random(seed).Next(0x1000000));
                LightDataSet light = DataManager.getDevice(name, null) as LightDataSet;
                StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_LIGHT, Config.TAG_COLOR, color, light.getId())));
                StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_LIGHT, Config.TAG_STATE, Config.STRING_STATUS_EIN, light.getId())));
                this.hour = Clock.hour;
                this.minute = Clock.minute + 20;
                calcCurrentTime();
                turnDeviceOff(light, this.hour + 5, this.minute);
                break;

            case Config.STRING_TYPE_EN_OVEN:

                OvenDataSet ovenDataSet = DataManager.getDevice(name, null) as OvenDataSet;
                if (ovenDataSet != null && ovenDataSet.getState() == 0)
                {
                    seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                    int temperature = new System.Random(seed).Next(276);
                    seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                    int ovenDuration = new System.Random(seed).Next(360);
                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_OVEN, Config.TAG_DURATION, ovenDuration.ToString(),
                        ovenDataSet.getId())));
                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_OVEN, Config.TAG_TEMPERATURE, temperature.ToString(),
                        ovenDataSet.getId())));
                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_OVEN, Config.TAG_STATE, Config.STRING_STATUS_EIN,
                        ovenDataSet.getId())));

                    int ovenHour = ovenDuration/60;
                    int ovenMinute = ovenDuration%60;
                    this.hour = Clock.hour + ovenHour;
                    this.minute = Clock.minute + ovenMinute;
                    calcCurrentTime();
                }

                break;
            case Config.STRING_TYPE_EN_PC:
                PcDataSet pc = DataManager.getDevice(name, null) as PcDataSet;
                if (pc != null && pc.getState() == 0)
                {
                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_PC, Config.TAG_STATE, Config.STRING_STATUS_EIN, pc.getId())));
                    this.hour = Clock.hour + 1;
                    this.minute = Clock.minute;
                    calcCurrentTime();
                    turnDeviceOff(pc, this.hour, this.minute);
                }

                break;
            case Config.STRING_TYPE_EN_SHUTTERS:
                ShuttersDataSet shutters = DataManager.getDevice(name, null) as ShuttersDataSet;

                int state = shutters.getState();
                int nextState = (state + 1)%2;
                StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_SHUTTERS, Config.TAG_STATE, nextState.ToString(),
                    shutters.getId())));
                this.hour = Clock.hour;
                this.minute = Clock.minute + 10;
                calcCurrentTime();
                break;
            case Config.STRING_TYPE_EN_SPEAKER:
                seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                int songid = new System.Random(seed).Next(5);
                SpeakerDataSet speaker = DataManager.getDevice(name, null) as SpeakerDataSet;
                StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_SPEAKER, Config.TAG_SONGID, songid.ToString(), speaker.getId())));
                StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_SPEAKER, Config.TAG_STATE, Config.STRING_STATUS_EIN,
                    speaker.getId())));
                this.hour = Clock.hour;
                this.minute = Clock.minute + 20;
                calcCurrentTime();
                turnDeviceOff(speaker, this.hour + 5, this.minute);
                break;

            case Config.STRING_TYPE_EN_STOVE:

                StoveDataSet stove = DataManager.getDevice(name, null) as StoveDataSet;
                if (stove != null && stove.getState() == 0)
                {
                    seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                    int stoveTemp = new System.Random(seed).Next(276);
                    seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                    int stoveDuration = new System.Random(seed).Next(360);

                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_STOVE, Config.TAG_DURATION, stoveDuration.ToString(),
                        stove.getId())));
                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_STOVE, Config.TAG_TEMPERATURE, stoveTemp.ToString(),
                        stove.getId())));
                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_STOVE, Config.TAG_STATE, Config.STRING_STATUS_EIN,
                        stove.getId())));

                    int stoveHour = stoveDuration/60;
                    int stoveMinute = stoveDuration%60;
                    this.hour = Clock.hour + stoveHour;
                    this.minute = Clock.minute + stoveMinute;
                    calcCurrentTime();
                }
                break;
            case Config.STRING_TYPE_EN_TV:
                TvDataSet tv = DataManager.getDevice(name, null) as TvDataSet;
                if (tv != null && tv.getState() == 0)
                {
                    seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                    int channelId = new System.Random(seed).Next(6);
                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_TV, Config.TAG_CHANNEL, channelId.ToString(), tv.getId())));
                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_TV, Config.TAG_STATE, Config.STRING_STATUS_EIN, tv.getId())));
                    this.hour = Clock.hour + 3;
                    this.minute = Clock.minute;
                    calcCurrentTime();
                    turnDeviceOff(tv, this.hour, this.minute);
                }
                break;

            case Config.STRING_TYPE_EN_WASHER:
                WasherDataSet washer = DataManager.getDevice(name, null) as WasherDataSet;
                if (washer != null && washer.getState() == 0)
                {
                    int washerDuration = switchDryerWasher(Config.STRING_TYPE_EN_WASHER, washer);
                    int washerHour = washerDuration/60;
                    int washerMinute = washerDuration%60;
                    this.hour = Clock.hour + washerHour;
                    this.minute = Clock.minute + washerMinute;
                    calcCurrentTime();
                }
                break;

            case Config.STRING_PREFAB_TUB:
            case Config.STRING_PREFAB_KITCHEN_SINK:
            case Config.STRING_PREFAB_SINK:
                WaterDataSet water = DataManager.getDevice(name, null) as WaterDataSet;
                if (water != null && water.getState() == 0)
                {
                    StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_WATER, Config.TAG_STATE, Config.STRING_STATUS_EIN,
                        water.getId())));
                    this.hour = Clock.hour + 1;
                    this.minute = Clock.minute + 30;
                    calcCurrentTime();
                    turnDeviceOff(water, this.hour, this.minute);
                }
                break;

            case Config.STRING_TYPE_EN_WINDOW:
                WindowDataSet window = DataManager.getDevice(name, null) as WindowDataSet;
                int windowState = (window.getState() + 1)%2;
                StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_WINDOW, Config.TAG_STATE, windowState.ToString(), window.getId())));
                this.hour = Clock.hour;
                this.minute = Clock.minute + 30;
                calcCurrentTime();
                break;
        }
    }

    private void calcCurrentTime()
    {
        int balance = minute/60;
        minute = minute%60;
        hour = (hour + balance)%24;
    }

    private void turnDeviceOff(DeviceDataSet device, int hour, int minute)
    {
        devices.Add(device);
        hourList.Add(hour);
        minuteList.Add(minute);
    }

    private IEnumerator turnDeviceOffIE(DeviceDataSet device, int hour, int minute)
    {
        int balance = minute/60;
        minute = minute%60;
        hour = (hour + balance)%24;

        while (true)
        {
            if (Clock.hour == hour && Clock.minute == minute)
            {
                StartCoroutine(rh.makeRequest(rh.updateDatabase(device.getType(), Config.TAG_STATE, Config.STRING_STATUS_AUS, device.getId())));
                break;
            }
            yield return null;
        }
    }

    private int switchDryerWasher(string type, DeviceDataSet dataSet)
    {
        int rpm = 0;
        int temperature = 0;
        int duration = 0;

        int seed = unchecked(DateTime.Now.Ticks.GetHashCode());
        int currentClothes = new System.Random(seed).Next(5);
        seed = unchecked(DateTime.Now.Ticks.GetHashCode());
        int currentAmount = new System.Random(seed).Next(3);
        switch (currentClothes)
        {
            case 0:
            case 1:
                rpm = 1000;
                temperature = 100;
                switch (currentAmount)
                {
                    case 0:
                        duration = 45;
                        break;
                    case 1:
                        duration = 72;
                        break;
                    case 2:
                        duration = 90;
                        break;
                }
                break;
            case 2:
                rpm = 900;
                temperature = 90;
                switch (currentAmount)
                {
                    case 0:
                        duration = 35;
                        break;
                    case 1:
                        duration = 42;
                        break;
                    case 2:
                        duration = 45;
                        break;
                }
                break;
            case 3:
                rpm = 800;
                temperature = 60;
                switch (currentAmount)
                {
                    case 0:
                        duration = 30;
                        break;
                    case 1:
                        duration = 35;
                        break;
                    case 2:
                        duration = 40;
                        break;
                }
                break;
            case 4:
                rpm = 1000;
                temperature = 80;
                switch (currentAmount)
                {
                    case 0:
                        duration = 35;
                        break;
                    case 1:
                        duration = 40;
                        break;
                    case 2:
                        duration = 45;
                        break;
                }
                break;
        }
        RequestHandler rh = new RequestHandler();

        StartCoroutine(rh.makeRequest(rh.updateDatabase(type, Config.TAG_AMOUNT, currentAmount.ToString(), dataSet.getId())));
        StartCoroutine(rh.makeRequest(rh.updateDatabase(type, Config.TAG_CLOTHES, currentClothes.ToString(), dataSet.getId())));
        StartCoroutine(rh.makeRequest(rh.updateDatabase(type, Config.TAG_TEMPERATURE, temperature.ToString(), dataSet.getId())));
        StartCoroutine(rh.makeRequest(rh.updateDatabase(type, Config.TAG_DURATION, duration.ToString(), dataSet.getId())));
        StartCoroutine(rh.makeRequest(rh.updateDatabase(type, Config.TAG_RPM, rpm.ToString(), dataSet.getId())));
        StartCoroutine(rh.makeRequest(rh.updateDatabase(type, Config.TAG_STATE, Config.STRING_STATUS_EIN, dataSet.getId())));
        print("washer ++ " + duration);
        return duration;
    }
}
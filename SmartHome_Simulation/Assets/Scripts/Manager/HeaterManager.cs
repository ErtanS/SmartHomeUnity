using UnityEngine;
using System.Collections;

public class HeaterManager : MonoBehaviour
{
    public static double realTemperature = 15;
    private double currentTemperature = 15;
    private int oldStatus = -1;
    private double oldTemperature = -1;
    private GameObject roomTag;

    private float timeleft = 3;
    private IEnumerator coroutine;
    private HeaterDataSet dataSet;

    // Use this for initialization
    void Start()
    {
        foreach (
            Transform trans in GameObject.Find(Config.STRING_GAMEOBJECT_MODES).GetComponentsInChildren<Transform>(true))
        {
            if (trans.tag.Equals(Config.OBJ_CURRENT_ROOM))
            {
                roomTag = trans.gameObject;
            }
        }
        StartCoroutine(changeScene());
    }

    /// <summary>
    /// Aktualisert die Temperatur im Raum abhängig davon,
    /// ob Heizung an ist(status = 1) 
    /// und eingestellter Temperatur
    /// </summary>
    private void updateState()
    {
        dataSet = (HeaterDataSet) DataManager.getDevice(name, dataSet);
        int status = dataSet.getState();
        double temperature = dataSet.getTemperature();
        double temperatureDifference = temperature - currentTemperature;

        if (status == Config.INT_STATUS_EIN && (oldStatus != status || temperature != oldTemperature))
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            calculateHeating(temperatureDifference);
        }

        if (status == Config.INT_STATUS_AUS && oldStatus != status)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            calculateHeating(Config.DOUBLE_STANDARD_TEMPERATURE - currentTemperature);
        }

        if (roomTag.name.Equals(dataSet.getScenarioRoom()))
        {
            realTemperature = currentTemperature;
        }
        oldStatus = status;
        oldTemperature = temperature;
    }

    /// <summary>
    /// Regelt die Temperatur nach gegebener Temperaturdifferenz und Faktor hoch bzw. runter
    /// Versuch der Annäherung an die Realität
    /// </summary>
    /// <param name="difference">Temperaturdifferenz</param>
    /// <param name="factor">Faktor</param>
    IEnumerator timer(double difference, int factor)
    {
        float time = (float) (timeleft*difference);
        int i = 1;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (time < timeleft*(difference - i))
            {
                currentTemperature += factor;
                i++;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Berechnung der neuen Temperatur
    /// </summary>
    /// <param name="difference">Differnez der eingestellten Temperatur zur tatsächlichen Temperatur</param>
    private void calculateHeating(double difference)
    {
        if (difference > 0)
        {
            coroutine = timer(difference, 1);
        }
        else if (difference < 0)
        {
            coroutine = timer(difference*-1, -1);
        }
        if (coroutine != null)
        {
            StartCoroutine(coroutine);
        }
    }

    /// <summary>
    /// Coroutine zum Verändern der Umgebung(Heizung ein/aus, Temperatur anpassen) 
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
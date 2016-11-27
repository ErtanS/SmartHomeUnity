using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;

public class Clock : MonoBehaviour
{
    public static float timeSpeed = 0.05f;
    public static int hour = 0;
    public static int minute = 0;
    private bool firstStart = false;
    private DataSet dataSet;

    // Update is called once per frame
    void Update()
    {
        if (!firstStart && DataManager.insertReady && !GameobjectLoader.isLoading())
        {
            StartCoroutine(timer());
            firstStart = true;
        }
    }

    /// <summary>
    /// Ablauf der Uhr zum zeitgesteuerten Schalten von Geräten
    /// </summary>
    private IEnumerator timer()
    {
        while (true)
        {
            if (Mode.isPlayMode() && !GameobjectLoader.isLoading())
            {
                if (minute++ == 59)
                {
                    minute = 0;

                    if (hour++ == 23)
                    {
                        hour = 0;
                    }
                }
                StartCoroutine(deviceUpdate(hour, minute));
            }
            yield return new WaitForSeconds(timeSpeed);
        }
    }

    /// <summary>
    /// Schaltet Geräte zu übergebener Zeit
    /// </summary>
    /// <param name="hour">Stunde</param>
    /// <param name="minute">Minute</param>
    private IEnumerator deviceUpdate(int hour, int minute)
    {
        if (DataManager.isTimestamp(hour, minute))
        {
            RequestHandler rh = new RequestHandler();
            yield return StartCoroutine(rh.makeRequest(rh.updateTimestamp(hour, minute)));
        }
        yield return null;
    }
}
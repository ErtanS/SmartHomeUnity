using UnityEngine;
using System.Collections;

public class StoveDataSet : DeviceDataSet
{
    private int duration;
    private int temperature;

    /// <summary>
    /// Instanziert eine neue Instanz eines StoveDataSets
    /// </summary>
    /// <param name="values"></param>
    /// <param name="temperature"></param>
    /// <param name="duration"></param>
    public StoveDataSet(DeviceDataSet values, int temperature, int duration) : base(values)
    {
        this.temperature = temperature;
        this.duration = duration;
    }

    /// <summary>
    /// Get Dauer
    /// </summary>
    /// <returns></returns>
    public int getDuration()
    {
        return duration;
    }

    /// <summary>
    /// Get Temperatur
    /// </summary>
    /// <returns></returns>
    public int getTemperature()
    {
        return temperature;
    }
}
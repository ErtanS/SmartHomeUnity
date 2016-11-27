using UnityEngine;
using System.Collections;

public class OvenDataSet : DeviceDataSet
{
    private int temperature;
    private int duration;

    /// <summary>
    /// Instanziert eine neue Instanz eines OvenDataSets
    /// </summary>
    /// <param name="values"></param>
    /// <param name="temperature"></param>
    /// <param name="duration"></param>
    public OvenDataSet(DeviceDataSet values, int temperature, int duration) : base(values)
    {
        this.temperature = temperature;
        this.duration = duration;
    }

    /// <summary>
    /// Get Temperature
    /// </summary>
    /// <returns></returns>
    public int getTemperature()
    {
        return temperature;
    }

    /// <summary>
    /// Get Dauer
    /// </summary>
    /// <returns></returns>
    public int getDuration()
    {
        return duration;
    }
}
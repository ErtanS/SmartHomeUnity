using UnityEngine;
using System.Collections;

public class WaterDataSet : DeviceDataSet
{
    private int intensity;
    private int temperature;

    /// <summary>
    /// Instanziert eine neue Instanz eines WaterDataSets
    /// </summary>
    /// <param name="values"></param>
    /// <param name="intensity"></param>
    /// <param name="temperature"></param>
    public WaterDataSet(DeviceDataSet values, int intensity, int temperature) : base(values)
    {
        this.intensity = intensity;
        this.temperature = temperature;
    }

    /// <summary>
    /// Get Intensität
    /// </summary>
    /// <returns></returns>
    public int getIntensity()
    {
        return intensity;
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
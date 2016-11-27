using UnityEngine;
using System.Collections;
using System;

public class HeaterDataSet : DeviceDataSet
{
    private int temperature;

    /// <summary>
    /// Initializes a new instance of the <see cref="HeaterDataSet"/> class.
    /// </summary>
    /// <param name="values">Grunddaten</param>
    /// <param name="temperature">Temperatur</param>
    public HeaterDataSet(DeviceDataSet values, int temperature) : base(values)
    {
        this.temperature = temperature;
    }

    /// <summary>
    /// Gets the temperature.
    /// </summary>
    /// <returns>The temperature.</returns>
    public int getTemperature()
    {
        return temperature;
    }
}
using UnityEngine;
using System.Collections;

public class DryerDataSet : DeviceDataSet
{
    private int temperature;
    private int duration;
    private int rpm;
    private int amount;
    private int clothes;

    /// <summary>
    /// Instanziert eine neue Instanz eines DryerDataSet
    /// </summary>
    /// <param name="values"></param>
    /// <param name="temperature"></param>
    /// <param name="duration"></param>
    /// <param name="rpm"></param>
    /// <param name="amount"></param>
    /// <param name="clothes"></param>
    public DryerDataSet(DeviceDataSet values, int temperature, int duration, int rpm, int amount, int clothes)
        : base(values)
    {
        this.temperature = temperature;
        this.duration = duration;
        this.rpm = rpm;
        this.amount = amount;
        this.clothes = clothes;
    }

    /// <summary>
    /// Get Temperatur 
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

    /// <summary>
    /// Get Umdrehnungen pro Minute
    /// </summary>
    /// <returns></returns>
    public int getRpm()
    {
        return rpm;
    }

    /// <summary>
    /// Get Menge
    /// </summary>
    /// <returns></returns>
    public int getAmount()
    {
        return amount;
    }

    /// <summary>
    /// Get Wäschetyp
    /// </summary>
    /// <returns></returns>
    public int getClothes()
    {
        return clothes;
    }
}
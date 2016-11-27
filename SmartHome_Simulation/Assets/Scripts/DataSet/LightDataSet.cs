using System;

public class LightDataSet : DeviceDataSet
{
    private String color;
    private int intensity;

    /// <summary>
    /// Initializes a new instance of the <see cref="LightDataSet"/> class.
    /// </summary>
    /// <param name="values">Grunddaten</param>
    /// <param name="color">Farbe</param>
    /// <param name="intensity">Helligkeit</param>
    public LightDataSet(DeviceDataSet values, String color, int intensity) : base(values)
    {
        this.color = color;
        this.intensity = intensity;
    }

    /// <summary>
    /// Gets the color.
    /// </summary>
    /// <returns>The color.</returns>
    public String getColor()
    {
        return color;
    }

    /// <summary>
    /// Gets the intensity.
    /// </summary>
    /// <returns>The intensity.</returns>
    public int getIntensity()
    {
        return intensity;
    }
}
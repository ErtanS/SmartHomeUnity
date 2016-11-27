using UnityEngine;
using System.Collections;

public class WallDataSet : DeviceDataSet
{
    private string color;
    private string pictureid;

    /// <summary>
    /// Instanziert eine neue Instanz eines WallDataSets
    /// </summary>
    /// <param name="values"></param>
    /// <param name="color"></param>
    /// <param name="pictureid"></param>
    public WallDataSet(DeviceDataSet values, string color, string pictureid) : base(values)
    {
        this.color = color;
        this.pictureid = pictureid;
    }

    /// <summary>
    /// Get Color
    /// </summary>
    /// <returns>Farbe in Hexadezimal</returns>
    public string getColor()
    {
        return color;
    }

    /// <summary>
    /// Get PictureId
    /// </summary>
    /// <returns></returns>
    public string getPictureid()
    {
        return pictureid;
    }
}
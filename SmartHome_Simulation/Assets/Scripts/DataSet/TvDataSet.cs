using UnityEngine;
using System.Collections;

public class TvDataSet : DeviceDataSet
{
    private int channel;
    private string pictureid;
    private int volume;

    /// <summary>
    /// Instanziert eine neue Instanz eines TvDataSets
    /// </summary>
    /// <param name="values"></param>
    /// <param name="channel"></param>
    /// <param name="pictureid"></param>
    /// <param name="volume"></param>
    public TvDataSet(DeviceDataSet values, int channel, string pictureid, int volume) : base(values)
    {
        this.channel = channel;
        this.pictureid = pictureid;
        this.volume = volume;
    }

    /// <summary>
    /// Get Tv-Kanal
    /// </summary>
    /// <returns></returns>
    public int getChannel()
    {
        return channel;
    }

    /// <summary>
    /// Get PictureId
    /// </summary>
    /// <returns></returns>
    public string getPictureid()
    {
        return pictureid;
    }

    /// <summary>
    /// Get Lautstärke
    /// </summary>
    /// <returns></returns>
    public int getVolume()
    {
        return volume;
    }
}
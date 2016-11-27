using UnityEngine;
using System.Collections;

public class PcDataSet : DeviceDataSet
{
    private int videoid;
    private string pictureid;
    private int volume;

    /// <summary>
    /// Instanziert eine neue Instanz eines PcDataSets
    /// </summary>
    /// <param name="values"></param>
    /// <param name="videoid"></param>
    /// <param name="pictureid"></param>
    /// <param name="volume"></param>
    public PcDataSet(DeviceDataSet values, int videoid, string pictureid, int volume) : base(values)
    {
        this.videoid = videoid;
        this.pictureid = pictureid;
        this.volume = volume;
    }

    /// <summary>
    /// Get VideoId
    /// </summary>
    /// <returns></returns>
    public int getVideoid()
    {
        return videoid;
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
using System;

public class SpeakerDataSet : DeviceDataSet
{
    private int volume;
    private int songid;
    private int stop;

    /// <summary>
    /// Initializes a new instance of the <see cref="SpeakerDataSet"/> class.
    /// </summary>
    /// <param name="values">Grunddaten</param>
    /// <param name="volume">Lautstärke</param>
    /// <param name="songid">iId des Songs</param>
    /// <param name="stop">Status, ob Song neu beginnen soll</param>
    public SpeakerDataSet(DeviceDataSet values, int volume, int songid, int stop) : base(values)
    {
        this.volume = volume;
        this.songid = songid;
        this.stop = stop;
    }

    /// <summary>
    /// Gets the volume.
    /// </summary>
    /// <returns>The volume.</returns>
    public int getVolume()
    {
        return volume;
    }

    /// <summary>
    /// Gets the songid.
    /// </summary>
    /// <returns>The songid.</returns>
    public int getSongid()
    {
        return songid;
    }

    /// <summary>
    /// Gets the stop.
    /// </summary>
    /// <returns>The stop.</returns>
    public int getStop()
    {
        return stop;
    }
}
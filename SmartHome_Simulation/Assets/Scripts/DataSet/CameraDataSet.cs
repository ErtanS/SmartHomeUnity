using UnityEngine;
using System.Collections;

public class CameraDataSet : DeviceDataSet
{
    private int emergency;
    private int autoEmergency;
    private int frequency;

    /// <summary>
    /// Instanziert eine neue Instanz eines CameraDataSets 
    /// </summary>
    /// <param name="values"> Grunddaten</param>
    /// <param name="emergency">Id des EmergencyDataSets</param>
    /// <param name="autoEmergency">
    /// Automatisches aktivieren des Notfallszenarios 
    /// 0 = Nicht aktivieren
    /// 1 = Automatisiertes Starten
    /// </param>
    /// <param name="frequency">Intervall in dem die Fotos geschossen werden sollen</param>
    public CameraDataSet(DeviceDataSet values, int emergency, int autoEmergency, int frequency) : base(values)
    {
        this.emergency = emergency;
        this.autoEmergency = autoEmergency;
        this.frequency = frequency;
    }

    /// <summary>
    /// Get NotfallSzenario
    /// </summary>
    /// <returns></returns>
    public int getEmergency()
    {
        return emergency;
    }

    /// <summary>
    /// Get Wert für Automatisiertes starten des Notfallszenarios
    /// </summary>
    /// <returns></returns>
    public int getAutoEmergency()
    {
        return autoEmergency;
    }

    /// <summary>
    /// Get Fotointervall 
    /// </summary>
    /// <returns></returns>
    public int getFrequency()
    {
        return frequency;
    }
}
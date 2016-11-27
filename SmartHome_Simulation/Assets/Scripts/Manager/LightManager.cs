using UnityEngine;
using System;
using System.Collections;

public class LightManager : MonoBehaviour
{
    private int oldStatus = -1;
    private int oldIntensity = -1;
    private string oldColor = "";
    private LightDataSet dataSet;
    private ArrayList lampen;

    // Initialisation
    public void Start()
    {
        lampen = getLights();
        StartCoroutine(changeScene());
    }

    /// <summary>
    /// Aktualisiert die Eigenschaften der Lampe.
    /// Schaltet die Lampe an (status = 1) bzw. aus (status = 0)
    /// verändert die Helligkeit und Farbe je nach Einstellung
    /// </summary>
    private void updateState()
    {
        dataSet = (LightDataSet) DataManager.getDevice(name, dataSet);
        int currentStatus = dataSet.getState();
        int currentIntensity = dataSet.getIntensity();
        string currentColor = dataSet.getColor();

        if (currentStatus != oldStatus ||
            ((currentIntensity != oldIntensity || currentColor != oldColor) && currentStatus == 1))
        {
            Boolean active = false;

            if (currentStatus == 1)
            {
                active = true;
            }
            else
            {
                active = false;
            }

            foreach (Transform item in lampen)
            {
                if (active)
                {
                    Light spot = (Light) item.FindChild(Config.STRING_LIGHT_SPOT).GetComponent(typeof(Light));
                    Light halo = (Light) item.FindChild(Config.STRING_LIGHT_HALO).GetComponent(typeof(Light));
                    spot.intensity = (float) (currentIntensity*4)/100;
                    halo.intensity = spot.intensity/2;
                    spot.color = hexToColor(currentColor);
                    halo.color = spot.color;
                }
                item.gameObject.SetActive(active);
            }
        }
        oldStatus = currentStatus;
        oldColor = currentColor;
        oldIntensity = currentIntensity;
    }

    /// <summary>
    /// Wandelt die Farbe von Hexadezimal in RGBA Farbwert um.
    /// </summary>
    /// <returns> Farbwert</returns>
    /// <param name="hex">Hexadezimalwert aus der Datenbank</param>
    public static Color hexToColor(string hex)
    {
        hex = hex.Replace("0x", ""); //in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", ""); //in case the string is formatted #FFFFFF
        byte a = 255; //assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }

    /// <summary>
    /// Bildet eine Liste aller Spots die zur entprechenden Lampe gehören
    /// </summary>
    /// <returns>Liste aller Spots</returns>
    private ArrayList getLights()
    {
        ArrayList temp = new ArrayList();
        Transform lampen = transform.FindChild(Config.STRING_LAMPEN);

        for (int i = 0; i < lampen.childCount; i++)
        {
            if (lampen.GetChild(i).name.StartsWith(Config.STRING_SPOT))
            {
                for (int j = 0; j < lampen.GetChild(i).childCount; j++)
                {
                    if (lampen.GetChild(i).GetChild(j).name.StartsWith(Config.STRING_SPOTLIGHT))
                    {
                        temp.Add(lampen.GetChild(i).GetChild(j));
                    }
                }
            }
        }
        return temp;
    }

    /// <summary>
    /// Coroutine zum Verändern der Umgebung(Licht ein/aus, Helligkeit und Farbe anpassen) 
    /// </summary>
    private IEnumerator changeScene()
    {
        while (true)
        {
            if (DataManager.insertReady && Mode.isPlayMode() && !GameobjectLoader.isLoading())
            {
                updateState();
            }
            yield return new WaitForSeconds(Config.FLOAT_REFRESH_INTERVAL);
        }
    }
}
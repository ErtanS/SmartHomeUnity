using System;
using System.Windows.Forms.VisualStyles;
using UnityEngine;

public class GetDataJob : ThreadedJob
{
    /// <summary>
    /// Startet neuen Thread zum Laden der Daten aus der Datenbank im Hintergrund.
    /// </summary>
    protected override void ThreadFunction()
    {
        //DateTime currentTime = DateTime.Now;
        while (true)
        {
            //DateTime time = DateTime.Now;
            //TimeSpan deltaTime = time.Subtract(currentTime);

            //if (deltaTime.TotalSeconds > 1)
            //{
                DataManager.loadData();
            //    currentTime = DateTime.Now;
            //}
            //Debug.Log("loop");
        }
    }
}
using UnityEngine;
using System.Collections;

public class PcManager : MonoBehaviour
{
    private PcDataSet dataSet;
    private Transform front;
    private string oldPicture = "-1";
    private int oldStatus = -1;
    private Texture2D texture;
    DownloadManager dm;

    // Use this for initialization
    void Start()
    {
        dm = new DownloadManager();
        front = transform.FindChild(Config.STRING_PREFAB_MAGICWALL).FindChild(Config.STRING_PREFAB_MAGICWALL_FRONT);
        texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        texture.SetPixel(1, 0, Color.black);
        texture.SetPixel(1, 1, Color.black);
        texture.SetPixel(0, 0, Color.black);
        texture.SetPixel(0, 1, Color.black);
        texture.Apply();
        StartCoroutine(changeScene());
    }

	/// <summary>
	/// Updates the state.
	/// </summary>
    private void updateState()
    {
        dataSet = (PcDataSet) DataManager.getDevice(name, dataSet);
        int status = dataSet.getState();
        string pictureid = dataSet.getPictureid();

        if ((!pictureid.Equals(oldPicture)|| status != oldStatus) && status == 1)
        {
            StartCoroutine(dm.LoadPicture(pictureid.ToString(), name));
        }
        else if (status == 0)
        {
            front.GetComponent<Renderer>().material.SetTexture(Config.MATERIAL_TEXTURE, texture);
        }
        oldStatus = status;
        oldPicture = pictureid;
    }

    /// <summary>
    /// Coroutine zum Verändern der Umgebung(Rolladen hoch/runter) 
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
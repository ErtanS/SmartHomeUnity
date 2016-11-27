using UnityEngine;
using System.Collections;

public class WallManager : MonoBehaviour
{
    private WallDataSet dataSet;
    private Transform front;
    private string oldPicture = "-1";
    private string oldColor;
    private Texture2D texture;
    DownloadManager dm;
    // Use this for initialization
    void Start()
    {
        dm = new DownloadManager();
        front = transform.FindChild(Config.STRING_PREFAB_MAGICWALL).FindChild(Config.STRING_PREFAB_MAGICWALL_FRONT);
        texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        StartCoroutine(changeScene());
    }

	/// <summary>
	/// Updates the state.
	/// </summary>
    private void updateState()
    {
        dataSet = (WallDataSet) DataManager.getDevice(name, dataSet);
        string pictureid = dataSet.getPictureid();
        string color = dataSet.getColor();

        if (pictureid.Equals("-1"))
        {
            Color currentColor = LightManager.hexToColor(color);
            texture.SetPixel(1, 0, currentColor);
            texture.SetPixel(1, 1, currentColor);
            texture.SetPixel(0, 0, currentColor);
            texture.SetPixel(0, 1, currentColor);
            texture.Apply();
            front.GetComponent<Renderer>().material.SetTexture(Config.MATERIAL_TEXTURE, texture);
        }
        if (pictureid != oldPicture && !pictureid.Equals("-1"))
        {
            StartCoroutine(dm.LoadPicture(pictureid.ToString(), name));
        }
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
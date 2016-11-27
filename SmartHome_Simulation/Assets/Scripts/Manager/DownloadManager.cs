using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

public class DownloadManager
{
	/// <summary>
	/// Loads the picture.
	/// </summary>
	/// <returns>The picture.</returns>
	/// <param name="pictureName">Picture name.</param>
	/// <param name="componentName">Component name.</param>
    public IEnumerator LoadPicture(string pictureName, string componentName)
    {
        GameObject wall = GameObject.Find(componentName);
        Transform picture =
            wall.transform.FindChild(Config.STRING_PREFAB_MAGICWALL).FindChild(Config.STRING_PREFAB_MAGICWALL_FRONT);
        Texture2D texture = new Texture2D(1, 1);
        if (pictureName.Equals("-1"))
        {
            Debug.Log("picturename = -1");
            texture = Resources.LoadAll(Config.STRING_PICTURE_FOLDER)[0] as Texture2D;
        }
        else
        {
            string url = "http://localhost/smart/picture/" + pictureName + ".png";
            UnityWebRequest www = UnityWebRequest.GetTexture(url);
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else {
                texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
            Debug.Log("picturename = " + pictureName);
            
            //WWW www = new WWW(url);
            //yield return www;
            //www.LoadImageIntoTexture(texture);
        }
        picture.GetComponent<Renderer>().material.SetTexture(Config.MATERIAL_TEXTURE, texture);
    }


}
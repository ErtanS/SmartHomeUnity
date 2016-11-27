using UnityEngine;
using System.Collections;

public class TvManager : MonoBehaviour
{
    private new Renderer renderer;
    private new AudioSource audio;
    private TvDataSet dataSet;
    private Transform front;
    private int  oldChannel = -1, oldVolume = -1, oldStatus = -1;
    private string oldPicture = "-1";
    private Texture2D texture;
    Object[] channelListe;
    MovieTexture movie;
    DownloadManager dm;
    GameObject roomTag;
    bool firstStart = false;
    string room;

    // Use this for initialization
    void Update()
    {
        if (!firstStart && DataManager.insertReady && Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            firstStart = true;
            dataSet = (TvDataSet) DataManager.getDevice(name, dataSet);
            dm = new DownloadManager();
            front = transform.FindChild(Config.STRING_PREFAB_MAGICWALL).FindChild(Config.STRING_PREFAB_MAGICWALL_FRONT);
            renderer = front.GetComponent<Renderer>();
            audio = front.GetComponent<AudioSource>();
            channelListe = Resources.LoadAll(Config.STRING_VIDEO_FOLDER, typeof(MovieTexture));
            roomTag = GameObject.FindGameObjectWithTag(Config.OBJ_CURRENT_ROOM);
            texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            texture.SetPixel(1, 0, Color.black);
            texture.SetPixel(1, 1, Color.black);
            texture.SetPixel(0, 0, Color.black);
            texture.SetPixel(0, 1, Color.black);
            texture.Apply();
            room = dataSet.getScenarioRoom();
            StartCoroutine(changeScene());
        }
        if (roomTag != null)
        {
            muteMusic();
        }
    }

	/// <summary>
	/// Updates the state.
	/// </summary>
    private void updateState()
    {
        dataSet = (TvDataSet) DataManager.getDevice(name, dataSet);
        int status = dataSet.getState();
        int channel = dataSet.getChannel();
        string pictureid = dataSet.getPictureid();
        int volume = dataSet.getVolume();

        if (( !pictureid.Equals(oldPicture) || status != oldStatus || channel != oldChannel) && channel == 0 && status == 1)
        {
            StartCoroutine(dm.LoadPicture(pictureid.ToString(), name));
        }
        if (status == 0 || channel == 0)
        {
            if (movie != null)
            {
                movie.Stop();
                audio.Stop();
            }
            if (status == 0)
            {
                renderer.material.SetTexture(Config.MATERIAL_TEXTURE, texture);
            }
        }
        if (status == 1 && channel != 0 && (channel != oldChannel || oldVolume != volume || oldStatus != status))
        {
            if (channel != oldChannel || status != oldStatus)
            {
                movie = channelListe[channel - 1] as MovieTexture;

                renderer.material.mainTexture = movie;
                audio.clip = movie.audioClip;
                movie.Play();
                audio.Play();
                movie.loop = true;
                audio.loop = true;
            }
            audio.volume = (float) volume/100;
        }
        oldStatus = status;
        oldPicture = pictureid;
        oldVolume = volume;
        oldChannel = channel;
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

    /// <summary>
    /// Schaltet den Lautsprecher des Raumes in dem man sich befindet an und stellt alle anderen Lautsprecher auf stumm.
    /// </summary>
    private void muteMusic()
    {
        if (roomTag.name.Equals(room) && Mode.isPlayMode())
        {
            audio.mute = false;
        }
        else
        {
            audio.mute = true;
        }
    }
}
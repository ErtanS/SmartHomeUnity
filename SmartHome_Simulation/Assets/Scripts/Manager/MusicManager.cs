using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    private int oldSong = -1;
    private Object[] musikListe;
    private int oldStatus = -1;
    private int oldVolume = -1;
    private AudioSource myAudioSource;
    private GameObject roomTag;
    private string room;
    private bool firstStart = false;
    private SpeakerDataSet dataSet;
    private int playstate = 0;
    private int id;

	/// <summary>
	/// Update this instance.
	/// </summary>
    public void Update()
    {
        if (!firstStart && DataManager.insertReady && Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            myAudioSource = getMusik();
            roomTag = GameObject.FindGameObjectWithTag(Config.OBJ_CURRENT_ROOM);
            musikListe = Resources.LoadAll(Config.STRING_MUSIC_FOLDER, typeof(AudioClip));
            dataSet = (SpeakerDataSet) DataManager.getDevice(name, dataSet);
            id = dataSet.getId();
            room = dataSet.getScenarioRoom();
            int currentSong = dataSet.getSongid();
            myAudioSource.clip = musikListe[currentSong] as AudioClip;
            firstStart = true;
            StartCoroutine(changeScene());
        }
        if (roomTag != null)
        {
            muteMusic();
        }
    }

    /// <summary>
    /// Schaltet den Lautsprecher des Raumes in dem man sich befindet an und stellt alle anderen Lautsprecher auf stumm.
    /// </summary>
    private void muteMusic()
    {
        if (roomTag.name.Equals(room) && Mode.isPlayMode())
        {
            myAudioSource.mute = false;
        }
        else
        {
            myAudioSource.mute = true;
        }
    }

    /// <summary>
    /// Aktualisiert die Eigenschaften des Lautsprechers
    /// Spielt die Musik ab(status = 1) bzw. stoppt diese(status = 0)
    /// Wechselt den Song
    /// Regelt die Lautstärke der Musikquelle.
    /// </summary>
    private void updateState()
    {
        dataSet = (SpeakerDataSet) DataManager.getDevice(name, dataSet);

        int currentStatus = dataSet.getState();
        int currentVolume = dataSet.getVolume();
        int currentSong = dataSet.getSongid();
        int stop = dataSet.getStop();
        
        if (stop == 1)
        {
            myAudioSource.Stop();
            RequestHandler rh = new RequestHandler();
            StartCoroutine(rh.makeRequest(rh.updateDatabase(Config.STRING_TYPE_EN_SPEAKER, Config.TAG_STOP, Config.STRING_STATUS_AUS, id)));
            oldStatus = -1;
            playstate = 0;
        }

        if (oldSong != currentSong && oldSong != -1)
        {
            myAudioSource.clip = musikListe[currentSong] as AudioClip;
            myAudioSource.Play();
            playstate = 0;
        }

        if (currentStatus != oldStatus)
        {
            if (currentStatus == 0)
            {
                myAudioSource.Pause();
            }
            if (currentStatus == 1)
            {
                if (playstate == 0)
                {
                    myAudioSource.Play();
                    playstate = 1;
                }
                else
                {
                    myAudioSource.UnPause();
                }
            }
        }

        if (currentVolume != oldVolume)
        {
            myAudioSource.volume = (float) currentVolume/100;
        }

        oldStatus = currentStatus;
        oldVolume = currentVolume;
        oldSong = currentSong;
    }

    /// <summary>
    /// Verwaltet die Audioquelle des Lautsprechers
    /// </summary>
    /// <returns>Audioquelle</returns>
    public AudioSource getMusik()
    {
        GameObject tempAudioSource = transform.FindChild(Config.STRING_AUDIO_SOURCE).gameObject;
        AudioSource mySource = (AudioSource) tempAudioSource.GetComponent(typeof(AudioSource));
        return mySource;
    }


    /// <summary>
    /// Coroutine zum Verändern der Umgebung(Musik ein/aus, Lautstärke und Songauswahl anpassen) 
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
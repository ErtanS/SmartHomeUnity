using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class CameraManager : MonoBehaviour
{
    private Camera cam;
    private int resWidth = 640;
    private int resHeight = 360;
    private bool firstStart = false;
    private GameObject thief;
    private int oldStatus = -1;
    private int oldFrequency = -1;
    private CameraDataSet dataSet;
    private IEnumerator coroutine;
    private GameObject thiefTag;
    private string room;
    private int camInterval = 1;
    private RequestHandler rh = new RequestHandler();
    private int autoEmergency = 0;

	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
        cam = transform.FindChild(Config.STRING_GAMEOBJECT_VIEW).GetComponent<Camera>();
        cam.enabled = false;
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
    {
        if (!firstStart && DataManager.insertReady && Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            thief =
                GameObject.Find(Config.STRING_GAMEOBJECT_MODES)
                    .transform.FindChild(Config.STRING_THIEF_BEHAVIOUR)
                    .FindChild(Config.STRING_THIEF)
                    .gameObject;
            firstStart = true;
            dataSet = (CameraDataSet) DataManager.getDevice(name, dataSet);
            room = dataSet.getScenarioRoom();
            coroutine = detectPlayer();
            StartCoroutine(changeScene());
        }
        if (thiefTag == null && GameObject.FindGameObjectWithTag(Config.STRING_THIEF_ROOM) != null)
        {
            thiefTag = GameObject.FindGameObjectWithTag(Config.STRING_THIEF_ROOM);
        }
    }

	/// <summary>
	/// Updates the state.
	/// </summary>
    private void updateState()
    {
        dataSet = (CameraDataSet) DataManager.getDevice(name, dataSet);
        int status = dataSet.getState();
        int frequency = dataSet.getFrequency();
        autoEmergency = dataSet.getAutoEmergency();

        if (frequency != oldFrequency)
        {
            camInterval = 5 + frequency*5;
            oldFrequency = frequency;
        }
        if (status != oldStatus)
        {
            if (status == Config.INT_STATUS_EIN)
            {
                transform.FindChild(Config.STRING_LED).gameObject.SetActive(true);
                if (coroutine != null)
                {
                    StartCoroutine(coroutine);
                }
            }
            else
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }
                transform.FindChild(Config.STRING_LED).gameObject.SetActive(false);
            }
            oldStatus = status;
        }
    }

	/// <summary>
	/// Detects the player.
	/// </summary>
	/// <returns>The player.</returns>
	/// <param name="autoEmergency">Auto emergency.</param>
    public IEnumerator detectPlayer()
    {
        int seconds = 0;
        while (true)
        {
            //Checks to see if player is in the view area of the camera
            if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(cam),
                thief.GetComponent<Collider>().bounds))
            {
                //Additional check to see if the player is obscured by a wall or other object
                if (thiefTag != null && thiefTag.name.Equals(room))
                {
                    if (autoEmergency == 1)
                    {
                        StartCoroutine(rh.makeRequest(rh.startEmergencyScenario(name)));
                    }
                    StartCoroutine(takeScreenshot());
                    cam.enabled = false;
                    seconds = camInterval;
                }
                else
                {
                    seconds = 0;
                }
            }
            else
            {
                seconds = 0;
            }
            yield return new WaitForSeconds(seconds);
        }
    }

	/// <summary>
	/// Takes the screenshot.
	/// </summary>
	/// <returns>The screenshot.</returns>
    public IEnumerator takeScreenshot()
    {
        yield return new WaitForEndOfFrame();
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        cam.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = ScreenShotName(resWidth, resHeight);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", bytes, filename, "image/png");
        StartCoroutine(rh.uploadRequest(new RequestSet(Config.URL_UPLOAD, form)));
        //UnityWebRequest www = UnityWebRequest.Post("http://localhost/smart/upload.php", form);
        //www.Send();

        //if (www.isError)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        string[] nameListe = filename.Split('/');
        StartCoroutine(rh.makeRequest(rh.pushFirebase(Config.STRING_MESSAGE_CAMERA + name + "~" + nameListe[nameListe.Length - 1])));
        yield return null;
        //}
        cam.enabled = false;
    }

	/// <summary>
	/// Generates the name for the screenshot
	/// </summary>
	/// <returns>The shot name.</returns>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png",
            Application.dataPath,
            width, height,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    /// <summary>
    /// Coroutine zum Veraedern der Umgebung() 
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
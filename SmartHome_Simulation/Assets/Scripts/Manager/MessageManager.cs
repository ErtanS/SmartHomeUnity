using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public GUIStyle style;
    private ArrayList messageList = new ArrayList();
    private float deltaTime = 0;
    private string displayText = "";
    private Vector2 scrollPosition;
    private string importantMsg = "";

    public const string MSG_DEFAULT = "Objekt kann hier nicht platziert werden.";
    public const string MSG_DEFAULT_USED = "An dieser Stelle befindet sich bereits ein Objekt.";

	/// <summary>
	/// Awake this instance.
	/// </summary>
    void Awake()
    {
        importantMsg = "";
        messageList = new ArrayList();
        StartCoroutine(messageHandler());
    }

	/// <summary>
	/// Adds the message to queue.
	/// </summary>
	/// <param name="msg">Message.</param>
    public void addMessageToQueue(string msg)
    {
        messageList.Add(msg);
    }

	/// <summary>
	/// Sets the important message.
	/// </summary>
	/// <param name="msg">Message.</param>
    public void setImportantMessage(string msg)
    {
        importantMsg = msg + "\n\n";
    }

	/// <summary>
	/// Clears the messages.
	/// </summary>
    public void clearMessages()
    {
        importantMsg = "";
        messageList.Clear();
    }

	/// <summary>
	/// Handles the messages.
	/// </summary>
	/// <returns>The handler.</returns>
    IEnumerator messageHandler()
    {
        while (true)
        {
            if (messageList.Count > 0 || !importantMsg.Equals(Config.STRING_EMPTY))
            {
                string longString = "";
                if (messageList.Count > 0)
                {
                    deltaTime += Time.deltaTime;
                    if (deltaTime > 2)
                    {
                        deltaTime = 0;
                        messageList.RemoveAt(0);
                    }
                    foreach (string msg in messageList)
                    {
                        longString = longString + msg + "\n\n";
                    }
                }
                displayText = importantMsg + longString;
            }
            yield return null;
        }
    }

	/// <summary>
	/// Raises the GU event.
	/// </summary>
    void OnGUI()
    {
        if (!GameobjectLoader.isLoading() && !Mode.isPauseMode() && !Mode.isMenuMode() && (!Mode.isPlayMode() || !importantMsg.Equals(Config.STRING_EMPTY) || (messageList != null && messageList.Count > 0)))
        {
            
            GUI.BeginGroup(
                new Rect(Screen.width - (Screen.width/7), Screen.height/150, Screen.width/7,
                    Screen.height - Screen.height/75), style);
            GUIStyle text = new GUIStyle();
            text.fontSize = 14;
            text.normal.textColor = Color.white;
            GUILayout.Label("INFO", text);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width/7),
                GUILayout.Height(Screen.height - Screen.height/75));
            if (!Mode.isPlayMode())
            {
                GUILayout.Label(Config.MSG_MENU + "\n\n" +displayText);
            }
            else
            {
                GUILayout.Label(displayText);
            }
            
            GUILayout.EndScrollView();
            GUI.EndGroup();
        }

        if (!GameobjectLoader.isLoading() && Mode.isPlayMode())
        {
            int w = Screen.width, h = Screen.height;
            GUIStyle style = new GUIStyle();
            Rect rect = new Rect(5, h - h * 2 / 85, w, h * 2 / 100);
            style.alignment = TextAnchor.LowerLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);

            string text = Config.MSG_MENU;
            GUI.Label(rect, text, style);
        }

    }
}
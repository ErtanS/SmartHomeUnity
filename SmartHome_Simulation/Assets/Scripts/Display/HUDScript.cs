using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour
{
    float deltaTime = 0.0f;
    private double temperature;
    private GameObject roomTag;

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime)*0.1f;
    }

    /// <summary>
    /// Stellt wichtige Informationen auf der Oberfläche dar:
    /// Temperatur, Uhrzeit, Raum
    /// </summary>
    void OnGUI()
    {
        if (Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            if (roomTag == null)
            {
                foreach (Transform trans in GameObject.Find(Config.STRING_GAMEOBJECT_MODES).GetComponentsInChildren<Transform>(true))
                {
                    if (trans.tag.Equals(Config.OBJ_CURRENT_ROOM)) {
                        roomTag = trans.gameObject;
                    }
                }
            }
            int w = Screen.width, h = Screen.height;
            GUIStyle style = new GUIStyle();
            Rect rect = new Rect(10, 10, w, h*2/100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h*2/50;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            style.font = (Font) Resources.Load(Config.SET_DIGITAL_FONT, typeof(Font));

            string room = roomTag.name;

            if (room.Equals(Config.STRING_NOT_IN_ROOM))
            {
                room = Config.STRING_OUTSIDE;
                HeaterManager.realTemperature = Config.DOUBLE_STANDARD_TEMPERATURE;
            }
            string text = string.Format("Raum: {0:0}\n\nTemperatur: {1:0.0} °C \n\nUhrzeit:  {2:00}:{3:00}  ", room,
                HeaterManager.realTemperature, Clock.hour, Clock.minute);
            GUI.Label(rect, text, style);
        }
    }
}
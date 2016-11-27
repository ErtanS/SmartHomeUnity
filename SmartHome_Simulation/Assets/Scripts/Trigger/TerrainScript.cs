using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour
{
    private GameObject roomTag;
    private GameObject thiefTag;

    // Use this for initialization
    void Update()
    {
        if (roomTag == null && GameObject.FindGameObjectWithTag(Config.OBJ_CURRENT_ROOM) != null)
        {
            roomTag = GameObject.FindGameObjectWithTag(Config.OBJ_CURRENT_ROOM);
        }
        if (thiefTag == null && GameObject.FindGameObjectWithTag(Config.STRING_THIEF_ROOM) != null)
        {
            thiefTag = GameObject.FindGameObjectWithTag(Config.STRING_THIEF_ROOM);
        }
    }

    /// <summary>
    /// Trigger zur Erkennung in welchem Raum, man sich aufhält
    /// </summary>
    /// <param name="col">Col.</param>
    void OnCollisionStay(Collision collision)
    {
        if (roomTag != null && collision != null && collision.collider.tag.Equals(Config.STRING_PLAYER))
        {
            roomTag.name = Config.STRING_NOT_IN_ROOM;
        }
        if (thiefTag != null && collision != null && collision.collider.tag.Equals(Config.STRING_THIEF))
        {
            thiefTag.name = Config.STRING_NOT_IN_ROOM;
        }
    }
}
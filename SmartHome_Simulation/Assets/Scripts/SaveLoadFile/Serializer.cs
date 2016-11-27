using UnityEngine;
using System.Collections;
using System;

public class Serializer : MonoBehaviour
{
    private string json;
    private Quaternion oldRotation;
    private Vector3 oldScale;
    private SerializeData data;
    private string oldParent;
    private string oldName;
   
    // Use this for initialization
    void Start()
    {
        serialize(false);
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
    void Update()
    {
        Vector3 scale = transform.localScale;
        Quaternion rot = transform.rotation;
        string parent = null;
        if (transform.parent != null)
        {
            parent = transform.parent.name;
        }
        if (data != null && (rot != oldRotation || scale != oldScale || name != oldName || oldParent != parent))
        {
            serialize(data.used);
        }
    }

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
    void OnDestroy()
    {
        GameobjectLoader.deleteJsonFromList(json);
    }

	/// <summary>
	/// Serialize the objects.
	/// </summary>
	/// <param name="used">If set to <c>true</c> used.</param>
    public void serialize(bool used)
    {
        if (data != null)
        {
            GameobjectLoader.deleteJsonFromList(json);
        }
        string prefab = transform.tag;

        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        oldRotation = rot;

        Vector3 scale = transform.localScale;
        oldScale = scale;
        oldName = name;

        string parent = null;
        if (transform.parent != null)
        {
            parent = transform.parent.name;
        }
        oldParent = parent;
        Vector2 size = GameobjectLoader.getSize(prefab);
        Vector2 gridPos = calcGridPos();
        int rotationAngle = (int) (Math.Round(rot.eulerAngles.y)%360);

        if (rotationAngle == 90)
        {
            float tempSize = size.x;
            size.x = size.y;
            size.y = -tempSize;
        }
        if (rotationAngle == 180)
        {
            size.x = -size.x;
            size.y = -size.y;
        }
        if (rotationAngle == 270)
        {
            float tempSize = size.x;
            size.x = -size.y;
            size.y = tempSize;
        }
        data = new SerializeData(name, prefab, pos, rot, scale, parent, size, used, gridPos);
        json = JsonUtility.ToJson(data);
        GameobjectLoader.setJsonString(json);
    }

	/// <summary>
	/// Ises the used.
	/// </summary>
	/// <returns><c>true</c>, if used was ised, <c>false</c> otherwise.</returns>
    public bool isUsed()
    {
        return data.used;
    }

	/// <summary>
	/// Gets the size.
	/// </summary>
	/// <returns>The size.</returns>
    public Vector2 getSize()
    {
        return data.size;
    }

	/// <summary>
	/// Gets the grid position.
	/// </summary>
	/// <returns>The grid position.</returns>
    public Vector2 getGridPos()
    {
        return data.gridPos;
    }

	/// <summary>
	/// Serializes the data from file.
	/// </summary>
	/// <param name="data">Data.</param>
    public void serializeDataFromFile(SerializeData data)
    {
        this.data = data;
        GameobjectLoader.deleteJsonFromList(json);
        json = JsonUtility.ToJson(data);
        GameobjectLoader.setJsonString(json);
    }

	/// <summary>
	/// Hases the objects.
	/// </summary>
	/// <returns><c>true</c>, if objects was hased, <c>false</c> otherwise.</returns>
	/// <param name="list">List.</param>
    private bool hasObjects(ArrayList list)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (list.Contains(transform.GetChild(i).tag))
            {
                return true;
            }
            if (transform.GetChild(i).name.StartsWith(Config.STRING_BELOW) ||
                transform.GetChild(i).name.StartsWith(Config.STRING_ABOVE)
                || transform.GetChild(i).name.StartsWith(Config.STRING_LEFT) ||
                transform.GetChild(i).name.StartsWith(Config.STRING_RIGHT))
            {
                for (int j = 0; j < transform.GetChild(i).childCount; j++)
                {
                    if (transform.GetChild(i).GetChild(j).tag.Equals(Config.STRING_PREFAB_SINK))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

	/// <summary>
	/// Calculates the grid position.
	/// </summary>
	/// <returns>The grid position.</returns>
    private Vector2 calcGridPos()
    {
        if (transform.parent != null && transform.parent.name.StartsWith(Config.STRING_PREFIX_POS_TRANSFORM))
        {
            int x = Convert.ToInt32(transform.parent.name.Split(':')[1]);
            int y = Convert.ToInt32(transform.parent.name.Split(':')[2]);
            return new Vector2(x, y);
        }
        else if (transform.parent != null && transform.parent.parent != null &&
                 transform.parent.parent.name.StartsWith(Config.STRING_PREFIX_POS_TRANSFORM))
        {
            int x = Convert.ToInt32(transform.parent.parent.name.Split(':')[1]);
            int y = Convert.ToInt32(transform.parent.parent.name.Split(':')[2]);
            return new Vector2(x, y);
        }
        return new Vector2(0, 0);
    }
}
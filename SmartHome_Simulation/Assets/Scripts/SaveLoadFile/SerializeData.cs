using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class SerializeData
{
    public String name;
    public String prefab;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public String parent;
    public Vector2 size;
    public bool used;
    public Vector2 gridPos;

	/// <summary>
	/// Initializes a new instance of the <see cref="SerializeData"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="prefab">Prefab.</param>
	/// <param name="position">Position.</param>
	/// <param name="rotation">Rotation.</param>
	/// <param name="scale">Scale.</param>
	/// <param name="parent">Parent.</param>
	/// <param name="size">Size.</param>
	/// <param name="used">If set to <c>true</c> used.</param>
	/// <param name="gridPos">Grid position.</param>
    public SerializeData(String name, String prefab, Vector3 position, Quaternion rotation, Vector3 scale, String parent,
        Vector2 size, bool used, Vector2 gridPos)
    {
        this.name = name;
        this.prefab = prefab;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.parent = parent;
        this.size = size;
        this.used = used;
        this.gridPos = gridPos;
    }
}
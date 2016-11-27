using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class wandGenerator
{
	/// <summary>
	/// Creates the wall.
	/// </summary>
	/// <returns>The wall.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="lengthX">Length x.</param>
	/// <param name="cube">Cube.</param>
	/// <param name="roomName">Room name.</param>
	/// <param name="rotation">Rotation.</param>
    public GameObject createWall(float x, float z, float lengthX, GameObject cube, string roomName, float rotation)
    {
        cube.transform.localScale = new Vector3(lengthX, 0.1f, 0.2f);
        float posX = x + (lengthX/2.0f);
        float posZ = z + 0.1f;
        cube.transform.position = new Vector3(posX, 0.05f, posZ);
        cube.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
        cube.transform.name = Config.STRING_WALL_X + Math.Round(x, 1) + "," + Math.Round(z, 1) + "_" + roomName;
        return cube;
    }

	/// <summary>
	/// Creates the floor.
	/// </summary>
	/// <returns>The floor.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="lengthX">Length x.</param>
	/// <param name="lengthZ">Length z.</param>
	/// <param name="cube">Cube.</param>
    public GameObject createFloor(float x, float z, float lengthX, float lengthZ, GameObject cube)
    {
        cube.transform.localScale = new Vector3(lengthX, 0.01f, lengthZ);
        float posX = x + 0.6f;
        float posZ = z + 0.6f;
        cube.transform.position = new Vector3(posX, -0.0045f, posZ);
        cube.transform.name = Config.STRING_FLOOR + Math.Round(x, 1) + "," + Math.Round(z, 1);
        cube.GetComponent<Renderer>().material.color = Color.gray;
        return cube;
    }

	/// <summary>
	/// Creates the pole.
	/// </summary>
	/// <returns>The pole.</returns>
	/// <param name="pole">Pole.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="roomName">Room name.</param>
    public GameObject createPole(GameObject pole, float x, float z, string roomName)
    {
        pole.transform.position = new Vector3(x, 0.05f, z);
        pole.transform.name = Config.STRING_POLE + Math.Round(x, 1) + "," + Math.Round(z, 1) + "_" + roomName;
        return pole;
    }
}
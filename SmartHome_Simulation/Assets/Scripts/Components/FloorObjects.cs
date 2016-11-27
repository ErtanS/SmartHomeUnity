using UnityEngine;
using System.Collections;

public class FloorObjects : Components
{
    private ArrayList floorObjects;
    private static bool start;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(manageFloorObjectsIE());
        }
    }

	/// <summary>
	/// Manages the floor objects.
	/// </summary>
    public static void manageFloorObjects()
    {
        start = true;
    }

	/// <summary>
	/// Refreshs the floor object list.
	/// </summary>
    private void refreshFloorObjectList()
    {
        floorObjects = new ArrayList();
        foreach (string tag in GameobjectLoader.floorObjects)
        {
            floorObjects.AddRange(findGameObjectsWithTag(tag));
        }
    }

	/// <summary>
	/// Manages the floor objects I.
	/// </summary>
	/// <returns>The floor objects I.</returns>
    private IEnumerator manageFloorObjectsIE()
    {
        refreshFloorObjectList();
        enableFloorObjects();
        yield return null;
    }

	/// <summary>
	/// Enables the floor objects.
	/// </summary>
    private void enableFloorObjects()
    {
        activateFloorObjects(!Mode.isBuildMode());
    }

	/// <summary>
	/// Activates the floor objects.
	/// </summary>
	/// <param name="active">If set to <c>true</c> active.</param>
    private void activateFloorObjects(bool active)
    {
        foreach (Transform floorObject in floorObjects)
        {
            floorObject.gameObject.SetActive(active);
        }
    }
}
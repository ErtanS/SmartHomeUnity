using UnityEngine;
using System.Collections;

public class WallObjects : Components
{
    private ArrayList wallObjects;
    private static bool start;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(manageWallObjectsIE());
        }
    }

	/// <summary>
	/// Manages the wall objects.
	/// </summary>
    public static void manageWallObjects()
    {
        start = true;
    }

	/// <summary>
	/// Refreshs the wall object list.
	/// </summary>
    private void refreshWallObjectList()
    {
        wallObjects = new ArrayList();
        wallObjects.AddRange(findGameObjectsWithTag(Config.STRING_PREFAB_SINK));
        wallObjects.AddRange(findGameObjectsWithTag(Config.STRING_TYPE_EN_SHUTTERS));
        wallObjects.AddRange(findGameObjectsWithTag(Config.STRING_TYPE_EN_WALL));
    }

	/// <summary>
	/// Manages the wall objects .
	/// </summary>
	/// <returns>The wall objects I.</returns>
    private IEnumerator manageWallObjectsIE()
    {
        refreshWallObjectList();
        enableWallObjects();
        yield return null;
    }

	/// <summary>
	/// Enables the wall objects.
	/// </summary>
    private void enableWallObjects()
    {
        activateWallObjects(!Mode.isBuildMode());
    }

	/// <summary>
	/// Activates the wall objects.
	/// </summary>
	/// <param name="active">If set to <c>true</c> active.</param>
    private void activateWallObjects(bool active)
    {
        foreach (Transform wallObject in wallObjects)
        {
            wallObject.gameObject.SetActive(active);
        }
    }
}
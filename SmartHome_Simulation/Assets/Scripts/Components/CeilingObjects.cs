using UnityEngine;
using System.Collections;

public class CeilingObjects : Components
{
    private ArrayList ceilingObjects;
    private static bool start;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(manageCeilingObjectsIE());
        }
    }

	/// <summary>
	/// Manages the ceiling objects.
	/// </summary>
    public static void manageCeilingObjects()
    {
        start = true;
    }

	/// <summary>
	/// Refreshs the ceiling object list.
	/// </summary>
    private void refreshCeilingObjectList()
    {
        ceilingObjects = new ArrayList();
        foreach (string tag in GameobjectLoader.ceilingObjects)
        {
            ceilingObjects.AddRange(findGameObjectsWithTag(tag));
        }
    }

	/// <summary>
	/// Manages the ceiling objects I.
	/// </summary>
	/// <returns>The ceiling objects I.</returns>
    private IEnumerator manageCeilingObjectsIE()
    {
        refreshCeilingObjectList();
        enableCeilingObjects();
        yield return null;
    }

	/// <summary>
	/// Enables the ceiling objects.
	/// </summary>
    private void enableCeilingObjects()
    {
        if (Mode.isBuildMode())
        {
            activateCeilingObjects(false);
        }
        else
        {
            activateCeilingObjects(true);
        }
    }

	/// <summary>
	/// Activates the ceiling objects.
	/// </summary>
	/// <param name="active">If set to <c>true</c> active.</param>
    private void activateCeilingObjects(bool active)
    {
        foreach (Transform ceilingObject in ceilingObjects)
        {
            ceilingObject.gameObject.SetActive(active);
        }
    }
}
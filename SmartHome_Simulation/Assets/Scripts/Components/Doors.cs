using UnityEngine;
using System.Collections;

public class Doors : Components
{
    private ArrayList doors;
    private static bool start;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(manageDoorsIE());
        }
    }

	/// <summary>
	/// Manages the doors.
	/// </summary>
    public static void manageDoors()
    {
        start = true;
    }

	/// <summary>
	/// Refreshs the door list.
	/// </summary>
    private void refreshDoorList()
    {
        doors = findGameObjectsWithTag(Config.STRING_TYPE_EN_DOOR);
    }

	/// <summary>
	/// Manages the doors I.
	/// </summary>
	/// <returns>The doors I.</returns>
    private IEnumerator manageDoorsIE()
    {
        refreshDoorList();
        enableDoors();
        yield return null;
    }

	/// <summary>
	/// Enables the doors.
	/// </summary>
    private void enableDoors()
    {
        if (Mode.isBuildMode())
        {
            print("türen deaktivieren");
            activateDoors(false);
        }
        else
        {
            activateDoors(true);
            activateCollider(Mode.isPlayMode());
        }
    }

	/// <summary>
	/// Activates the collider.
	/// </summary>
	/// <param name="active">If set to <c>true</c> active.</param>
    private void activateCollider(bool active)
    {
        foreach (Transform door in doors)
        {
            door.GetComponent<BoxCollider>().enabled = active;
            door.FindChild(Config.STRING_COLLIDER).gameObject.SetActive(!active);
        }
    }

	/// <summary>
	/// Activates the doors.
	/// </summary>
	/// <param name="active">If set to <c>true</c> active.</param>
    private void activateDoors(bool active)
    {
        foreach (Transform door in doors)
        {
            door.gameObject.SetActive(active);
        }
    }
}
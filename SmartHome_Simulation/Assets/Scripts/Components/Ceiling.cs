using UnityEngine;
using System.Collections;

public class Ceiling : Components
{
    private ArrayList ceiling;
    private static bool start;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(manageCeilingIE());
        }
    }

	/// <summary>
	/// Manages the ceiling.
	/// </summary>
    public static void manageCeiling()
    {
        start = true;
    }

	/// <summary>
	/// Refreshs the ceiling list.
	/// </summary>
    private void refreshCeilingList()
    {
        ceiling = findGameObjectsWithTag(Config.STRING_PREFAB_CEILING);
    }


	/// <summary>
	/// Manages the ceiling I.
	/// </summary>
	/// <returns>The ceiling I.</returns>
    private IEnumerator manageCeilingIE()
    {
        refreshCeilingList();
        enableCeiling();
        yield return null;
    }

	/// <summary>
	/// Enables the ceiling.
	/// </summary>
    private void enableCeiling()
    {
        if (Mode.isPlayMode())
        {
            activateCeiling(true);
        }
        else
        {
            activateCeiling(false);
        }
    }
		
	/// <summary>
	/// Activates the ceiling.
	/// </summary>
	/// <param name="active">If set to <c>true</c> active.</param>
    private void activateCeiling(bool active)
    {
        foreach (Transform ceil in ceiling)
        {
            ceil.FindChild(Config.STRING_CUBE).gameObject.SetActive(active);
        }
    }
}
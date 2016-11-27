using UnityEngine;
using System.Collections;

public class Poles : Components
{
    private ArrayList poles;
    private static bool start;

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(managePolesIE());
        }
    }

	/// <summary>
	/// Manages the poles.
	/// </summary>
    public static void managePoles()
    {
        start = true;
    }

	/// <summary>
	/// Refreshs the pole list.
	/// </summary>
    private void refreshPoleList()
    {
        poles = findGameObjectsWithTag(Config.STRING_PREFAB_POLE);
    }

	/// <summary>
	/// Manages the poles I.
	/// </summary>
	/// <returns>The poles I.</returns>
    private IEnumerator managePolesIE()
    {
        refreshPoleList();
        clearDuplicatePoles();
        buildHighOrSmallPoles();
        enablePoles();
        yield return null;
    }

	/// <summary>
	/// Scales the pole on Y axis.
	/// </summary>
	/// <param name="scale">Scale.</param>
	/// <param name="pos">Position.</param>
    private void scalePoleOnYAxis(float scale, float pos)
    {
        foreach (Transform pole in poles)
        {
            pole.localScale = new Vector3(pole.localScale.x, scale, pole.localScale.z);
            pole.position = new Vector3(pole.position.x, pos, pole.position.z);
        }
    }

	/// <summary>
	/// Builds the high or small poles.
	/// </summary>
    private void buildHighOrSmallPoles()
    {
        if (Mode.isBuildMode())
        {
            scalePoleOnYAxis(0.1f, 0.05f);
        }
        else
        {
            scalePoleOnYAxis(2.5f, 1.25f);
        }
    }

	/// <summary>
	/// Clears the duplicate poles.
	/// </summary>
    public void clearDuplicatePoles()
    {
        if (!Mode.isBuildMode())
        {
            int counter = 1;
            foreach (Transform pole in poles)
            {
                for (int i = counter; i < poles.Count; i++)
                {
                    Transform secondPole = poles[i] as Transform;
                    if (getName(pole).Equals(getName(secondPole)))
                    {
                        secondPole.gameObject.SetActive(false);
                        break;
                    }
                }
                counter++;
            }
        }
    }

	/// <summary>
	/// Gets the name.
	/// </summary>
	/// <returns>The name.</returns>
	/// <param name="pole">Pole.</param>
    private string getName(Transform pole)
    {
        return pole.name.Split('_')[0];
    }

	/// <summary>
	/// Enables the poles.
	/// </summary>
    private void enablePoles()
    {
        if (Mode.isBuildMode())
        {
            foreach (Transform pole in poles)
            {
                pole.gameObject.SetActive(true);
            }
        }
    }
}
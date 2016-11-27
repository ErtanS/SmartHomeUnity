using UnityEngine;
using System.Collections;

public class Walls : Components
{
    private ArrayList walls;
    private static bool start;

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(manageWallsIE());
        }
    }

	/// <summary>
	/// Manages the walls.
	/// </summary>
    public static void manageWalls()
    {
        start = true;
    }

	/// <summary>
	/// Refreshs the wall list.
	/// </summary>
    private void refreshWallList()
    {
        walls = findGameObjectsWithTag(Config.STRING_PREFAB_WALL);
    }

	/// <summary>
	/// Manages the walls I.
	/// </summary>
	/// <returns>The walls I.</returns>
    private IEnumerator manageWallsIE()
    {
        refreshWallList();
        buildHighOrSmallWalls();
        scaleDuplicateWalls();
        disableWallsWithDoorOrWindow();
        yield return null;
    }

	/// <summary>
	/// Scales the wall on Y axis.
	/// </summary>
	/// <param name="scale">Scale.</param>
	/// <param name="pos">Position.</param>
    private void scaleWallOnYAxis(float scale, float pos)
    {
        foreach (Transform wall in walls)
        {
            wall.localScale = new Vector3(wall.localScale.x, scale, wall.localScale.z);
            wall.position = new Vector3(wall.position.x, pos, wall.position.z);
        }
    }

	/// <summary>
	/// Builds the high or small walls.
	/// </summary>
    private void buildHighOrSmallWalls()
    {
        if (Mode.isBuildMode())
        {
            scaleWallOnYAxis(0.1f, 0.05f);
        }
        else
        {
            scaleWallOnYAxis(2.5f, 1.25f);
        }
    }

	/// <summary>
	/// Finds the duplicate wall.
	/// </summary>
	/// <returns>The duplicate wall.</returns>
	/// <param name="wall">Wall.</param>
    public Transform findDuplicateWall(Transform wall)
    {
        foreach (Transform dupWall in walls)
        {
            if (!dupWall.name.Equals(wall.name) && getName(wall).Equals(getName(dupWall)))
            {
                return dupWall;
            }
        }
        return null;
    }

	/// <summary>
	/// Scales the duplicate walls.
	/// </summary>
    private void scaleDuplicateWalls()
    {
        if (Mode.isBuildMode())
        {
            scaleDuplicateWalls(0.15f, 0, -0.1f, -0.05f);
        }
        else
        {
            scaleDuplicateWalls(1, 0.15f, 0.1f, 0.05f);
        }
    }

	/// <summary>
	/// Scales the duplicate walls.
	/// </summary>
	/// <param name="smaller">Smaller.</param>
	/// <param name="greater">Greater.</param>
	/// <param name="deltaScale">Delta scale.</param>
	/// <param name="deltaPosition">Delta position.</param>
    private void scaleDuplicateWalls(float smaller, float greater, float deltaScale, float deltaPosition)
    {
        foreach (Transform wall in walls)
        {
            Transform dupWall = findDuplicateWall(wall);
            if (dupWall != null)
            {
                if (wall.localScale.z > greater && wall.localScale.z < smaller)
                {
                    moveObjectOnRightPosition(wall, getSite(wall), deltaPosition);
                    wall.localScale -= new Vector3(0, 0, deltaScale);
                    moveObjectOnRightPosition(dupWall, getSite(dupWall), deltaPosition);
                    dupWall.localScale -= new Vector3(0, 0, deltaScale);
                }
            }
        }
    }

	/// <summary>
	/// Gets the name.
	/// </summary>
	/// <returns>The name.</returns>
	/// <param name="wall">Wall.</param>
    public static string getName(Transform wall)
    {
        return wall.name.Split('_')[0];
    }

	/// <summary>
	/// Gets the site.
	/// </summary>
	/// <returns>The site.</returns>
	/// <param name="wall">Wall.</param>
    public string getSite(Transform wall)
    {
        return wall.name.Split('_')[wall.name.Split('_').Length - 1];
    }

	/// <summary>
	/// Disables the walls with door or window.
	/// </summary>
    private void disableWallsWithDoorOrWindow()
    {
        if (!Mode.isBuildMode())
        {
            foreach (Transform wall in walls)
            {
                Transform dupWall = findDuplicateWall(wall);
                if (!disableWall(wall, dupWall))
                {
                    if (dupWall != null)
                    {
                        disableWall(dupWall, wall);
                    }
                }
            }
        }
    }

	/// <summary>
	/// Disables the wall.
	/// </summary>
	/// <returns><c>true</c>, if wall was disabled, <c>false</c> otherwise.</returns>
	/// <param name="wall">Wall.</param>
	/// <param name="secondWall">Second wall.</param>
    private bool disableWall(Transform wall, Transform secondWall)
    {
        if (wall.parent.childCount > 1)
        {
            for (int i = 0; i < wall.parent.childCount; i++)
            {
                if (wall.parent.GetChild(i).tag.Equals(Config.STRING_TYPE_EN_DOOR) ||
                    wall.parent.GetChild(i).tag.Equals(Config.STRING_TYPE_EN_WINDOW))
                {
                    wall.gameObject.SetActive(false);
                    if (secondWall != null)
                    {
                        secondWall.gameObject.SetActive(false);
                    }
                    return true;
                }
            }
        }
        return false;
    }
}
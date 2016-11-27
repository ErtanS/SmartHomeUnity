using UnityEngine;
using System.Collections;

public class Floors : Components
{
    private ArrayList floors;
    private Color colorOrg;
    private Color colorGrey = Color.grey;
    private static bool start;
    private bool colorSet = false;
    // Use this for initialization

	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!colorSet && GameobjectLoader.prefabsReady)
        {
            colorSet = true;
            GameObject temp = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_FLOOR));
            colorOrg = temp.GetComponent<Renderer>().material.GetColor(Config.SET_COLOR);
            Destroy(temp);
        }
        if (start)
        {
            start = false;
            StartCoroutine(manageFloorsIE());
        }
    }

	/// <summary>
	/// Manages the floors.
	/// </summary>
    public static void manageFloors()
    {
        start = true;
    }

	/// <summary>
	/// Refreshs the floor list.
	/// </summary>
    private void refreshFloorList()
    {
        floors = findGameObjectsWithTag(Config.STRING_PREFAB_FLOOR);
    }

	/// <summary>
	/// Manages the floors I.
	/// </summary>
	/// <returns>The floors I.</returns>
    private IEnumerator manageFloorsIE()
    {
        refreshFloorList();
        changeFloorColor();
        yield return null;
    }

	/// <summary>
	/// Changes the color of the floor.
	/// </summary>
    private void changeFloorColor()
    {
        changeColor(Mode.isBuildMode() ? colorGrey : colorOrg);
    }

	/// <summary>
	/// Changes the color.
	/// </summary>
	/// <param name="color">Color.</param>
    private void changeColor(Color color)
    {
        foreach (Transform floor in floors)
        {
            floor.GetComponent<Renderer>().material.SetColor(Config.SET_COLOR, color);
        }
    }
}
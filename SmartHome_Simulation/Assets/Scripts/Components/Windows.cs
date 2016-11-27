using UnityEngine;
using System.Collections;

/// <summary>
/// Windows.
/// </summary>
public class Windows : Components
{

    private ArrayList windows;
    private static bool start;

	/// <summary>
	/// Update this instance.
	/// </summary>
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(manageWindowsIE());
        }
    }

	/// <summary>
	/// Startet den Aktualisierungsvorgang.
	/// </summary>
    public static void manageWindows()
    {
        start = true;
    }

	/// <summary>
	/// Aktualisiert die Liste der Fenster.
	/// </summary>
    private void refreshWindowList()
    {
        windows = findGameObjectsWithTag(Config.STRING_TYPE_EN_WINDOW);
    }

	/// <summary>
	/// Coroutine zum Verwalten der Fenster.
	/// </summary>
    private IEnumerator manageWindowsIE()
    {
        refreshWindowList();
        enableWindows();
        yield return null;
    }

	/// <summary>
	/// Aktiviert bzw. deaktiviert die Fenster je nach Modus.
	/// </summary>
    private void enableWindows()
    {
        if (Mode.isBuildMode())
        {
            activateWindows(false);
        }
        else
        {
            activateWindows(true);
        }
    }

	/// <summary>
	/// Aktiviert die Fensterobjekte.
	/// </summary>
	/// <param name="active"> true oder false</param>
    private void activateWindows(bool active)
    {
        foreach (Transform window in windows)
        {
            window.gameObject.SetActive(active);
        }
    }
}
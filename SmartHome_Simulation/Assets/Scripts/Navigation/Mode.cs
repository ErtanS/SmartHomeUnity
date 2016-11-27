using UnityEngine;
using System.Collections;

public class Mode
{
    private static int currentMode;
    public const int MENU_MODE = 0;
    public const int BUILD_MODE = 1;
    public const int PLACE_MODE = 2;
    public const int PLAY_MODE = 3;
    public const int PLACE_SWITCH_MODE = 4;
    public const int PAUSE_MODE = 5;

    /// <summary>
    /// Changes to menu mode.
    /// </summary>
    public static void changeToMenuMode()
    {
        currentMode = MENU_MODE;
    }

    /// <summary>
    /// Changes to build mode.
    /// </summary>
    public static void changeToBuildMode()
    {
        currentMode = BUILD_MODE;
    }

    /// <summary>
    /// Changes to place mode.
    /// </summary>
    public static void changeToPlaceMode()
    {
        currentMode = PLACE_MODE;
    }

    /// <summary>
    /// Changes to play mode.
    /// </summary>
    public static void changeToPlayMode()
    {
        currentMode = PLAY_MODE;
    }

    /// <summary>
    /// Changes to place switch mode.
    /// </summary>
    public static void changeToPlaceSwitchMode()
    {
        currentMode = PLACE_SWITCH_MODE;
    }

    /// <summary>
    /// Changes to pause mode.
    /// </summary>
    public static void changeToPauseMode()
    {
        currentMode = PAUSE_MODE;
    }

    /// <summary>
    /// Ises the menu mode.
    /// </summary>
    /// <returns><c>true</c>, if menu mode was ised, <c>false</c> otherwise.</returns>
    public static bool isMenuMode()
    {
        return currentMode == MENU_MODE;
    }

    /// <summary>
    /// Ises the build mode.
    /// </summary>
    /// <returns><c>true</c>, if build mode was ised, <c>false</c> otherwise.</returns>
    public static bool isBuildMode()
    {
        return currentMode == BUILD_MODE;
    }

    /// <summary>
    /// Ises the place mode.
    /// </summary>
    /// <returns><c>true</c>, if place mode was ised, <c>false</c> otherwise.</returns>
    public static bool isPlaceMode()
    {
        return currentMode == PLACE_MODE;
    }

    /// <summary>
    /// Ises the play mode.
    /// </summary>
    /// <returns><c>true</c>, if play mode was ised, <c>false</c> otherwise.</returns>
    public static bool isPlayMode()
    {
        return currentMode == PLAY_MODE;
    }

    /// <summary>
    /// Ises the place switch mode.
    /// </summary>
    /// <returns><c>true</c>, if place switch mode was ised, <c>false</c> otherwise.</returns>
    public static bool isPlaceSwitchMode()
    {
        return currentMode == PLACE_SWITCH_MODE;
    }

    /// <summary>
    /// Ises the pause mode.
    /// </summary>
    /// <returns><c>true</c>, if pause mode was ised, <c>false</c> otherwise.</returns>
    public static bool isPauseMode()
    {
        return currentMode == PAUSE_MODE;
    }

    public static void setMode(int mode)
    {
        currentMode = mode;
    }
}
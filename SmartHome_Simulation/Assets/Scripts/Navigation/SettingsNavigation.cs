using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsNavigation : MonoBehaviour
{
    public static int numberOfPeople = 0;
    public static bool thiefActiveState = false;
    Slider sliderPeople;
    Slider sliderTime;
    Toggle toggleThief;

	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
        toggleThief = GameObject.Find(Config.OBJ_NAME_TOGGLE).GetComponent<Toggle>();
        sliderPeople = GameObject.Find(Config.OBJ_NAME_NUMBER).GetComponent<Slider>();
        sliderTime = GameObject.Find(Config.OBJ_NAME_TIMESPEED).GetComponent<Slider>();

        toggleThief.isOn = thiefActiveState;
        sliderPeople.value = numberOfPeople;
        sliderTime.value = Clock.timeSpeed;
    }

	/// <summary>
	/// change the thief state.
	/// </summary>
    public void thiefStateChanged()
    {
        thiefActiveState = toggleThief.isOn;
    }

	/// <summary>
	/// Numbers the people changed.
	/// </summary>
    public void numberPeopleChanged()
    {
        numberOfPeople = (int) sliderPeople.value;
    }

	/// <summary>
	/// change time speed.
	/// </summary>
    public void timeSpeedChanged()
    {
        Clock.timeSpeed = sliderTime.value;
    }

    public void mainMenu()
    {
        LevelManager.changeLevel(LevelManager.SCENE_MENU);
    }
}
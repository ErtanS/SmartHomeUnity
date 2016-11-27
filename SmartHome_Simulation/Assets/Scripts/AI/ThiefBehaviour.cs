using UnityEngine;
using System;
using System.Collections;

public class ThiefBehaviour : MonoBehaviour
{
    private int hour1 = -1;
    private int minute1 = -1;
    private int hour2 = -1;
    private int minute2 = -1;
    private int hour3 = -1;
    private int minute3 = -1;
    private Target target;
    private GameObject targetObject;
    private Grid grid;
    private Pathfinding pathfin;
    private GameObject start;
    private bool isFollowing = false;
    private bool stopped = false;

    private int oldMinute;
    // Use this for initialization
    void Start()
    {
        if (!SettingsNavigation.thiefActiveState)
        {
            transform.gameObject.SetActive(false);
        }
        else
        {
            target = transform.FindChild(Config.OBJ_NAME_TARGET).GetComponent<Target>();
            start = transform.FindChild(Config.OBJ_NAME_START).gameObject;
            pathfin = GetComponent<Pathfinding>();
            grid = GetComponent<Grid>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mode.isPlayMode() && !GameobjectLoader.isLoading())
        {
            stopped = false;
            int seed = unchecked(DateTime.Now.Ticks.GetHashCode());
            if (((Clock.hour == 2 && Clock.minute == 0) || (Clock.hour == 10 && Clock.minute == 0) ||
                 (Clock.hour == 18 && Clock.minute == 0)) && Clock.minute != oldMinute && !isFollowing)
            {
                findTarget();
            }

            if (Clock.hour == 12 && Clock.minute == 0 && Clock.minute != oldMinute)
            {
                hour1 = new System.Random(seed).Next(24);
                seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                minute1 = new System.Random(seed).Next(60);
                seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                hour2 = new System.Random(seed).Next(24);
                seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                minute2 = new System.Random(seed).Next(60);
                seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                hour3 = new System.Random(seed).Next(24);
                seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                minute3 = new System.Random(seed).Next(60);
                seed = unchecked(DateTime.Now.Ticks.GetHashCode());
            }
            oldMinute = Clock.minute;
            if (!isFollowing &&
                (Clock.hour == hour1 && Clock.minute == minute1 || Clock.hour == hour2 && Clock.minute == minute2 ||
                 Clock.hour == hour3 && Clock.minute == minute3))
            {
                seed = unchecked(DateTime.Now.Ticks.GetHashCode());
                if (new System.Random(seed).Next(5) == 0)
                {
                    isFollowing = true;
                    target.startFollowing();
                    print("START FOLLOWING");
                }
            }
        }
        else if (!stopped)
        {
            target.reset();
            stopped = true;
        }
    }

	/// <summary>
	/// Finds the target.
	/// </summary>
    private void findTarget()
    {
        ArrayList floorObjects = new ArrayList();
        foreach (string type in GameobjectLoader.floorObjects)
        {
            if (type.Equals(Config.STRING_TYPE_EN_STOVE))
            {
                foreach (GameObject temp in GameObject.FindGameObjectsWithTag(type))
                {
                    if (temp.transform.childCount == 3)
                    {
                        floorObjects.Add(temp);
                    }
                }
            }
            else
            {
                floorObjects.AddRange(GameObject.FindGameObjectsWithTag(type));
            }
        }
        if (floorObjects.Count > 0)
        {
            int seed = unchecked(DateTime.Now.Ticks.GetHashCode());

            int targetId = new System.Random(seed).Next(floorObjects.Count);
            targetObject = floorObjects[targetId] as GameObject;
            if (targetObject != null)
            {
                print(targetObject.name);

                grid.setTarget(targetObject);

                pathfin.setStart(start.transform);
                pathfin.setTarget(targetObject.transform);
            }
        }
        else
        {
            isFollowing = false;
        }
    }

	/// <summary>
	/// Sets the following.
	/// </summary>
	/// <param name="following">If set to <c>true</c> following.</param>
    public void setFollowing(bool following)
    {
        isFollowing = following;
    }
}
using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Target : MonoBehaviour
{
    public float waitForSeconds = 3;
    private ThiefBehaviour thiefBehaviour;
    private Pathfinding pathFinding;
    private GameObject thief;
    private int counter = 1;
    private ArrayList targets = new ArrayList();
    private bool wayBack;
    private GameObject start;
    private IEnumerator coroutine;
    private Vector3 oldPosition;
    private AICharacterControl control;
    private float deltaTime;
    private MessageManager message;

	/// <summary>
	/// Starts the following.
	/// </summary>
    public void startFollowing()
    {
        StartCoroutine(follow());
    }

	/// <summary>
	/// Start this instance.
	/// </summary>
    void Start()
    {
        message = GameObject.Find(Config.OBJ_NAME_CANVAS).GetComponent<MessageManager>();
        pathFinding = transform.parent.GetComponent<Pathfinding>();
        thiefBehaviour = transform.parent.GetComponent<ThiefBehaviour>();
        thief = transform.parent.FindChild(Config.STRING_THIEF).gameObject;
        control = thief.GetComponent<AICharacterControl>();
        start = transform.parent.FindChild(Config.OBJ_NAME_START).gameObject;
        coroutine = checkIfTargetIsMoving();
    }

	/// <summary>
	/// Follow the path.
	/// </summary>
    public IEnumerator follow()
    {
        float waitTime = 0;
        targets = pathFinding.getTargets();
        while (targets == null && waitTime < 10)
        {
            targets = pathFinding.getTargets();
            waitTime += Time.deltaTime;
            yield return null;
        }
        if (waitTime < 10)
        {
            thief.SetActive(true);
            wayBack = false;
            StartCoroutine(coroutine);
        }
        else
        {
            reset();
        }
        yield return null;
    }

	/// <summary>
	/// Checks if target is moving.
	/// </summary>
	/// <returns>The if target is moving.</returns>
    private IEnumerator checkIfTargetIsMoving()
    {
        deltaTime = 0;
        oldPosition = transform.position;
        while (true)
        {
            if (targets != null)
            {
                deltaTime += Time.deltaTime;
                if (oldPosition != transform.position)
                {
                    deltaTime = 0;
                    oldPosition = transform.position;
                }
                if (deltaTime > 10)
                {
                    if (!wayBack)
                    {
                        StartCoroutine(setWayToReturn());
                        wayBack = true;
                    }
                    else
                    {
                        message.addMessageToQueue(Config.MSG_THIEF_CAUGHT);
                        reset();
                    }
                }
            }
            yield return null;
        }
    }

	/// <summary>
	/// Sets the way to return.
	/// </summary>
	/// <returns>The way to return.</returns>
    private IEnumerator setWayToReturn()
    {
        transform.position = thief.transform.position;
        pathFinding.resetPath();
        pathFinding.stopSearching();
        pathFinding.setTargetToReturn(start.transform, thief.transform);
        targets = null;
        yield return new WaitForSeconds(waitForSeconds);
        float waitTime = 0;
        targets = pathFinding.getTargets();
        while (targets == null && waitTime < 10)
        {
            targets = pathFinding.getTargets();
            waitTime += Time.deltaTime;
            yield return null;
        }
        if (waitTime >= 10)
        {
            message.addMessageToQueue(Config.MSG_THIEF_CAUGHT);
            reset();
        }
        deltaTime = 0;
        counter = 1;
        yield return null;
    }

	/// <summary>
	/// Reset the Thief.
	/// </summary>
    public void reset()
    {
        StopCoroutine(coroutine);
        thiefBehaviour.setFollowing(false);
        thief.SetActive(false);
        counter = 1;
        thief.transform.position = new Vector3(-5, 0, -5);
        transform.position = new Vector3(0, 0, 0);
        pathFinding.calcTarget();
    }

	/// <summary>
	/// Raises the trigger stay event.
	/// </summary>
	/// <param name="col">Col.</param>
    void OnTriggerStay(Collider col)
    {
        if (col.tag.Equals(Config.STRING_THIEF) && targets != null)
        {
            if (counter < targets.Count)
            {
                transform.position = (Vector3) targets[counter++];
            }
        }
    }

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="col">Col.</param>
    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals(Config.STRING_THIEF) && targets != null && counter == targets.Count)
        {
            if (!wayBack)
            {
                StartCoroutine(setWayToReturn());
                wayBack = true;
            }
            else
            {
                reset();
            }
        }
    }
}
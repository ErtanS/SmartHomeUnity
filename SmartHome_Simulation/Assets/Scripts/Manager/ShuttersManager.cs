using System.Collections;
using UnityEngine;

public class ShuttersManager : MonoBehaviour
{
    private Animator animator;
    private bool waitUP = false;
    private bool waitDOWN = false;

    private int id;
    private ShuttersDataSet dataSet;

    /// <summary>
    /// Start this instance.
    /// </summary>
    public void Start()
    {
        animator = transform.FindChild(Config.OBJ_SHUTTERS_ROLLO).GetComponent<Animator>();
        StartCoroutine(changeScene());
    }

    /// <summary>
    /// Aktualisiert die Eigenschaften der Rollladen
    /// Fährt die Rollladen hoch(status = 0)
    /// Föhrt die Rolladen runter (status = 1)
    /// </summary>
    private void updateState()
    {
        dataSet = (ShuttersDataSet) DataManager.getDevice(name, dataSet);
        int status = dataSet.getState();

        if (status == 0 && !waitUP && animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_SHUTTERS_IDLE))
        {
            animator.SetTrigger(Config.ANIMATION_TRIGGER_UP);
            waitUP = true;
            waitDOWN = false;
        }
        else if (status == 1 && !waitDOWN & animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_SHUTTERS_UP_IDLE))
        {
            animator.SetTrigger(Config.ANIMATION_TRIGGER_DOWN);
            waitUP = false;
            waitDOWN = true;
        }
    }

    /// <summary>
    /// Coroutine zum Verändern der Umgebung(Rolladen hoch/runter) 
    /// </summary>
    private IEnumerator changeScene()
    {
        while (true)
        {
            if (DataManager.insertReady && Mode.isPlayMode() && !GameobjectLoader.isLoading())
            {
                updateState();
            }
            yield return new WaitForSeconds(Config.FLOAT_REFRESH_INTERVAL);
        }
    }
}
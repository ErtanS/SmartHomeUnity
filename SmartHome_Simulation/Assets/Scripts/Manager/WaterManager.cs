using UnityEngine;
using System.Collections;

public class WaterManager : MonoBehaviour
{
    private Animator animator;
    private bool waitUP = false;
    private bool waitDOWN = false;
    private int id;
    private WaterDataSet dataSet;
    private new string name;


    // Initialisation
    public void Start()
    {
        name = transform.parent.name;
        animator = transform.GetComponent<Animator>();
        StartCoroutine(changeScene());
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
    void Update()
    {
        name = transform.parent.name;
    }

    /// <summary>
    /// Aktualisiert die Eigenschaften der Rollladen
    /// Fährt die Rollladen hoch(status = 0)
    /// Föhrt die Rolladen runter (status = 1)
    /// </summary>
    private void updateState()
    {
        dataSet = (WaterDataSet) DataManager.getDevice(name, dataSet);
        int status = dataSet.getState();

        if (status == 0 && !waitUP && (animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_WATER_IDLE)))
        {
            animator.SetTrigger(Config.ANIMATION_TRIGGER_WATER_OFF);
            waitUP = true;
            waitDOWN = false;
        }
        else if (status == 1 && !waitDOWN & (animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_WATER_IDLE_OFF)))
        {
            animator.SetTrigger(Config.ANIMATION_TRIGGER_WATER_ON);
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
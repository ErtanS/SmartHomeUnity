using UnityEngine;
using System.Collections;

public class WindowManager : MonoBehaviour
{
    private Animator animator;
    private bool waitUP = false;
    private bool waitDOWN = false;
    private int id;
    private WindowDataSet dataSet;

    // Use this for initialization
    void Start()
    {
        animator = transform.FindChild(Config.OBJ_WINDOW_FENSTER).GetComponent<Animator>();
        StartCoroutine(changeScene());
    }

    /// <summary>
    /// Aktualisiert die Eigenschaften des Fensters
    /// Schließt das Fenster(status = 0)
    /// Öffnet das Fenster(status = 1)
    /// </summary>
    private void updateState()
    {
        dataSet = (WindowDataSet) DataManager.getDevice(name, dataSet);
        int status = dataSet.getState();
        if (status == 0 && !waitUP &&
            (animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_WINDOW_OPEN_IDLE)))
        {
            animator.SetTrigger(Config.ANIMATION_TRIGGER_WINDOW_CLOSE);
            waitUP = true;
            waitDOWN = false;
        }
        else if (status == 1 && !waitDOWN & (animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_WINDOW_IDLE)))
        {
            animator.SetTrigger(Config.ANIMATION_TRIGGER_WINDOW_OPEN);
            waitUP = false;
            waitDOWN = true;
        }
    }

    /// <summary>
    /// Coroutine zum Verändern der Umgebung(Fenster auf/zu) 
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
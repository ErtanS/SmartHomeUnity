using UnityEngine;
using System.Collections;

/// <summary>
/// Klasse zum Erstellen von neuen Threads
/// </summary>
public class ThreadedJob
{
    private bool m_IsDone = false;
    private object m_Handle = new object();
    private System.Threading.Thread m_Thread = null;

    public bool IsDone
    {
        get
        {
            bool tmp;
            lock (m_Handle)
            {
                tmp = m_IsDone;
            }
            return tmp;
        }
        set
        {
            lock (m_Handle)
            {
                m_IsDone = value;
            }
        }
    }

    /// <summary>
    /// Startet den Thread.
    /// </summary>
    public virtual void Start()
    {
        m_Thread = new System.Threading.Thread(Run);
        m_Thread.Start();
    }

    /// <summary>
    /// Bricht den Thread ab
    /// </summary>
    public virtual void Abort()
    {
        m_Thread.Abort();
    }

    protected virtual void ThreadFunction()
    {
    }

    protected virtual void OnFinished()
    {
    }

    /// <summary>
    /// Prüft, ob Thread mit seiner Aufgabe fertig ist.
    /// </summary>
    public virtual bool Update()
    {
        if (IsDone)
        {
            OnFinished();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Wartet bis Aufgabe beendet ist
    /// </summary>
    /// <returns>The for.</returns>
    IEnumerator WaitFor()
    {
        while (!Update())
        {
            yield return null;
        }
    }

    /// <summary>
    /// Führt die ThreadFunction aus
    /// </summary>
    private void Run()
    {
        ThreadFunction();
        IsDone = true;
    }
}
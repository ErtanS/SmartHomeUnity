using UnityEngine;

public class PathJob : ThreadedJob
{
    public bool findPath;
    public Pathfinding pathFinding;
    public Vector3 start;
    public Vector3 target;

    /// <summary>
    /// Startet neuen Thread zum Laden der Daten aus der Datenbank im Hintergrund.
    /// </summary>
    protected override void ThreadFunction()
    {
        findPath = pathFinding.FindPath(start, target);
    }
}
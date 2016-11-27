using UnityEngine;
using System.Collections;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public Node parent;

    /// <summary>
    /// Instanziert eine neue Instanz einer Node
    /// </summary>
    /// <param name="_walkable">Begehbar</param>
    /// <param name="_worldPos">Worldposition</param>
    /// <param name="_gridX">X Position auf Grid</param>
    /// <param name="_gridY">Y Position auf Grid</param>
    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    /// <summary>
    /// Kosten
    /// </summary>
    public int fCost
    {
        get { return gCost + hCost; }
    }
}
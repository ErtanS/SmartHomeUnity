using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Grid : MonoBehaviour
{
    private Vector2 gridWorldSize = new Vector2(24, 24);
    private const float nodeRadius = 0.1f;
    private List<Node> path;
    Node[,] grid;
    private AIManager manager = new AIManager();
    private GameObject target;
    private Pathfinding pathfin;
    float nodeDiameter;
    int gridSizeX, gridSizeY;
    private bool[,] targetGrid = new bool[30, 30];

    /// <summary>
    /// Methode die Ausgefüht wird, wenn das Objekt wieder aktiviert wird
    /// </summary>
    public void Awake()
    {
        nodeDiameter = nodeRadius*2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
        pathfin = GetComponent<Pathfinding>();
    }

    /// <summary>
    /// Erstellen des Grids
    /// </summary>
    public void startCreatingGrid()
    {
        StartCoroutine(CreateGrid());
    }

    /// <summary>
    /// Berechnung zur Erstellung des Grids
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right*gridWorldSize.x/2 -
                                  Vector3.forward*gridWorldSize.y/2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right*(x*nodeDiameter + nodeRadius) +
                                     Vector3.forward*(y*nodeDiameter + nodeRadius);
                bool currentState = manager.path[x, y];
                grid[x, y] = new Node(!currentState, worldPoint, x, y);
            }
        }
        pathfin.calcTarget();
        yield return null;
    }

    /// <summary>
    /// Veränderung des Status
    /// Status sagt aus, ob dieses Feld begehbar ist oder nicht
    /// </summary>
    /// <param name="x">X Position auf Grid</param>
    /// <param name="y">Y Position auf Grid</param>
    public void changeState(int x, int y)
    {
        if (grid != null && grid[x, y] != null)
        {
            grid[x, y].walkable = !manager.path[x, y];
        }
    }

    /// <summary>
    /// Nachbarn der Node ermitteln
    /// </summary>
    /// <param name="node">Node</param>
    /// <returns>Liste der Nachbarn</returns>
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    /// <summary>
    /// Umrechnen eines worldPoints zu einer Node im Grid
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns>Grid Position</returns>
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x/2)/gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y/2)/gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1)*percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1)*percentY);
        if (grid != null)
        {
            return grid[x, y];
        }
        return null;
    }

    //void OnDrawGizmos()
    //{
    //    if (name.Equals("ThiefBehaviour"))
    //    {
    //        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
    //        if (grid != null)
    //        {
    //            foreach (Node n in grid)
    //            {
    //                Gizmos.color = (n.walkable) ? Color.white : Color.red;
    //                if (path != null)
    //                    if (path.Contains(n))
    //                    {
    //                        Gizmos.color = Color.black;
    //                    }
    //                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// Registrierung des Pfades
    /// </summary>
    /// <returns></returns>
    private IEnumerator registerPathIE()
    {
        yield return new WaitForEndOfFrame();
        resetPath();
        GameObject[] walls = GameObject.FindGameObjectsWithTag(Config.STRING_PREFAB_WALL);
        foreach (GameObject wall in walls)
        {
            if (Math.Round(wall.transform.eulerAngles.y, 1)%360 == 90 ||
                Math.Round(wall.transform.eulerAngles.y, 1)%360 == 270)
            {
                manager.setPathValueWallY(wall.transform.position.x, wall.transform.position.z, true);
            }
            else
            {
                manager.setPathValueWallX(wall.transform.position.x, wall.transform.position.z, true);
            }
        }
        GameObject[] poles = GameObject.FindGameObjectsWithTag(Config.STRING_PREFAB_POLE);
        foreach (GameObject pole in poles)
        {
            manager.setPathValue(pole.transform.position.x, pole.transform.position.z);
        }
        GameObject[] doors = GameObject.FindGameObjectsWithTag(Config.STRING_TYPE_EN_DOOR);
        while (!DataManager.insertReady)
        {
            yield return null;
        }
        foreach (GameObject door in doors)
        {
            DoorDataSet data = (DoorDataSet) DataManager.getDevice(door.name, null);
            if (data != null)
            {
                if (tag.Equals(Config.STRING_THIEF_CONTROLLER) && data.getState() == 1)
                {
                    if (Math.Round(door.transform.eulerAngles.y, 1)%360 == 90 ||
                        Math.Round(door.transform.eulerAngles.y, 1)%360 == 270)
                    {
                        manager.setPathValueWallY(door.transform.position.x, door.transform.position.z, true);
                    }
                    else
                    {
                        manager.setPathValueWallX(door.transform.position.x, door.transform.position.z, true);
                    }
                }
            }
        }
        GameObject[] windows = GameObject.FindGameObjectsWithTag(Config.STRING_TYPE_EN_WINDOW);
        foreach (GameObject window in windows)
        {
            if (Math.Round(window.transform.eulerAngles.y, 1)%360 == 90 ||
                Math.Round(window.transform.eulerAngles.y, 1)%360 == 270)
            {
                manager.setPathValueWallY(window.transform.position.x, window.transform.position.z, true);
            }
            else
            {
                manager.setPathValueWallX(window.transform.position.x, window.transform.position.z, true);
            }
        }

        ArrayList floorObjects = new ArrayList();
        foreach (string type in GameobjectLoader.floorObjects)
        {
            floorObjects.AddRange(GameObject.FindGameObjectsWithTag(type));
        }
        floorObjects.AddRange(GameObject.FindGameObjectsWithTag(Config.STRING_PREFAB_SINK));
        foreach (GameObject temp in floorObjects)
        {
            if (!temp.tag.Equals(Config.STRING_TYPE_EN_STOVE))
            {
                Serializer serial = temp.GetComponent<Serializer>();
                manager.setPathValueFloorObjects(serial.getSize(), serial.getGridPos(), true);
            }
        }
        if (target != null)
        {
            Serializer serial = target.GetComponent<Serializer>();
            manager.setPathValueFloorObjects(serial.getSize(), serial.getGridPos(), false);
        }
        startCreatingGrid();
    }

    /// <summary>
    /// Starten der Coroutine zur Registrierung des Pfades
    /// </summary>
    public void registerPath()
    {
        StartCoroutine(registerPathIE());
    }

    /// <summary>
    /// Pfad zurücksetzen
    /// </summary>
    public void resetPath()
    {
        manager.path = new bool[120, 120];
        manager.pathCopy = new bool[120, 120];
    }

    /// <summary>
    /// Ziel für den Pfad festlegen
    /// </summary>
    /// <param name="target"></param>
    public void setTarget(GameObject target)
    {
        this.target = target;
        registerPath();
    }

    /// <summary>
    /// Gibt den Bereich des Zielobjektes frei
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool isPointOnTargetArea(int x, int y)
    {
        return manager.pathCopy[x, y];
    }
}
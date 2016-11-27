using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    private Transform target;
    private Transform start;
    private Transform currentTarget;
    private bool searching = false;
    private PathJob myJob = null;
    private List<Node> path = new List<Node>();


    void Awake()
    {
        grid = GetComponent<Grid>();
    }

    /// <summary>
    /// Starten des Pfad Jobs
    /// </summary>
    void Update()
    {
        if (target != null && start != null)
        {
            if (!searching && myJob == null)
            {
                myJob = new PathJob();
                myJob.pathFinding = this;
                myJob.start = start.position - new Vector3(11.9f, 0, 11.9f);
                myJob.target = target.position - new Vector3(11.9f, 0, 11.9f);
                myJob.Start();
            }
            if (myJob != null && myJob.findPath)
            {
                target = null;
                searching = false;
                myJob.Abort();
                myJob = null;
            }
        }
        else
        {
            searching = false;
        }
    }

    /// <summary>
    /// Setter StartTransform
    /// </summary>
    /// <param name="start"></param>
    public void setStart(Transform start)
    {
        this.start = start;
    }

    /// <summary>
    /// Setter target
    /// </summary>
    /// <param name="target"></param>
    public void setTarget(Transform target)
    {
        this.target = target;
        currentTarget = target;
    }

    /// <summary>
    /// Setter Target für den Rückweg
    /// </summary>
    /// <param name="target"></param>
    /// <param name="thief"></param>
    public void setTargetToReturn(Transform target, Transform thief)
    {
        this.target = target;
        start = thief;
    }

    /// <summary>
    /// Berechnung des Pfads von einem Start- zu einem Zielobjekt
    /// </summary>
    /// <param name="startPos">Startobjekt</param>
    /// <param name="targetPos">Zielobjekt</param>
    /// <returns></returns>
    public bool FindPath(Vector3 startPos, Vector3 targetPos)
    {
        searching = true;
        print("start searching");
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        if (targetNode.walkable)
        {
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                Node node = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                    {
                        if (openSet[i].hCost < node.hCost)
                            node = openSet[i];
                    }
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node == targetNode)
                {
                    RetracePath(startNode, targetNode);
                    print("Weg gefunden!!!!");
                    return true;
                }

                foreach (Node neighbour in grid.GetNeighbours(node))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = node;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
        print("kein Weg gefunden!!!!!");
        searching = false;
        return false;
    }

    /// <summary>
    /// Bildet den Pfad
    /// </summary>
    /// <param name="startNode"></param>
    /// <param name="endNode"></param>
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        this.path = path;
    }

    /// <summary>
    /// Ermitteln der Distanz
    /// </summary>
    /// <param name="nodeA"></param>
    /// <param name="nodeB"></param>
    /// <returns></returns>
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14*dstY + 10*(dstX - dstY);
        return 14*dstX + 10*(dstY - dstX);
    }

    /// <summary>
    /// Berechnung des Ziels
    /// </summary>
    public void calcTarget()
    {
        if (myJob != null)
        {
            myJob.Abort();
        }
        myJob = null;
        target = currentTarget;
    }

    /// <summary>
    /// Abbrechen des Suchvorgangs
    /// </summary>
    public void stopSearching()
    {
        searching = false;
    }

    /// <summary>
    /// Getter Target
    /// </summary>
    /// <returns></returns>
    public ArrayList getTargets()
    {
        ArrayList targets = new ArrayList();
        List<Node> temp = path;
        if (temp != null && temp.Count != 0)
        {
            foreach (Node n in temp)
            {
                if (!grid.isPointOnTargetArea(n.gridX, n.gridY))
                {
                    targets.Add(new Vector3(n.worldPosition.x, n.worldPosition.y, n.worldPosition.z));
                }
            }
            return targets;
        }
        return null;
    }

    /// <summary>
    /// Zurücksetzen des Pfads
    /// </summary>
    public void resetPath()
    {
        path = null;
    }

    /// <summary>
    /// Getter Pfad
    /// </summary>
    /// <returns></returns>
    public List<Node> getPath()
    {
        return path;
    }
}
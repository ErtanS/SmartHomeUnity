using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using System;
using System.Collections.Generic;

public class AIManager
{
    public bool[,] path = new bool[120, 120];
    public bool[,] pathCopy = new bool[120, 120];

	/// <summary>
	/// Sets the path value.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
    public void setPathValue(float x, float y)
    {
        double _x = Math.Round(x, 1);
        double _y = Math.Round(y, 1);

        double arrayX = Math.Round(_x/0.2f, 1);
        double arrayY = Math.Round(_y/0.2f, 1);

        path[(int) (arrayX), (int) (arrayY)] = true;
        path[(int) (arrayX + 1), (int) (arrayY)] = true;
        path[(int) (arrayX - 1), (int) (arrayY)] = true;
        path[(int) (arrayX), (int) (arrayY + 1)] = true;
        path[(int) (arrayX), (int) (arrayY - 1)] = true;
        path[(int) (arrayX - 1), (int) (arrayY - 1)] = true;
        path[(int) (arrayX - 1), (int) (arrayY + 1)] = true;
        path[(int) (arrayX + 1), (int) (arrayY + 1)] = true;
        path[(int) (arrayX + 1), (int) (arrayY - 1)] = true;
    }

	/// <summary>
	/// Sets the path value wall x.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="active">If set to <c>true</c> active.</param>
    public void setPathValueWallX(float x, float y, bool active)
    {
        double _x = Math.Round(x, 1);
        double _y = Math.Round(y, 1);
        for (int i = 0; i < 5; i++)
        {
            double arrayX = Math.Round((_x - 0.4f + i*0.2f)/0.2f, 0);
            double arrayY = Math.Round(_y/0.2f, 0);

            path[(int) arrayX, (int) arrayY] = active;
            path[(int) arrayX, (int) arrayY + 1] = active;
            path[(int) arrayX, (int) arrayY - 1] = active;
        }
    }

	/// <summary>
	/// Sets the path value wall y.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="active">If set to <c>true</c> active.</param>
    public void setPathValueWallY(float x, float y, bool active)
    {
        double _x = Math.Round(x, 1);
        double _y = Math.Round(y, 1);
        for (int i = 0; i < 5; i++)
        {
            double arrayX = Math.Round(_x/0.2f, 2);
            double arrayY = Math.Round((_y - 0.4f + (i*0.2f))/0.2f, 2);

            path[(int) arrayX, (int) arrayY] = active;
            path[(int) arrayX + 1, (int) arrayY] = active;
            path[(int) arrayX - 1, (int) arrayY] = active;
        }
    }

	/// <summary>
	/// Sets the path value floor objects.
	/// </summary>
	/// <param name="gridPos">Grid position.</param>
	/// <param name="active">If set to <c>true</c> active.</param>
    private void setPathValueFloorObjects(Vector2 gridPos, bool active)
    {
        double _x = Math.Round(gridPos.x*1.2f + 0.6f, 1);
        double _y = Math.Round(gridPos.y*1.2f + 0.6f, 1);

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                double arrayX = Math.Round((_x - 0.6f + (j*0.2f))/0.2f, 2);
                double arrayY = Math.Round((_y - 0.6f + (i*0.2f))/0.2f, 2);
                path[(int) arrayX, (int) arrayY] = active;
                if (!active)
                {
                    pathCopy[(int) arrayX, (int) arrayY] = !active;
                }
            }
        }
    }

	/// <summary>
	/// Sets the path value floor objects.
	/// </summary>
	/// <param name="size">Size.</param>
	/// <param name="gridPos">Grid position.</param>
	/// <param name="active">If set to <c>true</c> active.</param>
    public void setPathValueFloorObjects(Vector2 size, Vector2 gridPos, bool active)
    {
        for (int i = 0; i < Math.Abs(size.x); i++)
        {
            for (int j = 0; j < Math.Abs(size.y); j++)
            {
                setPathValueFloorObjects(
                    new Vector2((int) gridPos.x + i*(int) (size.x/Math.Abs(size.x)),
                        (int) gridPos.y + j*(int) (size.y/Math.Abs(size.y))), active);
            }
        }
    }
}
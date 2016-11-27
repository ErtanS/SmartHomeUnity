using UnityEngine;
using System.Collections;

public class Components : MonoBehaviour
{
	/// <summary>
	/// Finds the game objects with tag.
	/// </summary>
	/// <returns>The game objects with tag.</returns>
	/// <param name="tag">Tag.</param>
    public static ArrayList findGameObjectsWithTag(string tag)
    {
        ArrayList search = getAllTransforms();
        ArrayList result = new ArrayList();
        foreach (Transform trans in search)
        {
            if (trans.tag.Equals(tag))
            {
                result.Add(trans);
            }
        }
        return result;
    }

	/// <summary>
	/// Gets all transforms.
	/// </summary>
	/// <returns>The all transforms.</returns>
    private static ArrayList getAllTransforms()
    {
        ArrayList result = new ArrayList();
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            if (o.tag.Equals(Config.STRING_PREFAB_EMPTY) && o.transform.parent == null)
            {
                result.AddRange(o.GetComponentsInChildren<Transform>(true));
            }
        }
        return result;
    }

	/// <summary>
	/// Moves the object on right position.
	/// </summary>
	/// <param name="tran">Tran.</param>
	/// <param name="site">Site.</param>
	/// <param name="delta">Delta.</param>
    public static void moveObjectOnRightPosition(Transform tran, string site, float delta)
    {
        if (site.Equals(Config.STRING_ABOVE))
        {
            tran.position += new Vector3(0, 0, -delta);
        }
        else if (site.Equals(Config.STRING_BELOW))
        {
            tran.position += new Vector3(0, 0, delta);
        }
        else if (site.Equals(Config.STRING_LEFT))
        {
            tran.position += new Vector3(delta, 0, 0);
        }
        else if (site.Equals(Config.STRING_RIGHT))
        {
            tran.position += new Vector3(-delta, 0, 0);
        }
    }
}
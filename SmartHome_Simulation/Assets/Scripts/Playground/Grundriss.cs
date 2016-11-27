using UnityEngine;
using System.Collections;

public class Grundriss : MonoBehaviour
{
    public static ArrayList currentRoom = new ArrayList();
    public static bool isClickable = true;
    public static GameObject roomTemplate;
    public static ArrayList roomNames = new ArrayList();

    private wandGenerator generator = new wandGenerator();
    private Texture2D textureGreen;
    private GameObject cubeTemplate;
    private GameObject wallTemplate;
    private GameObject grid;
    private GameObject parentTemplate;
    private MessageManager message;

    private bool firstStart;

    // Use this for initialization
    void Update()
    {
        if (GameobjectLoader.prefabsReady && !firstStart)
        {
            firstStart = true;
            grid = GameObject.Find(Config.STRING_GAMEOBJECT_MANAGER);
            cubeTemplate = GameobjectLoader.getPrefab(Config.PREFAB_GRID_CUBE);
            wallTemplate = GameobjectLoader.getPrefab(Config.STRING_PREFAB_WALL);
            parentTemplate = GameobjectLoader.getPrefab(Config.STRING_PREFAB_EMPTY);
            textureGreen = createTexture(1, 1, Color.green);
            roomTemplate = Instantiate(parentTemplate);
            roomTemplate.tag = Config.STRING_UNDESTROYABLE;
            message = GameObject.Find(Config.OBJ_NAME_MESSAGE).GetComponent<MessageManager>();
            message.setImportantMessage(Config.MSG_DEFAULT_GRID_MODE_TEXT);
            createGrid(20, 20);
        }
    }

	/// <summary>
	/// Creates the grid.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
    private void createGrid(int x, int z)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                if (!(i == 0 || j == 0 || i == 19 || j == 19))
                {
                    GameObject cube = Instantiate(cubeTemplate);
                    cube.transform.position = new Vector3(i*1.2f + 0.6f, -0.05f, j*1.2f + 0.6f);
                    Material mat = cube.GetComponent<Renderer>().material;
                    if (i%2 == 0 && j%2 == 0 || i%2 == 1 && j%2 == 1)
                    {
                        mat.SetTexture("_MainTex", textureGreen);
                    }
                    cube.name = "cube_" + i + "_" + j;
                    cube.transform.SetParent(grid.transform);
                }
            }
        }
    }

	/// <summary>
	/// Draws the room.
	/// </summary>
	/// <param name="currentPoint">Current point.</param>
	/// <param name="poleParent">Pole parent.</param>
	/// <param name="roomParent">Room parent.</param>
    public void drawRoom(Vector2 currentPoint, GameObject poleParent, GameObject roomParent)
    {
        Vector2 left = new Vector2(currentPoint.x - 1, currentPoint.y);
        Vector2 right = new Vector2(currentPoint.x + 1, currentPoint.y);
        Vector2 below = new Vector2(currentPoint.x, currentPoint.y - 1);
        Vector2 above = new Vector2(currentPoint.x, currentPoint.y + 1);

        bool drawLeft = true;
        bool drawRight = true;
        bool drawBelow = true;
        bool drawAbove = true;

        foreach (Vector2 temp in currentRoom)
        {
            if (temp == left)
            {
                drawLeft = false;
            }
            if (temp == right)
            {
                drawRight = false;
            }
            if (temp == below)
            {
                drawBelow = false;
            }
            if (temp == above)
            {
                drawAbove = false;
            }
        }
        string parentName = roomParent.name;
        GameObject positionParent =
            GameObject.Find(roomParent.name)
                .transform.FindChild("pos:" + currentPoint.x + ":" + currentPoint.y)
                .gameObject;

        if (drawLeft)
        {
            GameObject leftParent = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_EMPTY));
            leftParent.name = Config.STRING_LEFT + "_" + positionParent.name;
            leftParent.transform.SetParent(positionParent.transform);

            GameObject wallLeft = generator.createWall(currentPoint.x*1.2f - 0.5f, currentPoint.y*1.2f + 0.5f, 1f,
                Instantiate(wallTemplate), parentName + "_" + Config.STRING_LEFT, 90);
            wallLeft.transform.SetParent(leftParent.transform);

            GameObject pole = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_POLE));
            GameObject poleLeft = generator.createPole(pole, currentPoint.x*1.2f, currentPoint.y*1.2f,
                parentName + "_" + Config.STRING_LEFT);
            poleLeft.transform.SetParent(poleParent.transform);
        }
        if (drawRight)
        {
            GameObject rightParent = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_EMPTY));
            rightParent.name = Config.STRING_RIGHT + "_" + positionParent.name;
            rightParent.transform.SetParent(positionParent.transform);

            GameObject wallRight = generator.createWall(currentPoint.x*1.2f + 0.7f, currentPoint.y*1.2f + 0.5f, 1,
                Instantiate(wallTemplate), parentName + "_" + Config.STRING_RIGHT, 270);
            wallRight.transform.SetParent(rightParent.transform);
            GameObject pole = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_POLE));
            GameObject poleRight = generator.createPole(pole, (currentPoint.x + 1)*1.2f, (currentPoint.y + 1)*1.2f,
                parentName + "_" + Config.STRING_RIGHT);
            poleRight.transform.SetParent(poleParent.transform);
        }
        if (drawBelow)
        {
            GameObject belowParent = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_EMPTY));
            belowParent.name = Config.STRING_BELOW + "_" + positionParent.name;
            belowParent.transform.SetParent(positionParent.transform);

            GameObject wallBelow = generator.createWall(currentPoint.x*1.2f + 0.1f, currentPoint.y*1.2f - 0.1f, 1,
                Instantiate(wallTemplate), parentName + "_" + Config.STRING_BELOW, 0);
            wallBelow.transform.SetParent(belowParent.transform);
            GameObject pole = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_POLE));
            GameObject poleBelow = generator.createPole(pole, (currentPoint.x + 1)*1.2f, (currentPoint.y)*1.2f,
                parentName + "_" + Config.STRING_BELOW);
            poleBelow.transform.SetParent(poleParent.transform);
        }
        if (drawAbove)
        {
            GameObject aboveParent = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_EMPTY));
            aboveParent.name = Config.STRING_ABOVE + "_" + positionParent.name;
            aboveParent.transform.SetParent(positionParent.transform);

            GameObject wallAbove = generator.createWall(currentPoint.x*1.2f + 0.1f, currentPoint.y*1.2f + 1.1f, 1,
                Instantiate(wallTemplate), parentName + "_" + Config.STRING_ABOVE, 180);
            wallAbove.transform.SetParent(aboveParent.transform);
            GameObject pole = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_POLE));
            GameObject poleAbove = generator.createPole(pole, (currentPoint.x)*1.2f, (currentPoint.y + 1)*1.2f,
                parentName + "_" + Config.STRING_ABOVE);
            poleAbove.transform.SetParent(poleParent.transform);
        }

        GameObject ceiling = Instantiate(GameobjectLoader.getPrefab(Config.STRING_PREFAB_CEILING));
        ceiling.transform.position = new Vector3(currentPoint.x*1.2f + 0.6f, 2.451f, currentPoint.y*1.2f + 0.6f);
        ceiling.transform.SetParent(positionParent.transform);
        ceiling.name = Config.STRING_PREFAB_CEILING + "_" + positionParent.name + "_" + parentName;
    }

	/// <summary>
	/// Creates the texture.
	/// </summary>
	/// <returns>The texture.</returns>
	/// <param name="width">Width.</param>
	/// <param name="height">Height.</param>
	/// <param name="color">Color.</param>
    private Texture2D createTexture(int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        for (int i = 0; i <= width; i++)
        {
            for (int j = 0; j <= height; j++)
            {
                texture.SetPixel(i, j, color);
            }
        }
        texture.Apply();
        return texture;
    }

	/// <summary>
	/// Resets the room template.
	/// </summary>
    public void resetRoomTemplate()
    {
        Destroy(roomTemplate);
        roomTemplate = Instantiate(parentTemplate);
        roomTemplate.tag = Config.STRING_UNDESTROYABLE;
        currentRoom.Clear();
        isClickable = true;
    }
}
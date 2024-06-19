using System.Collections.Generic;
using UnityEngine;

public class GenerateLayout : MonoBehaviour
{
    [InspectorButton("ButtonNewLayout")]
    public bool NewLayout;

    [Header("Settings")]
    private GameObject[,] gameobjectGridArray;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int spaceBetweenGridCells;

    [SerializeField] private List<GameObject> tempList;
    [SerializeField] private GameObject randomFromList;
    private GameObject spawnedStuff;

    [Header("Light Containment")]
    [SerializeField] private int longHallLength;
    [SerializeField] private int longHallTopOffset;
    [SerializeField] private int longHallLeftOffset;

    [SerializeField] private int connectingHallLength;

    void Awake()
    {
        CreateGrid();
    }

    void ButtonNewLayout()
    {
        DestroyLevel();
        CreateGrid();
    }

    void CreateGrid()
    {
        gameobjectGridArray = new GameObject[width, height];

        spawnedStuff = new GameObject("Spawned Stuff");

        // for (int w = 0; w < width; w++)
        // {
        //     for (int h = 0; h < height; h++)
        //     {
        //         GameObject spawnedObj = new GameObject(h + ", " + w);
        //         spawnedObj.transform.position = new Vector3(w, 0, h) * spaceBetweenGridCells;
        //         spawnedObj.transform.parent = spawnedStuff.transform;

        //         grid[w,h] = spawnedObj;
        //     }
        // }

        GenerateLongHallways();
    }

    void GenerateLongHallways()
    {
        for (int i = 0; i < longHallLength; i++)
        {
            GameObject longHallRoom = GameObject.CreatePrimitive(PrimitiveType.Cube);
            PositionRoom(longHallRoom, i + longHallLeftOffset, longHallTopOffset);
            tempList.Add(longHallRoom);
        }

        GenerateConnectingHalls();

        for (int i = 0; i < longHallLength; i++)
        {
            GameObject longHallRoom = GameObject.CreatePrimitive(PrimitiveType.Cube);
            PositionRoom(longHallRoom, i + longHallLeftOffset, longHallTopOffset + connectingHallLength + 1);
        }
    }

    void GenerateConnectingHalls()
    {
        int spaceAvailable = longHallLength;
        int randomDistance = Random.Range(0, 3);

        for (int spawnDistance = 0; spawnDistance < spaceAvailable;)
        {
            if (spawnDistance + randomDistance > tempList.Count - 1)
            {
                break;
            }
            else
            {
                for (int i = 0; i < connectingHallLength; i++)
                {
                    GameObject chosenHallBranchRoom = tempList[spawnDistance + randomDistance];
                    GameObject connectingHallRoom = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    PositionRoom(connectingHallRoom, (int)chosenHallBranchRoom.transform.position.x / spaceBetweenGridCells, (int)chosenHallBranchRoom.transform.position.z / spaceBetweenGridCells + i + 1);
                }
                
                spawnDistance += randomDistance;
                randomDistance = Random.Range(2, 6);
            }
        }
    }

    void PositionRoom(GameObject room, int posX, int posZ)
    {
        room.transform.position = new Vector3(posX, 0, posZ) * spaceBetweenGridCells;
        gameobjectGridArray[posX, posZ] = room;

        room.transform.parent = spawnedStuff.transform;
    }
    
    void DestroyLevel()
    {
        Destroy(spawnedStuff);
        foreach (GameObject ob in tempList)
        {
            Destroy(ob);
        }
        tempList.Clear();


        Debug.Log("Level gone!");
    }
}

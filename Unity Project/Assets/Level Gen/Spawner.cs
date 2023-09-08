using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> roomList;
    public GameObject[] spawnPoints; 

    private void Start()
    {
        int randFromList = Random.Range(0, roomList.Count); 
        GameObject room = Instantiate(roomList[randFromList]);

        Invoke("FindSpawnPoints", 2f);
    }

    void FindSpawnPoints()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject point in spawnPoints)
        {
            int randFromList = Random.Range(0, roomList.Count); 
            GameObject newRoom = Instantiate(roomList[randFromList], point.transform);
            // CheckForOverlap(newRoom);
        }

        Invoke("FindSpawnPoints", 1f);
    }

    // public void CheckForOverlap(GameObject newRoom)
    // {
    //     Collider newRoomBounds = newRoom.GetComponent<Collider>();

    //     if (newRoomBounds.bounds.Intersects(newRoomBounds.bounds))
    //     {
    //         newRoom.transform.Rotate(0, 90, 0);
    //         CheckForOverlap(newRoom);
    //     }
    //     else
    //     {
    //         //Continue
    //     }
        
    // }
}

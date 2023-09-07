using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> roomList;    

    private void Start()
    {
        PickRandomToSpawn();
    }

    void PickRandomToSpawn()
    {
        int randFromList = Random.Range(0, roomList.Count - 1); 
        GameObject firstRoom = Instantiate(roomList[randFromList]);
    }

}

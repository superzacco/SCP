using System;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateLayout : MonoBehaviour
{
    [SerializeField] GameObject cap;
    [SerializeField] GameObject hallway;
    
    void Awake()
    {
        Array spawnPoints;

        GameObject firstroom = Instantiate(hallway);

        spawnPoints = GameObject.FindGameObjectsWithTag("RSPoint");
        Debug.Log(spawnPoints);

        foreach (GameObject room in spawnPoints)
        {
            Instantiate(cap);
        }
    }
}

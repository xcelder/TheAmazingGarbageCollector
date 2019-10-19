using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCreator : MonoBehaviour
{
    [SerializeField] private Transform debrisContainer;

    [Header("Debris Prefabs")]
    [SerializeField] private GameObject smallDebris;
    [SerializeField] private GameObject mediumDebris;
    [SerializeField] private GameObject largeDebris;
    [Header("Test Garbage Amounts")]
    [SerializeField] private int smallAmount;
    [SerializeField] private int mediumAmount;
    [SerializeField] private int largeAmount;


    void Start()
    {
        // Create the initial debris.
        for (int i = 0; i < smallAmount; i++)
        {
            GameObject debris = CreateDebris(smallDebris, debrisContainer);
        }
        for (int i = 0; i < mediumAmount; i++)
        {
            GameObject debris = CreateDebris(mediumDebris, debrisContainer);
        }
        for (int i = 0; i < largeAmount; i++)
        {
            GameObject debris = CreateDebris(largeDebris, debrisContainer);
        }
    }


    private GameObject CreateDebris(GameObject prefab, Transform container)
    {
        GameObject debris = Instantiate(smallDebris, debrisContainer);
        // TODO: Set random position.
        // TODO: Set random rotation.
        return debris;
    }

}

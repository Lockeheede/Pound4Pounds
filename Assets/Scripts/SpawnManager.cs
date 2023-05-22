using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] cakePrefabs;

    private float spawnLimitLeft = -22;

    private float startDelay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        float spawnInterval = Random.Range(3f, 5f);
        InvokeRepeating("SpawnRandomCake", startDelay, spawnInterval);
    }

    void SpawnRandomCake()
    {
        int cakes = Random.Range(0, cakePrefabs.Length);
        Vector3 spawnPos = new Vector3(8, 1, 0);

        Instantiate(cakePrefabs[cakes], spawnPos, cakePrefabs[cakes].transform.rotation);
    }
}

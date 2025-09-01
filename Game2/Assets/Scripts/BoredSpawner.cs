using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoredSpawner: MonoBehaviour
{
    public GameObject BoredsSpawner;
    public Transform minX;
    public Transform maxX;
    public float minYDistance = 1.5f; 
    public float maxYDistance = 3f;
    private float spawnDelay = 1.5f;
    private float  firstPointY= 0f;
    private float lastY;
    void Start()
    {
        lastY = firstPointY;
        StartCoroutine(SpawnPlatformsRoutine());
    }

    IEnumerator SpawnPlatformsRoutine()
    {
        while (true)
        {
            SpawnBored();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnBored()
    {
        float randomX = Random.Range(minX.position.x, maxX.position.x);
        float randomY = lastY + Random.Range(minYDistance, maxYDistance);
       
        Vector2 spawnPosition = new Vector2(randomX, randomY);
        Instantiate(BoredsSpawner, spawnPosition, Quaternion.identity);


        lastY = randomY;
    }
    void Update()
    {
        
    }
}

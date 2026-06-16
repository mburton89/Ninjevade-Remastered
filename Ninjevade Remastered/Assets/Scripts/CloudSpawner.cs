using UnityEngine;
using System.Collections.Generic;

public class CloudSpawner : MonoBehaviour
{
    [Header("Cloud Settings")]
    public List<GameObject> cloudPrefabs;

    [Header("Spawn Position")]
    public float spawnX = -20f;
    public float minY = 3f;
    public float maxY = 10f;
    public float spawnZ = 0f;

    [Header("Timing")]
    public float spawnInterval = 2f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnCloud();
            timer = 0f;
        }
    }

    private void SpawnCloud()
    {
        Vector3 spawnPos = new Vector3(
            spawnX,
            Random.Range(minY, maxY),
            spawnZ
        );

        int rand = Random.Range(0, cloudPrefabs.Count);

        Instantiate(cloudPrefabs[rand], spawnPos, Quaternion.identity, this.transform);
    }
}
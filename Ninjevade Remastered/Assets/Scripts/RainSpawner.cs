using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    private float _secondsBetweenSpawns;
    private float _maxXOffset;
    private float _force;
    [SerializeField] private RainDrop _rainDropPrefab;

    public void Init(RainManager rainManager)
    {
        _secondsBetweenSpawns = rainManager.secondsBetweenSpawns;
        _maxXOffset = rainManager.maxXOffset;
         transform.eulerAngles = new Vector3(0, 0, rainManager.rotation);
        _force = rainManager.force;
        StartCoroutine(SpawnRainDropCo());
    }

    private IEnumerator SpawnRainDropCo()
    {
        float xOffset = Random.Range(-_maxXOffset, _maxXOffset);
        Vector3 spawnPosition = new Vector3(transform.position.x + xOffset, transform.position.y);
        RainDrop rainDrop = Instantiate(_rainDropPrefab, spawnPosition, transform.rotation);
        rainDrop.rigidBody2D.AddForce(transform.up * _force);
        yield return new WaitForSeconds(_secondsBetweenSpawns);
        StartCoroutine(SpawnRainDropCo());
    }
}

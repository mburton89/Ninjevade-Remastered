using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float _leftSideOfScreenXPos;
    private float _rightSideOfScreenXPos;
    [SerializeField] private float _enemySpawnXDistanceModifier;
    [SerializeField] private float _enemySpawnYPosition;
    [SerializeField] private Enemy _enemyPrefab;
    private int _enemyCount;
    [SerializeField] private GameObject _leftEnemyThrowTrigger;
    [SerializeField] private GameObject _rightEnemyThrowTrigger;
    [SerializeField] private float _triggerDistanceFromEdge;

    void Start()
    {
        _leftSideOfScreenXPos = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f)).x;
        _rightSideOfScreenXPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, 0.0f)).x;
        _leftEnemyThrowTrigger.transform.position = new Vector3(_leftSideOfScreenXPos + _triggerDistanceFromEdge, _leftEnemyThrowTrigger.transform.position.y);
        _rightEnemyThrowTrigger.transform.position = new Vector3(_rightSideOfScreenXPos - _triggerDistanceFromEdge, _rightEnemyThrowTrigger.transform.position.y);
        SpawnRandomSequence();
    }

    public void HandleEnemyDestroyed()
    {
        _enemyCount--;
        if (_enemyCount == 0 && !WaveManager.Instance.waveComplete)
        {
            SpawnRandomSequence();
        }
    }

    void SpawnRandomSequence()
    {
        int sequence = Random.Range(0, 3);
        print("sequence "+ sequence);
        if (sequence == 0)
        {
            SpawnEnemyOnLeft();
        }
        else if (sequence == 1)
        {
            SpawnEnemyOnRight();
        }
        else
        {
            SpawnEnemyOnLeft();
            SpawnEnemyOnRight();
        }
    }

    void SpawnEnemyOnLeft()
    {
        Vector3 spawnPosition = new Vector3(_leftSideOfScreenXPos - _enemySpawnXDistanceModifier, _enemySpawnYPosition, 0);
        Enemy enemy = Instantiate(_enemyPrefab, spawnPosition, transform.rotation);
        enemy.Init(this, true, WaveManager.Instance.currentNinjaRunSpeed);
        _enemyCount++;
        print("_enemyCount " + _enemyCount);
    }

    void SpawnEnemyOnRight()
    {
        Vector3 spawnPosition = new Vector3(_rightSideOfScreenXPos + _enemySpawnXDistanceModifier, _enemySpawnYPosition, 0);
        Enemy enemy = Instantiate(_enemyPrefab, spawnPosition, transform.rotation);
        enemy.Init(this, false, WaveManager.Instance.currentNinjaRunSpeed);
        _enemyCount++;
        print("_enemyCount " + _enemyCount);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    public float secondsBetweenSpawns;
    public float rotation;
    public float force;
    public float maxXOffset;
    [SerializeField] private List<RainSpawner> _rainSpawners;

    void Start()
    {
        foreach (RainSpawner rainSpawner in _rainSpawners)
        {
            rainSpawner.Init(this);
        }
    }
}

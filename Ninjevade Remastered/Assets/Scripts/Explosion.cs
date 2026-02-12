using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ExplosionChunk _explosionChunkPrefab;
    [SerializeField] private int _intensity;
    [SerializeField] private int _amountOfChunks;
    private AudioSource _audioSource;

    public void Splode()
    {
        //ScreenShaker.Instance.ShakeScreen();
        _audioSource.Play();

        for (int i = 0; i < _amountOfChunks; i++)
        {
            ExplosionChunk explosionChunk = Instantiate(_explosionChunkPrefab, transform.position, transform.rotation, transform);
            explosionChunk.Init(_intensity);
        }

        Destroy(gameObject, 1.5f);
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Splode();
    }
}

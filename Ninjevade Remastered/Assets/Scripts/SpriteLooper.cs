using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLooper : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private float _secondsBetweenFrames;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Play();
    }

    public void Play()
    {
        StartCoroutine(nameof(loopThroughSprites));
    }

    public void PlayOnce()
    {
        StartCoroutine(nameof(loopThroughSpritesOnce));
    }

    public void Stop()
    {
        StopCoroutine(nameof(loopThroughSprites));
    }

    IEnumerator loopThroughSprites()
    {
        foreach (Sprite sprite in _sprites)
        {
            _spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(_secondsBetweenFrames);
        }

        StartCoroutine(nameof(loopThroughSprites));
    }

    IEnumerator loopThroughSpritesOnce()
    {
        foreach (Sprite sprite in _sprites)
        {
            _spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(_secondsBetweenFrames);
        }
    }
}

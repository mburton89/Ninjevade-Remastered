using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [Header("Animation Sprites")]
    public List<Sprite> idleSprites;
    public Sprite jumpSprite;
    public Sprite fallSprite;
    public List<Sprite> deflectSprites;

    [Header("Animation Timing")]
    public float secondsBetweenFramesForIdle = 0.15f;
    public float secondsBetweenFramesForDeflect = 0.08f;
    // You can easily add more timings later if needed
    // public float secondsBetweenFramesForJump = 0.1f;  // (not used currently since jump is single sprite)

    private Coroutine activeCoroutine;
    private string currentAnimation = "";

    /// <summary>
    /// Initialize or reset the animator (starts with Idle)
    /// </summary>
    public void Init()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
        }
        PlayIdle();
    }

    /// <summary>
    /// Plays the Idle animation (looping)
    /// </summary>
    public void PlayIdle()
    {
        if (currentAnimation == "Idle") return;

        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        currentAnimation = "Idle";
        activeCoroutine = StartCoroutine(LoopAnimation(idleSprites, secondsBetweenFramesForIdle));
    }

    /// <summary>
    /// Sets the single Jump sprite (stays until another animation is called)
    /// </summary>
    public void PlayJump()
    {
        if (currentAnimation == "Jump") return;

        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        currentAnimation = "Jump";
        spriteRenderer.sprite = jumpSprite;
        // Single sprite → no coroutine needed
    }

    /// <summary>
    /// Sets the single Fall sprite (stays until another animation is called)
    /// </summary>
    public void PlayFall()
    {
        if (currentAnimation == "Fall") return;

        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        currentAnimation = "Fall";
        spriteRenderer.sprite = fallSprite;
        // Single sprite → no coroutine needed
    }

    /// <summary>
    /// Plays the Deflect animation once, then returns to Idle
    /// </summary>
    public void PlayDeflect()
    {
        if (activeCoroutine != null)
            StopCoroutine(activeCoroutine);

        currentAnimation = "Deflect";
        activeCoroutine = StartCoroutine(PlayDeflectOnce());
    }

    private IEnumerator LoopAnimation(List<Sprite> sprites, float frameTime)
    {
        if (sprites == null || sprites.Count == 0)
        {
            Debug.LogWarning("Idle sprite list is empty!");
            yield break;
        }

        while (true)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                spriteRenderer.sprite = sprites[i];
                yield return new WaitForSeconds(frameTime);
            }
        }
    }

    private IEnumerator PlayDeflectOnce()
    {
        if (deflectSprites == null || deflectSprites.Count == 0)
        {
            Debug.LogWarning("Deflect sprite list is empty!");
            PlayIdle();
            yield break;
        }

        for (int i = 0; i < deflectSprites.Count; i++)
        {
            spriteRenderer.sprite = deflectSprites[i];
            yield return new WaitForSeconds(secondsBetweenFramesForDeflect);
        }

        PlayIdle();
    }
}
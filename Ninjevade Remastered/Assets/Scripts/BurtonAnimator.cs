using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurtonAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public List<Sprite> idleSprites;
    public  List<Sprite> blinkSprites;
    
    public List<Sprite> shoutSprites;

    public float secondsBetweenFrames;

    Coroutine activeCoroutine;

    int timesNotPickedRandomAnimationInARow;
    int maxTimesNotToPickRandomAnimationInARow = 6;

    public void Init()
    {
        if (activeCoroutine != null)
        { 
            StopCoroutine(activeCoroutine);
        }
        ChooseAnimation();
    }

    void ChooseAnimation()
    {
        int rand = Random.Range(1, 8);
        if (rand == 2)
        {
            activeCoroutine = StartCoroutine(LoopBlink());
        }
        else
        {
            timesNotPickedRandomAnimationInARow++;
            if (timesNotPickedRandomAnimationInARow >= maxTimesNotToPickRandomAnimationInARow)
            {
                activeCoroutine = StartCoroutine(LoopBlink());
                timesNotPickedRandomAnimationInARow = 0;
            }
            else
            {
                activeCoroutine = StartCoroutine(LoopIdle());
            }
        }
    }

    private IEnumerator LoopIdle()
    { 
        for (int i = 0; i < idleSprites.Count; i++) 
        {
            spriteRenderer.sprite = idleSprites[i];
            yield return new WaitForSeconds(secondsBetweenFrames);
        }
        ChooseAnimation();
    }

    private IEnumerator LoopBlink()
    {
        for (int i = 0; i < blinkSprites.Count; i++)
        {
            spriteRenderer.sprite = blinkSprites[i];
            yield return new WaitForSeconds(secondsBetweenFrames);
        }
        ChooseAnimation();
    }
}

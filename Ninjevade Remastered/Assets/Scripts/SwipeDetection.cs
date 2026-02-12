using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 touchStartPos;
    private float minSwipeDistance = 50f; // Minimum distance required for a swipe
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 touchEndPos = touch.position;
                    Vector2 swipeDirection = touchEndPos - touchStartPos;

                    if (swipeDirection.magnitude >= minSwipeDistance)
                    {
                        swipeDirection.Normalize();

                        // Detect swipe up
                        if (swipeDirection.y > 0 && Mathf.Abs(swipeDirection.x) < Mathf.Abs(swipeDirection.y))
                        {
                            Debug.Log("Swiped Up");
                            player.Jump();
                        }
                        // Detect swipe left
                        else if (swipeDirection.x < 0 && Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                        {
                            Debug.Log("Swiped Left");
                            player.FaceLeft();
                            player.SwingWeapon();
                        }
                        // Detect swipe right
                        else if (swipeDirection.x > 0 && Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                        {
                            Debug.Log("Swiped Right");
                            player.FaceRight();
                            player.SwingWeapon();
                        }
                    }
                    break;
            }
        }
    }
}
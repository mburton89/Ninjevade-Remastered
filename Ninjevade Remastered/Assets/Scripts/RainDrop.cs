using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;

    private void Start()
    {
        Destroy(gameObject, 3);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollFly : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private float _force;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Fly()
    {
        Vector2 directionToFly = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        _rigidBody2D.AddForce(directionToFly.normalized * _force);
        _rigidBody2D.AddTorque(_force);
    }
}

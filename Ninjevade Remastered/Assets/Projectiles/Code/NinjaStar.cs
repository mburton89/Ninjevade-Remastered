using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStar : MonoBehaviour
{
    public Rigidbody2D rigidBody2D;
    public float rotationSpeed;
    [SerializeField] private AudioSource _deflectAudio;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().HandleHit();
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().HandleHit();
            Destroy(gameObject);
        }
        else if (collision.tag == "Weapon")
        {
            Deflect();
            _deflectAudio.Play();
        }
    }

    void Deflect()
    {
        rigidBody2D.velocity = -rigidBody2D.velocity * 2;
        rotationSpeed = -rotationSpeed * 2;
    }
}

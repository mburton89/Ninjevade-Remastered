using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemySpawner _controller;
    public float movementSpeed;
    private float _slowThrowSpeed;
    public float mediumThrowSpeed;
    private float _fastThrowSpeed;
    public float shortSecondsToWait;
    public float longSecondsToWait;
    private bool _throwsRight;
    private bool _hasHitThrowTrigger;
    private Animator _animator;
    [SerializeField] Transform _ninjaStarSpawnPosition;
    [SerializeField] private NinjaStar _ninjaStarPrefab;
    [SerializeField] private AudioSource _throwAudio;
    [SerializeField] private Explosion _explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Throw")
        {
            ThrowStar(mediumThrowSpeed);
            StartCoroutine(ThrowNextStarCo(_slowThrowSpeed, _fastThrowSpeed));
        }
    }

    public void Init(EnemySpawner controller, bool throwsRight)
    {
        _controller = controller;
        _throwsRight = throwsRight;
        _slowThrowSpeed = mediumThrowSpeed * 0.8f;
        _fastThrowSpeed = mediumThrowSpeed * 1.2f;
        if (!_throwsRight)
        {
            movementSpeed = -movementSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        _hasHitThrowTrigger = false;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_hasHitThrowTrigger)
        {
            transform.position += new Vector3(movementSpeed, 0, 0);
        }
    }

    void ThrowStar(float speed)
    {
        _hasHitThrowTrigger = true;
        NinjaStar ninjaStar = Instantiate(_ninjaStarPrefab, _ninjaStarSpawnPosition.position, transform.rotation);
        if(!_throwsRight)
        {
            speed = -speed;
        }
        ninjaStar.rigidBody2D.AddForce(Vector2.right * speed);
        _animator.SetTrigger("Throw");
        _throwAudio.Play();
    }

    public void HandleHit()
    {
        _controller.HandleEnemyDestroyed();
        Explosion explosion = Instantiate(_explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private IEnumerator ThrowNextStarCo(float slowSpeed, float fastSpeed)
    {
        yield return new WaitForSeconds(1);
        _animator.SetTrigger("StartNewThrow");
        float secondsBeforeThrow;
        int waitSequence = Random.Range(0, 2);
        if (waitSequence == 0)
        {
            secondsBeforeThrow = shortSecondsToWait;
        }
        else
        {
            secondsBeforeThrow = longSecondsToWait;
        }
        yield return new WaitForSeconds(secondsBeforeThrow);
        int sequence = Random.Range(0, 2);
        if (waitSequence == 0)
        {
            ThrowStar(slowSpeed);
        }
        else
        {
            ThrowStar(fastSpeed);
        }
        StartCoroutine(ThrowNextStarCo(_slowThrowSpeed, _fastThrowSpeed));
    }

    public void Cheer()
    {
        _animator.SetTrigger("Cheer");
    }
}

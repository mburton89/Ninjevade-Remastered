using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float jumpForce;
    public float gravityScale;

    [Header("Combat")]
    public float secondsDeflectHitBoxExists;
    public float secondsBeforeCanAttack;

    [Header("Animation Frames")]
    public List<Sprite> idleSprites;
    public List<Sprite> jumpSprites;
    public List<Sprite> deflectSprites;

    [Header("Animation Settings")]
    public float idleFrameRate = 8f;
    public float jumpFrameRate = 8f;
    public float deflectFrameRate = 12f;

    [SerializeField] private GameObject _attackHitBox;
    [SerializeField] private AudioSource _swingAudio;
    [SerializeField] private AudioSource _jumpAudio;

    private bool _isGrounded;
    private bool _isFacingRight;
    private bool _canSwingWeapon;

    private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _spriteRenderer;

    private Coroutine _animationCoroutine;

    private enum AnimationState
    {
        Idle,
        Jump,
        Deflect
    }

    private AnimationState _currentState;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _rigidBody2D.gravityScale = gravityScale;

        _isGrounded = true;
        _isFacingRight = true;
        _canSwingWeapon = true;

        PlayAnimation(AnimationState.Idle);
    }

    private void Update()
    {
        HandleKeyboard();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _rigidBody2D.linearVelocity = Vector2.zero;
            _isGrounded = true;

            if (_currentState != AnimationState.Deflect)
            {
                PlayAnimation(AnimationState.Idle);
            }
        }
    }

    public void Jump()
    {
        _rigidBody2D.AddForce(Vector2.up * jumpForce);

        _isGrounded = false;

        PlayAnimation(AnimationState.Jump);

        if (_jumpAudio != null)
        {
            _jumpAudio.Play();
        }
    }

    public void SwingWeapon()
    {
        if (_canSwingWeapon)
        {
            StartCoroutine(SwingWeaponCo());
            StartCoroutine(CanSwingWeaponBuffer());
        }
    }

    public void FaceLeft()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        _isFacingRight = false;
    }

    public void FaceRight()
    {
        transform.eulerAngles = Vector3.zero;
        _isFacingRight = true;
    }

    private void HandleKeyboard()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _isGrounded)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_isFacingRight)
            {
                FaceLeft();
            }

            SwingWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!_isFacingRight)
            {
                FaceRight();
            }

            SwingWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SwingWeapon();
        }
    }

    public void HandleHit()
    {
        SceneController.Instance.RestartScene();
        Destroy(gameObject);
    }

    private IEnumerator SwingWeaponCo()
    {
        _attackHitBox.SetActive(true);

        PlayAnimation(AnimationState.Deflect);

        if (_swingAudio != null)
        {
            _swingAudio.Play();
        }

        yield return new WaitForSeconds(secondsDeflectHitBoxExists);

        _attackHitBox.SetActive(false);

        if (_isGrounded)
        {
            PlayAnimation(AnimationState.Idle);
        }
        else
        {
            PlayAnimation(AnimationState.Jump);
        }
    }

    private IEnumerator CanSwingWeaponBuffer()
    {
        _canSwingWeapon = false;

        yield return new WaitForSeconds(secondsBeforeCanAttack);

        _canSwingWeapon = true;
    }

    private void PlayAnimation(AnimationState state)
    {
        if (_currentState == state)
        {
            return;
        }

        _currentState = state;

        if (_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(Animate(state));
    }

    private IEnumerator Animate(AnimationState state)
    {
        List<Sprite> frames;
        float frameRate;

        switch (state)
        {
            case AnimationState.Jump:
                frames = jumpSprites;
                frameRate = jumpFrameRate;
                break;

            case AnimationState.Deflect:
                frames = deflectSprites;
                frameRate = deflectFrameRate;
                break;

            default:
                frames = idleSprites;
                frameRate = idleFrameRate;
                break;
        }

        if (frames == null || frames.Count == 0)
        {
            yield break;
        }

        int frameIndex = 0;
        float delay = 1f / frameRate;

        while (_currentState == state)
        {
            _spriteRenderer.sprite = frames[frameIndex];

            frameIndex++;

            if (frameIndex >= frames.Count)
            {
                frameIndex = 0;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
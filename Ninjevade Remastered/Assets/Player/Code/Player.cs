using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float gravityScale;
    public float secondsDeflectHitBoxExists;
    public float secondsBeforeCanAttack;
    private bool _isGrounded;
    private bool _isFacingRight;
    private bool _canSwingWeapon;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    [SerializeField] private GameObject _attackHitBox;
    [SerializeField] private AudioSource _swingAudio;
    [SerializeField] private AudioSource _jumpAudio;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _rigidBody2D.gravityScale = gravityScale;
        _isGrounded = true;
        _isFacingRight = true;
        _canSwingWeapon = true;
    }

    void Update()
    {
        HandleKeyboard();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _rigidBody2D.velocity = Vector2.zero;
            _isGrounded = true;
            _animator.SetBool("IsJumping", false);
        }
    }

    void Jump()
    {
        _rigidBody2D.AddForce(Vector2.up * jumpForce);
        _isGrounded = false;
        _animator.SetBool("IsJumping", true);
        _jumpAudio.Play();
    }

    void SwingWeapon()
    {
        if (_canSwingWeapon)
        {
            StartCoroutine(nameof(SwingWeaponCo));
            StartCoroutine(nameof(CanSwingWeaponBuffer));
        }
    }

    void FaceLeft()
    {
        transform.eulerAngles = new Vector3(0, 180, 0);
        _isFacingRight = false;
    }

    void FaceRight()
    {
        transform.eulerAngles = Vector3.zero;
        _isFacingRight = true;
    }

    void HandleKeyboard()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && _isGrounded)
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
        _animator.SetBool("IsDeflecting", true);
        _swingAudio.Play();
        yield return new WaitForSeconds(secondsDeflectHitBoxExists);
        _attackHitBox.SetActive(false);
        _animator.SetBool("IsDeflecting", false);
    }

    private  IEnumerator CanSwingWeaponBuffer()
    {
        _canSwingWeapon = false;
        yield return new WaitForSeconds(secondsBeforeCanAttack);
        _canSwingWeapon = true;
    }
}

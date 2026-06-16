using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [Header("Movement")]
    public float minMoveSpeed = 1f;
    public float maxMoveSpeed = 5f;

    private float moveSpeed;

    private void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    private void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
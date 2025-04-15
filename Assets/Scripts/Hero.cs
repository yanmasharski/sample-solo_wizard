using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [SerializeField] private Rigidbody rb;

    public static Transform instance { get; private set; }

    private void Awake()
    {
        instance = transform;
    }

    private void Start()
    {
        rb.freezeRotation = true;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput -= 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            verticalInput += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verticalInput -= 1f;
        }
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (movement != Vector3.zero)
        {
            transform.position += movement * moveSpeed * Time.deltaTime;
        }
    }

    private void HandleRotation()
    {
        float rotationInput = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationInput += 1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationInput -= 1f;
        }

        if (rotationInput != 0f)
        {
            transform.Rotate(0f, rotationInput * rotationSpeed * Time.deltaTime, 0f);
        }

    }
}

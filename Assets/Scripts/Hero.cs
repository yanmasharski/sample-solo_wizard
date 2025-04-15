using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    /// <summary>
    /// Due to the spec armor logic is inversed. Lower value means more armor.
    /// </summary>
    [Range(0, 1)][SerializeField] private float armor = 0.5f;
    [SerializeField] private SpellCast[] spellCasts;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Rigidbody rb;

    private int health;
    private int spellCastIndex = 0;

    public static Transform instance { get; private set; }

    private void Awake()
    {
        instance = transform;
    }

    private void Start()
    {
        rb.freezeRotation = true;
        health = maxHealth;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleSpellCast();
    }

    private void HandleMovement()
    {
        float forwardInput = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            forwardInput += 1f;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            forwardInput -= 1f;
        }

        var movement = transform.forward * forwardInput;

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

    private void HandleSpellCast()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            spellCastIndex++;
            spellCastIndex = spellCastIndex % spellCasts.Length;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            spellCastIndex--;
            spellCastIndex = spellCastIndex % spellCasts.Length;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            var spellCast = spellCasts[spellCastIndex];
            spellCast.Cast(transform.position, transform.forward);
        }
    }

    private void DealDamage(IDamageDealer damageDealer)
    {
        health -= (int)(damageDealer.Damage * armor);
        if (health <= 0)
        {
            Die();
        }

        damageDealer.DamageDealed();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IDamageDealer>(out var damageDealer))
        {
            DealDamage(damageDealer);
        }
    }

    private void Die()
    {
        Debug.Log("Hero is dead");
        Destroy(gameObject);
    }



}

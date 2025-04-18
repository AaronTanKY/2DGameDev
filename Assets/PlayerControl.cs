using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    float movementX;
    float movementZ;
    bool isGrounded = false;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float force = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX, movementZ);
        rb.AddForce(movement * speed);

        // speed limit
        if (rb.linearVelocity.magnitude > 7)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * 7;
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        Debug.Log(v);

        movementX = v.x;
        movementZ = v.y;
    }

    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

}

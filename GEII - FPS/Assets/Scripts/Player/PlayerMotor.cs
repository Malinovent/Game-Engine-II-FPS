using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 velocity;

    public void UpdateMotor()
    {
        if (controller.isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Move(Vector2 input)
    {
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    public void Jump()
    {
        if (controller.isGrounded)
        {
            // Physics-based jump formula
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    public void Rotate(float inputX)
    {
        transform.Rotate(Vector3.up * inputX);
    }
}

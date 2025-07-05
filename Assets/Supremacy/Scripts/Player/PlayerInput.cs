using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 moveInput;
    public Vector2 dashInput;

    public bool jumpPressed;
    public bool dashPressed;

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        dashInput = moveInput.normalized;

        jumpPressed = Input.GetKey(KeyCode.Space);
        dashPressed = Input.GetKey(KeyCode.LeftShift);
    }
}

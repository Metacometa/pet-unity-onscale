using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 moveInput;
    public bool jumpInput;
    public bool dashInput;

    public Vector2 mousePosition;

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        jumpInput = Input.GetKey(KeyCode.Space);
        dashInput = Input.GetKey(KeyCode.LeftShift);

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}

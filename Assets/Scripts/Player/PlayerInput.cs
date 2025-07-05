using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public void HandleInput(ref InputContext context)
    {
        context.moveInput.x = Input.GetAxisRaw("Horizontal");
        context.moveInput.y = Input.GetAxisRaw("Vertical");

        context.dashInput = context.moveInput.normalized;

        context.jumpPressed = Input.GetKey(KeyCode.Space);
        context.dashPressed = Input.GetKey(KeyCode.LeftShift);
    }
}

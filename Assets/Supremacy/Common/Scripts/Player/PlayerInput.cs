using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public Vector2 moveInput;
    public bool jumpInput;

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetKey(KeyCode.Space);
    }
}

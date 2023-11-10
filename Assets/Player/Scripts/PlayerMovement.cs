using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public float horizontalInput;
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update() 
    {
        if (player.moveDirection.x > 0) 
        {
            horizontalInput = 1;
        }
        else if (player.moveDirection.x < 0) 
        {
            horizontalInput = -1;
        }
        else 
        {
            horizontalInput = 0;
        }
    }

    public void Move() 
    {
        if (horizontalInput > 0) 
        {
            player.rb.velocity = new Vector2(horizontalInput * moveSpeed, player.rb.velocity.y);
        }
        else if (horizontalInput < 0) 
        {
            player.rb.velocity = new Vector2(horizontalInput * moveSpeed, player.rb.velocity.y);        
        }
        else 
        {
            player.rb.velocity = new Vector2(0, player. rb.velocity.y);        
        }
    }




}

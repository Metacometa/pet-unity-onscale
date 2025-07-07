using UnityEngine;

[System.Serializable]
public class PlayerContext
{
    public bool isDashing = false;
    public bool canDash = true;

    public AirState airState = AirState.Null;

    [SerializeField] private Vector2 orientation = Vector2.down.normalized;
    public Vector2 Orientation
    {
        get => orientation.normalized;
        set => orientation = value.normalized;
    }
}

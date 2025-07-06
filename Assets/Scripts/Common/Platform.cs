using UnityEngine;

public enum PlatformType
{
    Normal,
    Gravity,
    Moving,
    Bouncy
}

public class Platform : MonoBehaviour
{
    [SerializeField] public PlatformType type;
}

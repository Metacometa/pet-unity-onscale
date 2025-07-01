using Unity.VisualScripting;
using UnityEngine;

public class PlayerOrientation : MonoBehaviour
{
    [SerializeField]/*[HideInInspector]*/ private Vector2 dir;
    public Vector2 Dir
    {
        get => dir.normalized;
        set => dir = value.normalized;
    }

    void Awake()
    {
        dir = Vector2.down.normalized;
    }

    public void RotateTo(in Vector2 dir)
    {
        this.dir = dir.normalized;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + (Vector3)Dir;

        Gizmos.DrawLine(startPoint, endPoint);
        
        //Debug.DrawRay(transform.position, dir, Color.red);
    }
}

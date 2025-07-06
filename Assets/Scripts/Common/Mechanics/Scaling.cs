using UnityEngine;

public class Scaling : MonoBehaviour
{
    [SerializeField] private float scalingSpeed;

    [SerializeField] private float minScale = 0.2f;
    [SerializeField] private float maxScale = 1.5f;

    private Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }
    private Vector3 Scale
    {
        get => transform.localScale;
        set => transform.localScale = value;
    }

    public void Shrink(float minValue, float maxValue)
    {
        float scaleDelta = scalingSpeed * Time.deltaTime;
        float newScale = Scale.y + scaleDelta;

        if (newScale >= minValue && newScale <= maxValue)
        {
            Scale += new Vector3(scaleDelta, scaleDelta, 0);

            //Vertical shift
            Scale = new Vector3(Position.x, Position.y + scaleDelta, 0);
        }
    }
}

using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public TurnAxis turnAxis;
    public TurnDirection turnDirection;
    public float turnSpeed;

    void Update()
    {
        Turn();
    }

    void Turn()
    {
        if (turnAxis == TurnAxis.Y)
        {
            Vector3 angle = Vector3.up * Time.deltaTime * turnSpeed * (int)turnDirection;
            transform.Rotate(angle);
        }
        else if (turnAxis == TurnAxis.Z)
        {
            Vector3 angle = Vector3.forward * Time.deltaTime * turnSpeed * (int)turnDirection;
            transform.Rotate(angle);
        }

    }
}

public enum TurnDirection
{
    Clockwise = 1,
    Counterclockwise = -1
}

public enum TurnAxis
{
    Y,
    Z
}

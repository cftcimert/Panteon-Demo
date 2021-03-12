using UnityEngine;

public class HorizontalObstacleMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;
    private Direction direction;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (direction == Direction.Left)
        {
            rb.velocity = Vector3.left * speed;

            if (transform.position.x <= leftPoint.position.x)
                direction = Direction.Right;
        }

        else if (direction == Direction.Right)
        {
            rb.velocity = Vector3.right * speed;

            if (transform.position.x >= rightPoint.position.x)
                direction = Direction.Left;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftPoint.position, 0.2f);
        Gizmos.DrawWireSphere(rightPoint.position, 0.2f);
        Gizmos.DrawLine(leftPoint.position, rightPoint.position);
    }
}

enum Direction
{
    Left,
    Right
}
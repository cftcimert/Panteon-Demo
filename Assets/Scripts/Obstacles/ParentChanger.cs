using UnityEngine;

public class ParentChanger : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Opponent"))
        {
            if (!collision.gameObject.GetComponent<OpponentFall>().isFalling)
            {
                collision.transform.SetParent(transform);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
}

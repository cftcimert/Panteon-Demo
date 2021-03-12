using UnityEngine;

public class PlayerFall : MonoBehaviour
{
    Animator animator;
    GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (gameManager.isGame)
            {
                Fall();
            }           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border"))
        {
            FallingIdle(other.transform.parent);
        }
    }

    private void Fall()
    {
        animator.SetBool("isRun", false);
        animator.SetTrigger("Fall");
        StartCoroutine(gameManager.FinishGame());
    }

    private void FallingIdle(Transform border)
    {
        transform.SetParent(border);
        animator.SetBool("isRun", false);
        animator.SetTrigger("Falling Idle");
        StartCoroutine(gameManager.FinishGame());
    }
}
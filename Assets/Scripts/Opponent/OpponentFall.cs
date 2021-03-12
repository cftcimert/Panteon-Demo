using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class OpponentFall : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;
    OpponentMovement opponentMovement;
    [HideInInspector] public bool isFalling;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        opponentMovement = GetComponent<OpponentMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(Fall());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border"))
        {
            StartCoroutine(FallingIdle(other.transform.parent));
        }
    }

    private IEnumerator Fall()
    {
        if (!navMeshAgent.isStopped)
        {
            animator.SetBool("isRun", false);
            animator.SetTrigger("Fall");
            navMeshAgent.isStopped = true;
            yield return new WaitForSeconds(3);
            BackStartPoint();
        }
    }

    private IEnumerator FallingIdle(Transform border)
    {
        isFalling = true;
        transform.SetParent(border);
        navMeshAgent.enabled = false;
        animator.SetBool("isRun", false);
        animator.SetTrigger("Falling Idle");
        yield return new WaitForSeconds(3);
        BackStartPoint();
    }

    private void BackStartPoint()
    {
        isFalling = false;
        transform.position = opponentMovement.startPoint;
        animator.SetBool("isRun", true);
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(opponentMovement.endPoint);
    }
}

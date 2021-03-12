using UnityEngine;
using UnityEngine.AI;

public class OpponentMovement : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navMeshAgent;

    [HideInInspector] public Vector3 startPoint;
    [HideInInspector] public Vector3 endPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        startPoint = transform.position;
        endPoint = startPoint + Vector3.forward * 125;

        navMeshAgent.SetDestination(endPoint);
    }

    void Update()
    {
        if (navMeshAgent.enabled)
        {
            float instantSpeed = Vector3.Distance(Vector3.zero, navMeshAgent.desiredVelocity);

            //Sets the opponent run animation
            if (instantSpeed < 1)
            {
                animator.speed = 1;
                animator.SetBool("isRun", false);
            }
            else
            {
                animator.SetBool("isRun", true);
                animator.speed = instantSpeed / navMeshAgent.speed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GetComponent<Ranking>().isFinish = true;
            gameObject.SetActive(false);
        }
    }
}

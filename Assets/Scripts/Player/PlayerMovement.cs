using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Transform shotPoint;
    private Animator animator;
    private GameManager gameManager;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManager.isGame)
        {
            if (gameManager.gameMode == GameMode.Running)
            {
                //Movement player by using finger press and drag
                if (Input.GetMouseButton(0))
                {
                    transform.position += transform.forward * speed * Time.deltaTime;
                }

                //Sets the player run animation
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("isRun", true);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    animator.SetBool("isRun", false);
                }
            }

            if (gameManager.gameMode == GameMode.Shooting)
            {
                float instantSpeed = Vector3.Distance(Vector3.zero, navMeshAgent.desiredVelocity);
                if (instantSpeed < 0.1f)
                {
                    navMeshAgent.enabled = false;
                    animator.SetBool("isRun", false);
                    ActivateShooting();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            if (gameManager.gameMode == GameMode.Running)
            {
                transform.SetParent(gameManager.transform.parent);
                gameManager.gameMode = GameMode.Shooting;
                transform.rotation = Quaternion.Euler(Vector3.zero); //fixs the rotation
                animator.SetBool("isRun", true);
                GoToShotPoint();
            }
        }
    }

    private void GoToShotPoint()
    {
        navMeshAgent.speed = speed;
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(shotPoint.position);
    }

    private void ActivateShooting()
    {
        GameObject.FindGameObjectWithTag("MainCamera").transform.SetParent(transform);
        transform.rotation = Quaternion.Euler(Vector3.zero); //fixs the rotation
        FindObjectOfType<CameraController>().SwitchToFPS();
    }

    public void StartRunAnimation()
    {
        animator.SetBool("isRun", true);
    }
}

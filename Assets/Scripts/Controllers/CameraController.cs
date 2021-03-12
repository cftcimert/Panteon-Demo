using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Animator animator;
    Vector3 offset;
    [HideInInspector] public bool isFps;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (!isFps)
            transform.position = target.transform.position + offset;
    }

    public void SwitchToFPS()
    {
        isFps = true;
        animator.SetTrigger("Switch");
        FindObjectOfType<MenuController>().ActivedPaintSlider();
    }
}

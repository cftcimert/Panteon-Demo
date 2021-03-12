using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public LayerMask platformLayerMask;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.isGame)
        {
            if (gameManager.gameMode == GameMode.Running)
            {
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out RaycastHit raycast, 100, platformLayerMask))
                    {
                        transform.LookAt(raycast.point, Vector3.up);
                    }
                }
            }
        }
    }
}

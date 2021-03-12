using UnityEngine;

public class PaintballShooter : MonoBehaviour
{
    GameManager gameManager;
    MenuController menuController;
    CameraController cameraController;

    public float shotTime;
    public LayerMask wallLayerMask;
    float passingTime;

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        menuController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuController>();
        passingTime = shotTime;
    }

    void Update()
    {
        if (cameraController.isFps && gameManager.isGame)
        {
            if (passingTime < shotTime)
            {
                passingTime += Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    passingTime = 0;
                    Vector3 touchPoint = CalculateShootRay();
                    if (touchPoint != Vector3.zero)
                    {
                        GameObject paintball = PaintballPool.instance.GetPaintballFromPool();
                        paintball.GetComponent<PaintProjectileBehavior>().target = CalculateShootRay(); //sends its destination
                        paintball.GetComponent<PaintProjectileBehavior>().paintColor = PaintProjectileManager.GetInstance().paintBombColor;
                    }

                    if (menuController.CheckSliderValue())
                    {
                        StartCoroutine(FindObjectOfType<GameManager>().WinGame());
                    }
                }
            }
        }
    }

    private Vector3 CalculateShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycast, 100, wallLayerMask))
        {
            return raycast.point;
        }

        return Vector3.zero;
    }
}
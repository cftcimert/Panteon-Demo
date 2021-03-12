using UnityEngine;
using System.Collections.Generic;

public class PaintballPool : MonoBehaviour
{
    public static PaintballPool instance;
    private readonly Stack<GameObject> paintballPool = new Stack<GameObject>();

    private void Start()
    {
        if (null != instance)
            Destroy(this);
        else
        {
            instance = this;
        }
    }

    public GameObject GetPaintballFromPool()
    {
        // Check for objects in the pool
        if (paintballPool.Count > 0)
        {
            // Get the last object in the pool
            GameObject paintball = paintballPool.Pop();
            paintball.GetComponent<PaintProjectileBehavior>().startPoint = Camera.main.transform.position + Camera.main.transform.forward * 3;
            paintball.gameObject.SetActive(true);
            return paintball;
        }

        // creates a new paintball.
        return Instantiate(PaintProjectileManager.GetInstance().paintBombPrefab, Camera.main.transform.position + Camera.main.transform.forward * 3, Camera.main.transform.rotation);
    }

    public void AddPaintballToPool(GameObject paintball)
    {
        paintball.gameObject.SetActive(false);

        // add paintball to the pool
        paintballPool.Push(paintball);
    }
}

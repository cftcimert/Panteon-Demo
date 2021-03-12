using UnityEngine;
using System.Collections;

public class MoveStick : MonoBehaviour
{
    public float speed = 2.0f;
    public float minShotTime, maxShotTime;
    float startPoint;
    float randomDuration;

    void Start()
    {
        startPoint = transform.localPosition.x;
        randomDuration = Random.Range(minShotTime, maxShotTime);
    }

    void Update()
    {
        if (IsShot())
        {
            StartCoroutine(MoveOneShot());
        }
    }

    private IEnumerator MoveOneShot()
    {
        //Move to left
        while (transform.localPosition.x > -startPoint)
        {
            transform.localPosition += Vector3.left * speed * Time.deltaTime / 10;
            yield return new WaitForEndOfFrame();
        }

        //Move to right
        while (transform.localPosition.x < startPoint)
        {
            transform.localPosition += Vector3.right * speed * Time.deltaTime / 10;
            yield return new WaitForEndOfFrame();
        }
    }

    private bool IsShot()
    {
        if (randomDuration > 0)
        {
            randomDuration -= Time.deltaTime;
        }
        else
        {
            randomDuration = Random.Range(minShotTime, maxShotTime);
            return true;
        }

        return false;
    }
}

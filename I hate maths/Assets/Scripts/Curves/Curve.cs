using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{
    private bool isGoing;
    [SerializeField] int i = 0;
    [SerializeField] int randomNum;
    [SerializeField] float speed;
    [SerializeField] float t;

    [SerializeField] Transform RouteToFollow;
    [SerializeField] Transform[] Route;

    private Vector2 objPos;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        randomNum = Random.Range(0, 3);
            if (randomNum == 0)
            {
                i = 0;
                RouteToFollow = Route[0];
            }
            else if (randomNum == 1)
            {
                i = 1;
                RouteToFollow = Route[1];
            }

        Debug.Log(Route.Length);
        t = 0;

        isGoing = true;
    }

    private void Update()
    {
        if (isGoing)
            StartCoroutine(Go());
    }

    IEnumerator Go()
    {
//        isGoing = false;

        Vector2 p0 = Route[i].GetChild(0).position;
        Vector2 p1 = Route[i].GetChild(1).position;
        Vector2 p2 = Route[i].GetChild(2).position;
        Vector2 p3 = Route[i].GetChild(3).position;


        while(t <= 1)
        {
            t += Time.deltaTime * speed;
            objPos = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 +
                3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;

            transform.position = objPos;

            yield return new WaitForEndOfFrame();

        }
        t = 1;
        yield return new WaitForSeconds(.1f);
        while(t >= 0)
        {
            t -= Time.deltaTime * speed;
            objPos = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 +
                3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;

            transform.position = objPos;

            yield return new WaitForEndOfFrame();
        }
        t = 0;

        isGoing = true;
    }
}
